using System.Collections.Generic;
using Photon.Pun;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviourPun
    {
        public static GameManager Instance { get; private set; }

        private LevelUI levelUI;
        private int[] teamScores = new int[2];
        private string playerName;
        private string roomCode;

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

        private void Update()
        {
            if (levelUI != null)
                levelUI.SetTimer(Time.timeSinceLevelLoad);
        }

        public void SetNameAndRoomCode(string playerName, string roomCode)
        {
            this.playerName = playerName;
            this.roomCode = roomCode;
        }

        public KeyValuePair<string, string> GetNameAndRoomCode()
        {
            return new KeyValuePair<string,string>(playerName, roomCode);
        }

        public void OnPlayerDamage(int health)
        {
            levelUI?.SetHealth(health);
        }

        public void OnPlayerDeath(int deadTeamID)
        {
            int otherTeamID = deadTeamID == 0 ? 1 : 0;
            photonView.RPC(nameof(RPC_AddPoints), RpcTarget.AllBuffered, otherTeamID, 1);
        }

        [PunRPC]
        private void RPC_AddPoints(int teamID, int points)
        {
            teamScores[teamID] += points;
            if (levelUI != null)
                levelUI.SetTeamScore(teamID, teamScores[teamID]);
        }
        public void SetLevelUIInstance(LevelUI levelUI)
        {
            this.levelUI = levelUI;
        }
    }
}