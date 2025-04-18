using Photon.Pun;
using UnityEngine;

namespace PLayerScripts.WeaponScripts
{
    public class SnowballProjectile : MonoBehaviourPun
    {
        [SerializeField] private SnowBallStats data;
        [SerializeField] private Rigidbody rb;

        void Awake()
        {
            object[] initData = photonView.InstantiationData;
            if (initData != null && initData.Length > 0)
            {
                Vector3 force = (Vector3)initData[0];
                rb.AddForce(force, ForceMode.Impulse);
            }

            Destroy(gameObject, data.LifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == data.PlayerLayerIndex)
            {
                PhotonView targetPv = other.GetComponent<PhotonView>();
                if (targetPv != null)
                {
                    Vector3 dir = (other.transform.position - transform.position).normalized;
                    Vector3 knockForce = dir * data.KnockbackForce;

                    targetPv.RPC(
                        nameof(Movement.ApplyKnockback),
                        targetPv.Owner,
                        knockForce
                    );
                }
            }

            if (photonView.IsMine)
            {
                PhotonNetwork.Instantiate(data.IceParticlesPrefab.name, transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}