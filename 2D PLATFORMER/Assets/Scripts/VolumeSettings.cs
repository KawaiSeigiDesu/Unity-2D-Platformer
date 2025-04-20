using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start() {
        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        }
        else {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume() {
        float volume = musicSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}
