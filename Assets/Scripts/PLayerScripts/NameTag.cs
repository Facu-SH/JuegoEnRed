using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace PLayerScripts
{
    public class NameTag : MonoBehaviourPun
    {
        [SerializeField] private TextMeshPro textMesh;  
        [SerializeField] private Camera camera;
        private string playerName = "";

        private void Start()
        {
            if (photonView.IsMine)
            {
                playerName = PhotonNetwork.LocalPlayer.UserId;
                photonView.RPC("SetNameRPC", RpcTarget.AllBuffered, playerName);
            }
        }

        private void Update()
        {
            textMesh.transform.forward = camera.transform.forward;
        }
        [PunRPC]
        public void SetNameRPC(string name)
        {
            textMesh.text = name;
        }
    }
}