using System;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManager = Managers.GameManager;

namespace UI
{
    public class InGamePause : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
            }
        }

        public void ClosePauseMenu()
        {
            pauseMenu.SetActive(false);
        }
        
        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }
}