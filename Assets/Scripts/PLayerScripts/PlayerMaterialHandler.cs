using Photon.Pun;
using UnityEngine;

namespace PLayerScripts
{
    public class PlayerMaterialHandler : MonoBehaviourPun
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private BasePLayerStats data;


        private void Awake()
        {
            var inst = photonView.InstantiationData;
            if (inst != null && inst.Length > 0 && inst[0] is int idx)
            {
                if (photonView.IsMine)
                    photonView.RPC(
                        nameof(RPC_SetTeamMaterial),
                        RpcTarget.AllBuffered,
                        idx
                    );
            }
        }

        [PunRPC]
        private void RPC_SetTeamMaterial(int teamIdx)
        {
            if (teamIdx < 0 || teamIdx >= data.TeamMaterials.Length) return;
            meshRenderer.material = data.TeamMaterials[teamIdx];
        }
    }

}