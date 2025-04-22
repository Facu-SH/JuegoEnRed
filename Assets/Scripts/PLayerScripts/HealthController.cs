using System;
using Enums;
using Interfaces;
using Managers;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace PLayerScripts
{
    public class HealthController : MonoBehaviourPun, IDamageable
    {
        [SerializeField] private BasePLayerStats data;
        private TeamColor team;
        private int health;

        private void OnEnable()
        {
            health = data.MaxHealth;
            GameManager.Instance.OnPlayerDamage(health);
        }

        private void Awake()
        {
            var inst = photonView.InstantiationData;
            if (inst != null && inst.Length > 0 && inst[0] is int idx)
                team = (TeamColor)idx;
            
            GameManager.Instance.OnPlayerDamage(health);
        }

        public void GetDamage(int damage)
        {
            photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, damage);
        }
        
        [PunRPC]
        private void RPC_TakeDamage(int damage)
        {
            if (!photonView.IsMine) return;

            health -= damage;
            GameManager.Instance.OnPlayerDamage(health);

            if (health <= 0)
            {
                photonView.RPC(nameof(RPC_NotifyDeath), RpcTarget.All, (int)team);

                photonView.RPC(nameof(RPC_Despawn), RpcTarget.All);
            }
        }

        [PunRPC]
        private void RPC_Despawn()
        {
            MyPlayerManager.Instance.HandleDeath(gameObject);
        }
        
        [PunRPC]
        private void RPC_NotifyDeath(int deadTeamID)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            GameManager.Instance.OnPlayerDeath(deadTeamID);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == data.RespawnLayerIndex)
            {
                photonView.RPC(nameof(RPC_NotifyDeath), RpcTarget.All, (int)team);

                photonView.RPC(nameof(RPC_Despawn), RpcTarget.All);
            }
        }
    }
}