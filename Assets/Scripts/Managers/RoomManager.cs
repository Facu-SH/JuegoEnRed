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
        private PhotonNetworkManager net;

        private void Start()
        {
            var nameAndRoomCode = GameManager.Instance.GetNameAndRoomCode();
            playerName = nameAndRoomCode.Key;
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

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
            SceneManager.LoadScene(1);
        }
    }
}
