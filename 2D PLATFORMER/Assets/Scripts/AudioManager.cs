using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip background;
    public AudioClip backgroundMainMenu;
    public AudioClip checkpoint;
    public AudioClip death;
    public AudioClip portalIn;
    public AudioClip portalOut;
    public AudioClip wallTouch;
    public AudioClip buttonClicked;

    private void Start() {
        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayButtonClickedSFX() {
        SFXSource.PlayOneShot(buttonClicked);
    }
}
