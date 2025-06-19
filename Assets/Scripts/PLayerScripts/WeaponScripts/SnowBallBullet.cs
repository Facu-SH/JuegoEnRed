using System.Collections.Generic;
using Enums;
using Interfaces;
using Managers;
using Photon.Pun;
using UnityEngine;

namespace PLayerScripts.WeaponScripts
{
    public class SnowBallBullet : MonoBehaviourPun
    {
        [SerializeField] private SnowBallStats data;
        [SerializeField] private Rigidbody rb;
        private TeamColor team;
        private bool shouldCollide = true;

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
            if (!shouldCollide || !photonView.IsMine)
                return;
            
            shouldCollide = false;

            PhotonNetwork.Instantiate(
                data.IceParticlesPrefab.name,
                transform.position,
                Quaternion.identity
            );
            
            PhotonNetwork.Destroy(gameObject);
            
            int mask = 1 << data.PlayerLayerIndex;
            Collider[] hits = Physics.OverlapSphere(transform.position, data.SplashRadius, mask);

            // 3) Dedupe por gameObject antes de aplicar impacto
            var processed = new HashSet<GameObject>();
            foreach (var hit in hits)
            {
                var go = hit.transform.root.gameObject; 
                // o hit.gameObject si todos tus colliders están en el root
                if (processed.Add(go))
                {
                    ApplyImpact(hit);
                }
            }
        }


        private void ApplyImpact(Collider col)
        {
            if (!col.TryGetComponent<IDamageable>(out var dmg)) return;

            // RPC de knockback
            if (col.TryGetComponent<PhotonView>(out var targetPv))
            {
                Vector3 dir = (col.transform.position - transform.position).normalized;
                targetPv.RPC(
                    nameof(Movement.RPC_ApplyKnockback),
                    targetPv.Owner,
                    dir * data.KnockbackForce
                );
            }

            // Daño
            if (col.TryGetComponent<ITeam>(out var otherTeam) && otherTeam.Team != team) dmg.GetDamage(data.Damage);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, data.SplashRadius);
        }

        private void OnDestroy()
        {
            AudioManager.Instance.PlaySound(data.ExplodeSound);
        }

        private void OnEnable()
        {
            AudioManager.Instance.PlaySound(data.ShootingSound);
        }
    }
}