using System;
using Enums;
using Managers;
using Photon.Pun;
using UnityEngine;

namespace PLayerScripts
{
    public class HealthController : MonoBehaviourPun
    {
        [SerializeField] private BasePLayerStats data;
        [SerializeField] private TeamColor team;
        private int health;

        private void Awake()
        {
            if (photonView.Owner.CustomProperties.TryGetValue("TeamColor", out var raw) 
                && raw is TeamColor tc)
            {
                team = tc;
            }
        }

        private void OnEnable()
        {
            health = data.MaxHealth;
            GameManager.Instance.OnPlayerDamage(health);
        }

        private void Start()
        {
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