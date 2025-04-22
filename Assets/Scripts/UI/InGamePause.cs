using Managers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void ClosePauseAndResumeMovement()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            MyPlayerManager.Instance.TogglePlayerControls();
        }

        public void ClosePauseMenu()
        {
            ClosePauseAndResumeMovement();
        }

        public void GoToMenu()
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(1);
        }
    }
}