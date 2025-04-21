using Managers;
using Photon.Pun;
using PLayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGamePause : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private Movement playerMovement;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePauseAndResumeMovement();
            }
        }
        public void ClosePauseMenu()
        {
            ClosePauseAndResumeMovement();
        }
        
        public void GoToMenu()
        {
            SceneManager.LoadScene(1);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        private void ClosePauseAndResumeMovement()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            MyPlayerManager.Instance.TogglePlayerControls();
        }
    }
}