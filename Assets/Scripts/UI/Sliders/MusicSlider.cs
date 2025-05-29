using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Sliders
{
    public class MusicSlider : MonoBehaviour
    {
        public Slider slider;
        public float sliderValue;
        //public Image mute;
        private AudioSource music;

        void Start()
        {
            music = MusicManager.Instance.GetAudioSource();
            // se mantiene la posicion del slider
            slider.value = PlayerPrefs.GetFloat("MusicAudio", 0.45f);
            music.volume = slider.value;
        }

        public void ChangeVolumen(float value)
        {
            sliderValue= value;
            PlayerPrefs.SetFloat("MusicAudio", sliderValue);
            music.volume = slider.value;
        }
    
    }
}