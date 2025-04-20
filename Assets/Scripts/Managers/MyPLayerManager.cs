using Enums;
using PLayerScripts;
using PLayerScripts.WeaponScripts;
using UnityEngine;

namespace Managers
{
    public class MyPlayerManager : MonoBehaviour
    {
        public static MyPlayerManager Instance { get; private set; }

        private Shooting playerShooting;
        private Movement playerMovement;
        public TeamColor Team => playerShooting != null
            ? playerShooting.Team
            : default;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void SetPlayerShootingInstance(Shooting shooting)
        {
            playerShooting = shooting;
        }
        public void SetPlayerMovementInstance(Movement movement)
        {
            playerMovement = movement;
        }
        
        public void TogglePlayerControls()
        {
            playerMovement.enabled = !playerMovement.enabled;
            if (playerShooting != null)
                playerShooting.enabled = !playerShooting.enabled;
        }
    }
}