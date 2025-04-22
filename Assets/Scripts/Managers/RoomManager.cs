using Enums;
using Photon.Pun;
using PLayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerReference;
        [SerializeField] private Transform spawnPoint;

        private string playerName;
        private string roomCode;
        private PhotonNetworkManager net;

        private void Start()
        {
            var nameAndRoomCode = GameManager.Instance.GetNameAndRoomCode();
            playerName = nameAndRoomCode.Key;
            roomCode   = nameAndRoomCode.Value;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("RoomManager: ¡Sala unida! Instanciando jugador…");

            TeamColor myTeam = PhotonNetwork.LocalPlayer.ActorNumber % 2 == 0
                ? TeamColor.Blue
                : TeamColor.Red;

            object[] initData = new object[] { (int)myTeam };

            GameObject player = PhotonNetwork.Instantiate(
                playerReference.name,
                spawnPoint.position,
                Quaternion.identity,
                0,
                initData
            );

            if (player.TryGetComponent<PlayerSetUp>(out var setup))
                setup.StartUpLocalPlayer(playerName);
        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Debug.Log("RoomManager: Sala abandonada, volviendo al menú...");
            SceneManager.LoadScene(1);
        }
    }
}
