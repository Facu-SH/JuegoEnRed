using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerJoinLeaveMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private float lifeTime = 3f;

        public void SetMessage(string playerName, bool joined)
        {
            messageText.text = $"{playerName} {(joined ? "joined the game" : "left the game")}";
            Destroy(gameObject, lifeTime);
        }
    }
}