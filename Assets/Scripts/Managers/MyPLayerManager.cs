using System.Collections;
using System.Collections.Generic;
using PLayerScripts;
using PLayerScripts.WeaponScripts;
using UI;
using UnityEngine;

namespace Managers
{
    public class MyPlayerManager : MonoBehaviour
    {
        public static MyPlayerManager Instance { get; private set; }

        [SerializeField] private List<Transform> playerSpawnPoints;
        [SerializeField] private LevelVariables data;

        private Shooting playerShooting;
        private Movement playerMovement;
        private LevelUI levelUI;
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
            levelUI.ActivateUI(Team);
            GameManager.Instance.shouldWait = false;
        }

        private void SetColorAndTpPlayer()
        {
            playerShooting.gameObject.transform.position = playerSpawnPoints[Team].position;
        }

        public void SetPlayerMovementInstance(Movement movement)
        {
            playerMovement = movement;
        }
        
        public void SetLevelUIInstance(LevelUI _levelUI)
        {
            levelUI = _levelUI;
        }

        public void SetEndedGame()
        {
            DeactivatePlayerControls();
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
            if (playerMovement != null) playerMovement.enabled = false;
            playerMovement = null;
            
            if (playerShooting != null) playerShooting.enabled = false;
            playerShooting = null;
        }

        public void HandleDeath(GameObject playerGO, bool playerDead)
        {
            StartCoroutine(DeathAndRespawnRoutine(playerGO, playerDead));
        }

        private IEnumerator DeathAndRespawnRoutine(GameObject playerGO, bool playerDead)
        {
            if(playerGO != null) playerGO.SetActive(false);
            if (playerDead) deadMessage.SetActive(true);

            yield return new WaitForSeconds(data.TimeToRespawn);

            if(playerGO != null) playerGO.transform.position = playerSpawnPoints[Team].position;

            if (playerDead) deadMessage.SetActive(false);
            if(playerGO != null) playerGO.SetActive(true);
            if (isEnd)
            {
                DeactivatePlayerControls();
            }
        }
    }
}