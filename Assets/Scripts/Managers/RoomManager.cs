using Enums;
using Photon.Pun;
using PLayerScripts;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerReference;
        [SerializeField] private Transform spawnPoint;

        private string playerName;
        private PhotonNetworkManager net;

        private void Awake()
        {
            var nameAndRoomCode = GameManager.Instance.GetNameAndRoomCode();
            playerName = nameAndRoomCode.Key;
            PhotonNetworkManager.Instance.OnJoinedRoomEvent += SpawnPlayer;
        }

        private void OnDestroy()
        {
            PhotonNetworkManager.Instance.OnJoinedRoomEvent -= SpawnPlayer;
        }

        private void SpawnPlayer()
        {
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
    }
}