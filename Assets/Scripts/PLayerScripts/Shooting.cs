using Enums;
using Interfaces;
using Managers;
using Photon.Pun;
using UnityEngine;

namespace PLayerScripts.WeaponScripts
{
    public class Shooting : MonoBehaviourPun, ITeam
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private SnowBallStats data;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private TeamColor team;
        public TeamColor Team => team;
        
        private float nextFireTime = 0f;
        
        void Awake()
        {
            if (photonView.Owner.CustomProperties.TryGetValue("TeamColor", out var raw) 
                && raw is TeamColor tc)
            {
                team = tc;
            }
        }
        
        void Start()
        {
            if (photonView.IsMine)
                MyPlayerManager.Instance.SetPlayerShootingInstance(this);
        }
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
            Vector3 totalForce = spawnPoint.forward * data.SnowballSpeed + playerRb.velocity * data.VelocityInfluence;
            GameObject ball =PhotonNetwork.Instantiate(
                data.SnowballPrefab.name,
                spawnPoint.position,
                spawnPoint.rotation,
                0,
                new object[] { totalForce }
            );
        }
    }
}