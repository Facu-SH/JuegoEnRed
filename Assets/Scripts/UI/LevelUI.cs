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
        [SerializeField] private TextMeshProUGUI myTeamText;

        [Header("Team Scores")] 
        [SerializeField] private TextMeshProUGUI teamBlueScoreText;
        [SerializeField] private TextMeshProUGUI teamRedScoreText;

        [Header("Win/Lose")]
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private TextMeshProUGUI redWinText;
        [SerializeField] private TextMeshProUGUI blueWinText;
        [SerializeField] private TextMeshProUGUI redLoseText;
        [SerializeField] private TextMeshProUGUI blueLoseText;

        [Header("Start UI Elements List")] [SerializeField]
        private List<GameObject> UI;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                GameManager.Instance.SetLevelUIInstance(this);
                MyPlayerManager.Instance.SetLevelUIInstance(this);
            }
        }

        public void ActivateUI(int team)
        {
            foreach (GameObject _gameObject in UI)
            {
                _gameObject.SetActive(true);
            }
            
            if (team == 1)
            {
                myTeamText.text = $"Team Red";
                myTeamText.color = Color.red;
            }
            else
                myTeamText.text = $"Team Blue";
        }

        public void ActiveEndCanvas(int winnerTeam)
        {
            var myTeam = MyPlayerManager.Instance.Team;
            if (myTeam == winnerTeam)
            {
                redWinText.text = teamRedScoreText.text;
                blueWinText.text = teamBlueScoreText.text;
                winPanel.SetActive(true);
            }
            else
            {
                redLoseText.text = teamRedScoreText.text;
                blueLoseText.text = teamBlueScoreText.text;
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
    }
}