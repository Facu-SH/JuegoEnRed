using Managers;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

namespace UI
{
    public class GameUIErrorHandler : MonoBehaviour
    {
        [SerializeField] private GameObject disconnectPanel;
        [SerializeField] private TextMeshProUGUI disconnectReasonText;
        [SerializeField] private GameObject deadMessage;

        private void Awake()
        {
            PhotonNetworkManager.Instance.OnNetworkDisconnected += ShowDisconnectPanel;
        }

        private void Start()
        {
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
                    disconnectReasonText.text = "Error: La conexión tardó demasiado. (Timeout)";
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