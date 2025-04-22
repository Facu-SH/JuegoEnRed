using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
namespace Managers
{
    public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    public static PhotonNetworkManager Instance { get; private set; }
    public bool IsConnectedToMasterServer { get; private set; }
    
    public event Action OnConnectedToMasterEvent;
    public event Action<DisconnectCause> OnNetworkDisconnected;
    public event Action<short, string> OnJoinRoomFailedHandler;
    public event Action OnJoinedRoomEvent;
    
    private string pendingRoomName;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public void JoinRoom(string roomName)
    {
        if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMaster)
        {
            PhotonNetwork.JoinOrCreateRoom(
                roomName,
                new RoomOptions { MaxPlayers = 4 },
                TypedLobby.Default
            );
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        IsConnectedToMasterServer = true;
        OnConnectedToMasterEvent?.Invoke();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        IsConnectedToMasterServer = false;
        if (cause == DisconnectCause.DisconnectByClientLogic) return;
        OnNetworkDisconnected?.Invoke(cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        OnJoinRoomFailedHandler?.Invoke(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        OnJoinedRoomEvent?.Invoke();
    }
}
}