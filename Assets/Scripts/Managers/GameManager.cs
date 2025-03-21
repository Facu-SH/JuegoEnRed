using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        private string playerName;
        private string roomCode;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public void SetNameAndRoomCode(string playerName, string roomCode)
        {
            this.playerName = playerName;
            this.roomCode = roomCode;
        }
        public KeyValuePair<string,string> GetNameAndRoomCode()
        {
            return new KeyValuePair<string,string>(playerName, roomCode);
        }
    }
}