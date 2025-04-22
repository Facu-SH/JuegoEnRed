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

        [SerializeField] private List<Transform> playerSpawnPoints;
        private GameObject deadMessage;
        private bool isEnd;
        public int Team { get; private set; }

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
            isEnd = false;
            playerShooting = shooting;
            Team = (int)shooting.Team;
            SetColorAndTpPlayer();
        }

        private void SetColorAndTpPlayer()
        {
            playerShooting.gameObject.transform.position = playerSpawnPoints[Team].position;
        }
        public void SetPlayerMovementInstance(Movement movement)
        {
            playerMovement = movement;
        }

        public void SetEndedGame()
        {
            isEnd = true;
        }

        public void SetDeadMessageInstance(GameObject deadMessage)
        {
            this.deadMessage = deadMessage;
        }
        
        public void TogglePlayerControls()
        {
            if (playerMovement != null) playerMovement.enabled = !playerMovement.enabled;
            
            if (playerShooting != null) playerShooting.enabled = !playerShooting.enabled;
        }

        private void DeactivatePlayerControls()
        {
            playerMovement.enabled = false;
            playerMovement = null;
            playerShooting.enabled = false;
            playerShooting = null;
        }
        public void HandleDeath(GameObject playerGO, bool playerDead)
        {
            StartCoroutine(DeathAndRespawnRoutine(playerGO, playerDead));
        }
        private IEnumerator DeathAndRespawnRoutine(GameObject playerGO, bool playerDead)
        {
            playerGO.SetActive(false);
            if(playerDead) deadMessage.SetActive(true);
            
            yield return new WaitForSeconds(10f);
            
            playerGO.transform.position = playerSpawnPoints[Team].position;
            
            if(playerDead) deadMessage.SetActive(false);
            playerGO.SetActive(true);
            if (isEnd)
            {
                DeactivatePlayerControls();
            }
        }
    }
}