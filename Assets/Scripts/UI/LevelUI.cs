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

        private void Awake()
        {
            if (photonView.IsMine)
                Managers.GameManager.Instance.SetLevelUIInstance(this);
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
            if (teamID == 0)
                teamBlueScoreText.text = $"Azules: {score}";
            else
                teamRedScoreText.text = $"Rojos: {score}";
        }
    }
}