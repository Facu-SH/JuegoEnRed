using Enums;
using Interfaces;
using Photon.Pun;
using UnityEngine;

namespace PLayerScripts.WeaponScripts
{
    public class SnowballProjectile : MonoBehaviourPun
    {
        [SerializeField] private SnowBallStats data;
        [SerializeField] private Rigidbody rb;
        private TeamColor team;

        void Awake()
        {
            object[] initData = photonView.InstantiationData;
            if (initData != null && initData.Length > 0)
            {
                Vector3 force = (Vector3)initData[0];
                team = (TeamColor)initData[1];
                rb.AddForce(force, ForceMode.Impulse);
            }

            Destroy(gameObject, data.LifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == data.PlayerLayerIndex)
            {
                if (other.TryGetComponent(out PhotonView targetPv))
                {
                    Vector3 dir = (other.transform.position - transform.position).normalized;
                    Vector3 knockForce = dir * data.KnockbackForce;

                    targetPv.RPC(
                        nameof(Movement.ApplyKnockback),
                        targetPv.Owner,
                        knockForce
                    );
                }

                if (other.TryGetComponent(out ITeam otherTeam))
                {
                    if (otherTeam.Team != team)
                    {
                        if (other.TryGetComponent(out IDamageable damageable))
                        {
                            damageable.GetDamage(data.Damage);
                        }
                    }
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