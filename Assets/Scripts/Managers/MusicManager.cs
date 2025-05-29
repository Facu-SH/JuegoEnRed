using Music;
using UnityEngine;

namespace Managers
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private MusicData data;
        public static MusicManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            m_audioSource.volume = PlayerPrefs.GetFloat("MusicAudio", 0.45f);
        }

        public void PlayMenuMusic()
        {
            if (data.MenuMusic == null) return;
            
            m_audioSource.clip = data.MenuMusic;
            m_audioSource.Play();
        }
        public void PlayGameMusic()
        {
            if (data.GameMusic == null) return;
            
            m_audioSource.clip = data.GameMusic;
            m_audioSource.Play();
        }
        public void PlayEndMusic()
        {
            if (data.EndMusic == null) return;
            
            m_audioSource.clip = data.EndMusic;
            m_audioSource.Play();
        }

        public void StopMusic()
        {
            m_audioSource.Pause();
        }

        public void PlayMusic()
        {
            m_audioSource.Play();
        }

        public AudioSource GetAudioSource()
        {
            return m_audioSource;
        }
    }
}