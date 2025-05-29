using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Sliders
{
    public class SoundSlider : MonoBehaviour
    {
        public Slider slider;
        public float sliderValue;
        //public Image mute;
        private new AudioSource audio;

        void Start()
        {
            audio = AudioManager.Instance.GetAudioSource();
            // se mantiene la posicion del slider
            slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.45f);
            audio.volume = slider.value;
        }

        public void ChangeVolumen(float value)
        {
            sliderValue= value;
            PlayerPrefs.SetFloat("volumenAudio", sliderValue);
            audio.volume = slider.value;
        }
    }
}