using Managers;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

namespace UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField roomInputField;
        [SerializeField] private Button joinButton;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private GameObject errorPanel;
        [SerializeField] private TextMeshProUGUI errorText;

        private void Awake()
        {
            PhotonNetworkManager.Instance.OnJoinRoomFailedHandler += HandleJoinRoomFailed;
            PhotonNetworkManager.Instance.OnNetworkDisconnected += HandleDisconnected;
        }

        private void OnDestroy()
        {
            PhotonNetworkManager.Instance.OnJoinRoomFailedHandler -= HandleJoinRoomFailed;
            PhotonNetworkManager.Instance.OnNetworkDisconnected -= HandleDisconnected;
        }
        
        private void HandleJoinRoomFailed(short code, string message)
        {
            errorText.text = $"Error al unirse: {message} ({code})";
            errorPanel.SetActive(true);
        }

        private void HandleDisconnected(DisconnectCause cause)
        {
            switch (cause)
            {
                case DisconnectCause.ClientTimeout:
                case DisconnectCause.ServerTimeout:
                    errorText.text = "Tiempo de conexión agotado con el servidor.";
                    break;

                case DisconnectCause.ExceptionOnConnect:
                    errorText.text = "No se pudo conectar al Master Server.";
                    break;

                default:
                    errorText.text = $"Desconectado: {cause}";
                    break;
            }

            errorPanel.SetActive(true);
        }

        public void CloseFeedbackPanel()
        {
            errorPanel.SetActive(false);
        }
    }
}