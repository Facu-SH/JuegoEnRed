using System;
using Cinemachine;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace PLayerScripts
{
    public class NameTag : MonoBehaviourPun
    {
        [SerializeField] private TextMeshPro textMesh; 
        private string playerName = "";
        private Camera cam;

        public void SetName(string playerName)
        {
            this.playerName = playerName;
            photonView.RPC("SetNameRPC", RpcTarget.AllBuffered, this.playerName);
        }

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (!photonView.IsMine)
            {
                Vector3 dir = transform.position - cam.transform.position;
                dir.y = 0;
                textMesh.transform.rotation = Quaternion.LookRotation(dir);
            }
        }
        [PunRPC]
        public void SetNameRPC(string name)
        {
            textMesh.text = name;
        }
    }
}