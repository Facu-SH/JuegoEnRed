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
            if (!photonView.IsMine) return;

            if (other.gameObject.layer == data.PlayerLayerIndex)
            {
                if (other.TryGetComponent<PhotonView>(out var targetPv))
                {
                    var dir = (other.transform.position - transform.position).normalized;
                    var knockForce = dir * data.KnockbackForce;
                    targetPv.RPC(nameof(Movement.RPC_ApplyKnockback), targetPv.Owner, knockForce);
                }

                if (other.TryGetComponent<ITeam>(out var otherTeam) && otherTeam.Team != team
                                                                    && other.TryGetComponent<IDamageable>(
                                                                        out var damageable))
                {
                    damageable.GetDamage(data.Damage);
                }
            }

            PhotonNetwork.Instantiate(data.IceParticlesPrefab.name, transform.position, Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}