using PLayerScripts;
using UnityEngine;

namespace Managers
{
    public class MyPLayerManager : MonoBehaviour
    {
        public static MyPLayerManager Instance { get; private set; }
        
        private Movement playerMovement;
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
        public void TogglePlayerMovementActivation()
        {
            playerMovement.enabled = !playerMovement.enabled;
        }
    }
}