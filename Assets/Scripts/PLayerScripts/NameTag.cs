using Photon.Pun;
using TMPro;
using UnityEngine;

namespace PLayerScripts
{
    public class NameTag : MonoBehaviourPun
    {
        [SerializeField] private TextMeshPro textMesh;
        private string playerName = "";
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (!photonView.IsMine)
            {
                if (cam == null) return;
                Vector3 dir = transform.position - cam.transform.position;
                dir.y = 0;
                textMesh.transform.rotation = Quaternion.LookRotation(dir);
            }
        }

        public void SetName(string playerName)
        {
            this.playerName = playerName;
            photonView.RPC(nameof(RPC_SetNameRPC), RpcTarget.AllBuffered, this.playerName);
        }

        [PunRPC]
        public void RPC_SetNameRPC(string name)
        {
            textMesh.text = name;
        }
    }
}