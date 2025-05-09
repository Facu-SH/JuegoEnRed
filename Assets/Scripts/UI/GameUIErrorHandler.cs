﻿using Managers;
using UnityEngine;
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
            PhotonNetworkManager.Instance.OnJoinRoomFailedHandler += ShowDisconnectPanel;
        }

        private void Start()
        {
            MyPlayerManager.Instance.SetDeadMessageInstance(deadMessage);
        }

        private void OnDestroy()
        {
            PhotonNetworkManager.Instance.OnNetworkDisconnected -= ShowDisconnectPanel;
            PhotonNetworkManager.Instance.OnJoinRoomFailedHandler -= ShowDisconnectPanel;
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

        private void ShowDisconnectPanel(short returnCode, string message)
        {
            disconnectReasonText.text = $"Desconectado: {message}";
            disconnectPanel.SetActive(true);
        }
    }
}