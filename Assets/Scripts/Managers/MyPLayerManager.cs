using PLayerScripts;
using PLayerScripts.WeaponScripts;
using UnityEngine;

namespace Managers
{
    public class MyPLayerManager : MonoBehaviour
    {
        public static MyPLayerManager Instance { get; private set; }
        
        private Movement playerMovement;
        private Shooting playerShooting;
        private string roomCode;
        
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

        public void SetPlayerMovementInstance(Movement _playerMovement)
        {
            playerMovement = _playerMovement;
        }
        public void SetPlayerShootingInstance(Shooting _shooting)
        {
            playerShooting = _shooting;
        }
        public void TogglePlayerMovementAndShootingActivation()
        {
            playerMovement.enabled = !playerMovement.enabled;
            playerShooting.enabled = !playerShooting.enabled;
        }
    }
}