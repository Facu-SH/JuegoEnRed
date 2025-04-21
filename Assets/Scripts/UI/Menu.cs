using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManager = Managers.GameManager;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject changeNameMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI roomCode;
        [SerializeField] private TMP_InputField playerNameInput;
        [SerializeField] private TMP_InputField roomCodeInput;
        public void GoToGameScene()
        {
            GameManager.Instance.SetNameAndRoomCode(playerNameInput.text, roomCodeInput.text);
            SceneManager.LoadScene(2);
        }

        public void OpenChangeNameMenu()
        {
            changeNameMenu.SetActive(true);
        }

        public void CloseChangeNameMenu()
        {
            changeNameMenu.SetActive(false);
        }
        public void OpenSettingsMenu()
        {
            settingsMenu.SetActive(true);
        }

        public void CloseSettingsMenu()
        {
            settingsMenu.SetActive(false);
        }
        public void ChangePlayerName()
        {
            playerName.text = playerNameInput.text;
            
        }
        public void ChangeRoomCode()
        {
            roomCode.text = roomCodeInput.text;
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}