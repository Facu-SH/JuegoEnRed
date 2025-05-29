using UnityEngine;

namespace Music
{
    [CreateAssetMenu(fileName = "newMusicData", menuName = "Music/MusicData")]
    public class MusicData : ScriptableObject
    {
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioClip gameMusic;
        [SerializeField] private AudioClip endMusic;

        public AudioClip MenuMusic => menuMusic;
        public AudioClip GameMusic => gameMusic;
        public AudioClip EndMusic => endMusic;
    }
}