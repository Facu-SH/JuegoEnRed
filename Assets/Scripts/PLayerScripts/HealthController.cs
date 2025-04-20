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

        private void Start()
        {
            GameManager.Instance.OnPlayerDamage(health);
            team = MyPlayerManager.Instance.Team;
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
                photonView.RPC(nameof(RPC_NotifyDeath), RpcTarget.All, (int)MyPlayerManager.Instance.Team);
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