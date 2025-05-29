using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        public static AudioManager Instance;

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
        }

        public void PlaySound(AudioClip p_Clip)
        {
            if (p_Clip is not null)
            {
                m_audioSource.PlayOneShot(p_Clip);
            }
        }
    
        public AudioSource GetAudioSource()
        {
            return m_audioSource;
        }
    }
}