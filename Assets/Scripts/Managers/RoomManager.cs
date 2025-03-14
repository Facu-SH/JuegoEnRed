using Photon.Pun;
using PLayerScripts;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerReference;
        [SerializeField] private Transform spawnPoint;
        void Start()
        {
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
            PhotonNetwork.JoinOrCreateRoom("Prueba",null,null);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Joined the room");
            
            GameObject player = PhotonNetwork.Instantiate(playerReference.name, spawnPoint.position, Quaternion.identity);
            if (player.TryGetComponent<PlayerSetUp>(out PlayerSetUp playerSetUp))
            {
                playerSetUp.StartUpLocalPlayer();
            }

        }
    }
}
