using Photon.Pun;
using UnityEngine;

namespace PLayerScripts
{
    public class PlayerMaterialHandler : MonoBehaviourPun
    {
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private BasePLayerStats data;
        private Material newMaterial;

        private void Awake()
        {
            var inst = photonView.InstantiationData;
            if (inst != null && inst.Length > 0 && inst[0] is int idx)
            {
                if (photonView.IsMine) photonView.RPC(nameof(RPC_SetTeamMaterial), RpcTarget.AllBuffered, idx);
            }
        }

        [PunRPC]
        private void RPC_SetTeamMaterial(int teamIdx)
        {
            if (teamIdx < 0 || teamIdx >= data.TeamMaterials.Length) return;
            //meshRenderer.material = data.TeamMaterials[teamIdx];
            newMaterial = data.TeamMaterials[teamIdx];
            ReplaceSpecificMaterial(newMaterial, 1);
        }
        public void ReplaceSpecificMaterial(Material material, int index)
        {
            Material[] materials = meshRenderer.materials;
            materials[index] = material;
            meshRenderer.materials = materials;
        }
    }
}