using System.Collections;
using System.Collections.Generic;
using Enums;
using PLayerScripts;
using PLayerScripts.WeaponScripts;
using UnityEngine;
using UnityEngine.Serialization;

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

        [SerializeField] private List<Transform> playerSpawnPoints;
        private GameObject deadMessage;

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
            SetColorAndTpPlayer();
        }

        private void SetColorAndTpPlayer()
        {
            playerShooting.gameObject.transform.position = playerSpawnPoints[(int)Team].position;
        }
        public void SetPlayerMovementInstance(Movement movement)
        {
            playerMovement = movement;
        }

        public void SetDeadMessageInstance(GameObject deadMessage)
        {
            this.deadMessage = deadMessage;
        }
        
        public void TogglePlayerControls()
        {
            if (playerShooting != null) playerMovement.enabled = !playerMovement.enabled;
            
            if (playerShooting != null) playerShooting.enabled = !playerShooting.enabled;
        }
        public void HandleDeath(GameObject playerGO)
        {
            StartCoroutine(DeathAndRespawnRoutine(playerGO));
        }
        private IEnumerator DeathAndRespawnRoutine(GameObject playerGO)
        {
            playerGO.SetActive(false);
            deadMessage.SetActive(true);
            
            yield return new WaitForSeconds(10f);
            
            playerGO.transform.position = playerSpawnPoints[(int)Team].position;
            
            deadMessage.SetActive(false);
            playerGO.SetActive(true);
        }
    }
}