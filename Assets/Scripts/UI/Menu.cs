using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Managers;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [Header("Panels")] [SerializeField] private GameObject changeNameMenu;
        [SerializeField] private GameObject settingsMenu;

        [Header("Displays")] [SerializeField] private TextMeshProUGUI playerNameDisplay;
        [SerializeField] private TextMeshProUGUI roomCodeDisplay;

        [Header("Inputs")] [SerializeField] private TMP_InputField playerNameInput;
        [SerializeField] private TMP_InputField roomCodeInput;

        [Header("Buttons")] [SerializeField] private Button joinButton;
        [SerializeField] private Button exitButton;

        [Header("Feedback")] [SerializeField] private TextMeshProUGUI feedbackText;

        private void Awake()
        {
            joinButton.interactable = false;
            feedbackText.text = "";

            PhotonNetworkManager.Instance.OnConnectedToMasterEvent += OnConnectedToMaster;
            PhotonNetworkManager.Instance.OnJoinRoomFailedHandler += OnJoinRoomFailed;
            PhotonNetworkManager.Instance.OnNetworkDisconnected += OnNetworkDisconnected;

            exitButton.onClick.AddListener(Application.Quit);

            if (PhotonNetworkManager.Instance.IsConnectedToMasterServer)
            {
                OnConnectedToMaster();
            }
        }

        private void OnDestroy()
        {
            PhotonNetworkManager.Instance.OnConnectedToMasterEvent -= OnConnectedToMaster;
            PhotonNetworkManager.Instance.OnJoinRoomFailedHandler -= OnJoinRoomFailed;
            PhotonNetworkManager.Instance.OnNetworkDisconnected -= OnNetworkDisconnected;

            exitButton.onClick.RemoveListener(Application.Quit);
        }

        private void OnConnectedToMaster()
        {
            joinButton.interactable = true;
            feedbackText.text = "Conectado a Photon";
        }

        private void OnJoinRoomFailed(short returnCode, string message)
        {
            feedbackText.text = $"Error al unirse: {message} ({returnCode})";
        }

        private void OnNetworkDisconnected(DisconnectCause cause)
        {
            feedbackText.text = $"Desconectado: {cause}";
            joinButton.interactable = false;
        }

        public void OnJoinClicked()
        {
            feedbackText.text = "Intentando unirse...";
            playerNameDisplay.text = playerNameInput.text;
            roomCodeDisplay.text = roomCodeInput.text;

            GameManager.Instance.SetNameAndRoomCode(
                playerNameInput.text,
                roomCodeInput.text
            );
            SceneManager.LoadScene("MainScene");
            PhotonNetworkManager.Instance.JoinRoom(roomCodeInput.text);
        }

        public void OpenChangeNameMenu()
        {
            changeNameMenu.SetActive(true);
        }

        public void CloseChangeNameMenu()
        {
            changeNameMenu.SetActive(false);
        }

        public void OpenSettingsMenu()
        {
            settingsMenu.SetActive(true);
        }

        public void CloseSettingsMenu()
        {
            settingsMenu.SetActive(false);
        }

        public void ChangePlayerName()
        {
            playerNameDisplay.text = playerNameInput.text;
        }

        public void ChangeRoomCode()
        {
            roomCodeDisplay.text = roomCodeInput.text;
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(1);
        }
    }
}