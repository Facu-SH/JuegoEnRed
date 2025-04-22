using Managers;
using Photon.Pun;
using UnityEngine;

namespace UI
{
    public class InGamePause : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
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
            PhotonNetwork.LeaveRoom();
        }
        private void ClosePauseAndResumeMovement()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            MyPlayerManager.Instance.TogglePlayerControls();
        }
    }
}