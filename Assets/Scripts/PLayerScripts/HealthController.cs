using System;
using Enums;
using Managers;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace PLayerScripts
{
    public class HealthController : MonoBehaviourPun
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
            health -= damage;
            GameManager.Instance.OnPlayerDamage(health);

            if (health <= 0)
            {
                photonView.RPC(nameof(RPC_NotifyDeath), RpcTarget.All, (int)team);
                Die();
            }
        }

        [PunRPC]
        private void RPC_NotifyDeath(int deadTeamID)
        {
            GameManager.Instance.OnPlayerDeath(deadTeamID);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}