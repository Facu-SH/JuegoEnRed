using Photon.Pun;
using UnityEngine;

namespace PLayerScripts.WeaponScripts
{
    public class Shooting : MonoBehaviourPun
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private SnowBallStats data;
        [SerializeField] private Rigidbody playerRb;
        
        private float nextFireTime = 0f;

        void Update()
        {
            if (!photonView.IsMine) return;
            
            if (Time.time >= nextFireTime && Input.GetButton("Fire1"))
            {
                nextFireTime = Time.time + data.FireRate;
                Shoot();
            }
        }
        void Shoot()
        {
            Vector3 dir = spawnPoint.forward;
            Debug.DrawRay(spawnPoint.position, dir * 2f, Color.red, 2f);
            Vector3 totalForce = spawnPoint.forward * data.SnowballSpeed + playerRb.velocity * data.VelocityInfluence;
            PhotonNetwork.Instantiate(
                data.SnowballPrefab.name,
                spawnPoint.position,
                spawnPoint.rotation,
                0,
                new object[] { totalForce }
            );
        }
    }
}