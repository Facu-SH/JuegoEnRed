using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class TimerPowerUp : MonoBehaviour
    {
        [Header("Duración Power-Up")]
        [SerializeField] private float cooldownTime;

        [Header("UI")]
        [SerializeField] private Image fill;

        private float timeRemaining;
        private bool isRunning;

        void Awake()
        {
            gameObject.SetActive(false);
        }

        void Update()
        {
            if (!isRunning) return;
            
            timeRemaining -= Time.deltaTime;
            fill.fillAmount = timeRemaining / cooldownTime;

            if (timeRemaining <= 0f)
            {
                DeactivatePowerUp();
            }
        }

        public void ActivatePowerUp()
        {
            timeRemaining = cooldownTime;
            fill.fillAmount = 1f;
            isRunning = true;
            gameObject.SetActive(true);
        }

        private void DeactivatePowerUp()
        {
            isRunning = false;
            gameObject.SetActive(false);
        }
    }
}