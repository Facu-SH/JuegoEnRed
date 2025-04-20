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
            var inst = photonView.InstantiationData;
            if (inst != null && inst.Length > 0 && inst[0] is int idx)
                team = (TeamColor)idx;
            
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
            PhotonNetwork.Instantiate(
                data.SnowballPrefab.name,
                spawnPoint.position,
                spawnPoint.rotation,
                0,
                new object[] { totalForce, team }
            );
        }
    }
}