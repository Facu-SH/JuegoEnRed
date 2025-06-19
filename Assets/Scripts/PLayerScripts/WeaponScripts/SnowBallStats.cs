using UnityEngine;

namespace PLayerScripts.WeaponScripts
{
    [CreateAssetMenu(fileName = "newSnowBallStats", menuName = "Bullets/SnowBallStats")]
    public class SnowBallStats : ScriptableObject
    {
        [SerializeField] private GameObject snowballPrefab;
        [SerializeField] private float snowballSpeed = 15f;
        [SerializeField] private float velocityInfluence = 0.3f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private int playerLayerIndex;
        [SerializeField] private float knockbackForce;
        [SerializeField] private int damage;
        [SerializeField] private AudioClip explodeSound;
        [SerializeField] private AudioClip shootingSound;
        
        [Header("Fire Rate")]
        [SerializeField] private float fireRate = 0.2f;
        
        [Header("Splash Effect")]
        [SerializeField] private float splashRadius = 3f;
        
        [Header("Particles")]
        [SerializeField] private GameObject iceParticlesPrefab;  
        
        public GameObject SnowballPrefab => snowballPrefab;
        public float SnowballSpeed => snowballSpeed;
        public float VelocityInfluence => velocityInfluence;
        public float LifeTime => lifeTime;
        public int PlayerLayerIndex => playerLayerIndex;
        public float KnockbackForce => knockbackForce;
        public int Damage => damage;
        public float FireRate => fireRate;
        public float SplashRadius => splashRadius;
        public GameObject IceParticlesPrefab => iceParticlesPrefab;
        public AudioClip ExplodeSound => explodeSound;
        public AudioClip ShootingSound => shootingSound;
    }
}