using System;
using System.Collections.Generic;
using Managers;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelUI : MonoBehaviourPun
    {
        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Team Scores")]
        [SerializeField] private TextMeshProUGUI teamBlueScoreText;
        [SerializeField] private TextMeshProUGUI teamRedScoreText;
        [Header("Win/Lose")]
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        
        [SerializeField] private List<GameObject> UI;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                GameManager.Instance.SetLevelUIInstance(this);
                PhotonNetworkManager.Instance.OnJoinedRoomEvent += OnJoinedRoom;
            }
        }

        private void OnDestroy()
        {
            if (photonView.IsMine)
            {
                PhotonNetworkManager.Instance.OnJoinedRoomEvent -= OnJoinedRoom;
            }
        }

        public void ActiveEndCanvas(int winnerTeam)
        {
            var myTeam = MyPlayerManager.Instance.Team;
            if (myTeam == winnerTeam)
            {
                winPanel.SetActive(true);
            }
            else
            {
                losePanel.SetActive(true);
            }
        }

        public void SetTimer(float timeInSeconds)
        {
            int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
            int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }

        public void SetHealth(int currentHealth)
        {
            healthText.text = $"Health: {currentHealth}";
        }
        
        public void SetTeamScore(int teamID, int score)
        {
            if (teamID == 1)
                teamRedScoreText.text = $"Red Score: {score}";
            else
                teamBlueScoreText.text = $"Blue score: {score}";
        }

        private void OnJoinedRoom()
        {
            foreach (GameObject _gameObject in UI)
            {
                _gameObject.SetActive(true);
            }
        }
    }
}