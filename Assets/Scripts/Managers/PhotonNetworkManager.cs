using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

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
        public event Action<Hashtable> OnRoomPropertiesUpdateEvent;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            PhotonNetwork.NetworkingClient.StateChanged += OnClientStateChanged;
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
        
        private void OnClientStateChanged(ClientState from, ClientState to)
        {
            if (to == ClientState.Disconnecting || to == ClientState.Disconnected)
            {
                var cause = PhotonNetwork.NetworkingClient.DisconnectedCause;
                if (cause != DisconnectCause.DisconnectByClientLogic)
                    OnNetworkDisconnected?.Invoke(cause);
            }
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
        
        public override void OnRoomPropertiesUpdate(Hashtable propsThatChanged)
        {
            base.OnRoomPropertiesUpdate(propsThatChanged);
            OnRoomPropertiesUpdateEvent?.Invoke(propsThatChanged);
        }
    }
}