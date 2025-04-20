using Enums;
using Photon.Pun;
using PLayerScripts;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerReference;
        [SerializeField] private Transform spawnPoint;
        private string playerName;
        private string roomCode;
        
        void Start()
        {
            var nameAndRoomCode = GameManager.Instance.GetNameAndRoomCode();
            playerName = nameAndRoomCode.Key;
            roomCode = nameAndRoomCode.Value;
            Debug.Log("Trying to connect to the server");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connected to master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            Debug.Log("Trying to join a room");
            PhotonNetwork.JoinOrCreateRoom(roomCode,null,null);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Joined the room");
            
            TeamColor myTeam = PhotonNetwork.LocalPlayer.ActorNumber % 2 == 0
                ? TeamColor.Blue
                : TeamColor.Red;
            
            PhotonNetwork.LocalPlayer.SetCustomProperties(
                new ExitGames.Client.Photon.Hashtable{{ "TeamColor", myTeam }}
            );
            
            GameObject player = PhotonNetwork.Instantiate(
                playerReference.name,
                spawnPoint.position,
                Quaternion.identity
            );
            if (player.TryGetComponent<PlayerSetUp>(out PlayerSetUp playerSetUp))
            {
                playerSetUp.StartUpLocalPlayer(playerName);
            }

        }
    }
}
