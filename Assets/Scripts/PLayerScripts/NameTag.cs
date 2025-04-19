using System;
using Cinemachine;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace PLayerScripts
{
    public class NameTag : MonoBehaviourPun
    {
        [SerializeField] private TextMeshPro textMesh;  
        [SerializeField] private Camera camera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private string playerName = "";
        private CinemachinePOV povComponent;

        public void SetName(string playerName)
        {
            this.playerName = playerName;
            photonView.RPC("SetNameRPC", RpcTarget.AllBuffered, this.playerName);
        }

        private void Start()
        {
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        }

        private void Update()
        {
            float yaw = povComponent.m_HorizontalAxis.Value;
            textMesh.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
        [PunRPC]
        public void SetNameRPC(string name)
        {
            textMesh.text = name;
        }
    }
}