using UnityEngine;

namespace PLayerScripts
{
    [CreateAssetMenu(fileName = "newBasePLayerStats",menuName = "PLayer/BasePLayerStats")]
    public class BasePLayerStats : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float mouseSensitivity;
        
        public float Speed => speed;
        public float MaxVelocity => maxVelocity;
        public float MouseSensitivity => mouseSensitivity;
    }
}