using Managers;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

namespace UI
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private GameObject disconnectPanel;
        [SerializeField] private Button returnToMenuButton;
        [SerializeField] private TextMeshProUGUI disconnectReasonText;
        [SerializeField] private GameObject deadMessage;

        private void Awake()
        {
            PhotonNetworkManager.Instance.OnNetworkDisconnected += ShowDisconnectPanel;
        }

        private void Start()
        {
            returnToMenuButton.onClick.AddListener(() =>
                UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"));
            MyPlayerManager.Instance.SetDeadMessageInstance(deadMessage);
        }

        private void OnDestroy()
        {
            if (PhotonNetworkManager.Instance != null)
                PhotonNetworkManager.Instance.OnNetworkDisconnected -= ShowDisconnectPanel;
        }

        private void ShowDisconnectPanel(DisconnectCause cause)
        {
            switch (cause)
            {
                case DisconnectCause.ClientTimeout:
                case DisconnectCause.ServerTimeout:
                    disconnectReasonText.text = "La conexión tardó demasiado.";
                    break;
                case DisconnectCause.ExceptionOnConnect:
                    disconnectReasonText.text = "Error al conectar con el Master Server.";
                    break;
                default:
                    disconnectReasonText.text = $"Desconectado: {cause}";
                    break;
            }
            disconnectPanel.SetActive(true);
        }
    }

}