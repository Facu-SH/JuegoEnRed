using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using LevelScripts;
using Photon.Pun;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviourPun
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private LevelVariables data;

        private LevelUI levelUI;
        private FloorController floorController;
        private string playerName;
        private string roomCode;
        private double startTime = 0;
        private bool isEnd;
        private bool[] isFloorDown = new bool[]{false,false};
        public bool shouldWait;

        private Dictionary<int, int> teamScores = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 0 }
        };

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            PhotonNetworkManager.Instance.OnJoinedRoomEvent += OnJoinedRoom;
            PhotonNetworkManager.Instance.OnRoomPropertiesUpdateEvent += OnRoomPropertiesUpdate;
        }

        private void OnDestroy()
        {
            PhotonNetworkManager.Instance.OnJoinedRoomEvent -= OnJoinedRoom;
            PhotonNetworkManager.Instance.OnRoomPropertiesUpdateEvent -= OnRoomPropertiesUpdate;
        }

        private void Update()
        {
            if (shouldWait) return;
            
            if (!isEnd && levelUI != null && startTime > 0)
            {
                float elapsed = (float)(PhotonNetwork.Time - startTime);
                levelUI.SetTimer(elapsed);
                
                if (!isFloorDown[0] && elapsed > data.TimeToFloorDown1 && floorController != null && photonView.IsMine)
                {
                    isFloorDown[0] = true;
                    floorController.FirstFloorDown();
                }
                else if (!isFloorDown[1] && elapsed > data.TimeToFloorDown2 && floorController != null && photonView.IsMine)
                {
                    isFloorDown[1] = true;
                    floorController.SecondFloorDown();
                }
                
                if (PhotonNetwork.IsMasterClient && elapsed >= data.TimeToEndLevel)
                {
                    TryEndGame(true);
                }
            }
        }

        private void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                double t0 = PhotonNetwork.Time;
                var props = new Hashtable { { "StartTime", t0 } };
                PhotonNetwork.CurrentRoom.SetCustomProperties(props);
                startTime = t0;
            }
            else
            {
                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("StartTime", out object ts))
                    startTime = (double)ts;
            }
        }
        
        private void OnRoomPropertiesUpdate(Hashtable propsThatChanged)
        {
            if (propsThatChanged.TryGetValue("StartTime", out object ts))
                startTime = (double)ts;
        }

        public KeyValuePair<string, string> GetNameAndRoomCode()
        {
            return new KeyValuePair<string, string>(playerName, roomCode);
        }

        public void SetNameAndRoomCode(string _playerName, string _roomCode)
        {
            playerName = _playerName;
            roomCode = _roomCode;
            isEnd = false;
            
            teamScores = new Dictionary<int, int>
            {
                { 0, 0 },
                { 1, 0 }
            };
            isFloorDown = new bool[]{false,false};
            shouldWait = true;
        }

        public void SetLevelUIInstance(LevelUI _levelUI)
        {
            levelUI = _levelUI;
        }
        
        public void SetFloorControllerInstance(FloorController _floorController)
        {
            floorController = _floorController;
        }

        public void OnPlayerDamage(int health)
        {
            levelUI?.SetHealth(health);
        }

        public void OnPlayerDeath(int deadTeamID)
        {
            int otherTeamID = deadTeamID == 0 ? 1 : 0;
            photonView.RPC(nameof(RPC_AddPoints), RpcTarget.AllBuffered, otherTeamID, 1);

            TryEndGame(false);
        }

        private void TryEndGame(bool isTimeToEnd)
        {
            if (isTimeToEnd)
            {
                var winnerTeamIndex = teamScores.OrderByDescending(kv => kv.Value).First();
                photonView.RPC(nameof(RPC_ActiveEndCanvas), RpcTarget.AllBuffered, winnerTeamIndex.Key);
            }
            else
            {
                var winnerTeamIndex = teamScores.FirstOrDefault(x => x.Value >= data.PointsToWin);
                if (winnerTeamIndex.Value != 0)
                {
                    photonView.RPC(nameof(RPC_ActiveEndCanvas), RpcTarget.AllBuffered, winnerTeamIndex.Key);
                }
            }
        }

        [PunRPC]
        private void RPC_ActiveEndCanvas(int winnerTeamIndex)
        {
            isEnd = true;
            levelUI.ActiveEndCanvas(winnerTeamIndex);
            MyPlayerManager.Instance.SetEndedGame();
        }

        [PunRPC]
        private void RPC_AddPoints(int teamID, int points)
        {
            teamScores[teamID] += points;
            if (levelUI != null)
                levelUI.SetTeamScore(teamID, teamScores[teamID]);
        }
    }
}