using Interfaces;
using Photon.Pun;
using UnityEngine;

namespace PLayerScripts
{
    public class HealthController : MonoBehaviourPun, IDamageable
    {
        [SerializeField] private BasePLayerStats data;
        private int health;

        private void OnEnable()
        {
            health = data.MaxHealth;
        }
        public void GetDamage(int damage)
        {
            photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.AllBuffered, damage);
        }

        [PunRPC]
        private void RPC_TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                Die();
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}