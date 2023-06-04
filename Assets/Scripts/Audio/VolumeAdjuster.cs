using UnityEngine;
using UnityEngine.UI;

//Set volumes in AudioManager from interactions with settings UI
public class VolumeAdjuster : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    [SerializeField] Button sfxMuteButton;
    [SerializeField] Button musicMuteButton;

    [SerializeField] Sprite mutedSprite;
    [SerializeField] Sprite unmutedSprite;
    [SerializeField] Sprite halfVolumeSprite;

    private void Start()
    {
        UpdateSFXSlider();
        UpdateMusicSlider();

        SetButtonImages();   
    }

    //Set the button images to the appropriate sprites
    void SetButtonImages()
    {
        if (AudioManager.Instance.SFXIsMuted)
            sfxMuteButton.GetComponent<Image>().sprite = mutedSprite;
        else if (AudioManager.Instance.SFXVolume < .5f)
            sfxMuteButton.GetComponent<Image>().sprite = halfVolumeSprite;
        else
            sfxMuteButton.GetComponent<Image>().sprite = unmutedSprite;

        if (AudioManager.Instance.MusicIsMuted)
            musicMuteButton.GetComponent<Image>().sprite = mutedSprite;
        else if (AudioManager.Instance.MusicVolume < .05f)
            musicMuteButton.GetComponent<Image>().sprite = halfVolumeSprite;
        else
            musicMuteButton.GetComponent<Image>().sprite = unmutedSprite;
    }

    public void SFXSliderValueChanged()
    {
        if (sfxSlider.value != 0 || !AudioManager.Instance.SFXIsMuted)
            AudioManager.Instance.SFXVolume = sfxSlider.value;

        if (sfxSlider.value == 0 && !AudioManager.Instance.SFXIsMuted || sfxSlider.value != 0 && AudioManager.Instance.SFXIsMuted)
        {
            AudioManager.Instance.ToggleMuteSFX();
        }

        SetButtonImages();
    }

    public void SFXMuteButtonPressed()
    {
        AudioManager.Instance.ToggleMuteSFX();

        UpdateSFXSlider();

        SetButtonImages();
    }

    //Update the slider to the correct position. Will call SFXSliderValueChanged()
    void UpdateSFXSlider()
    {
        if (AudioManager.Instance.SFXIsMuted)
            sfxSlider.value = 0;
        else
            sfxSlider.value = AudioManager.Instance.SFXVolume;
    }

    public void MusicSliderValueChanged()
    {
        if (musicSlider.value != 0 || !AudioManager.Instance.MusicIsMuted)
            AudioManager.Instance.MusicVolume = musicSlider.value;

        if (musicSlider.value == 0 && !AudioManager.Instance.MusicIsMuted || musicSlider.value != 0 && AudioManager.Instance.MusicIsMuted)
        {
            AudioManager.Instance.ToggleMuteMusic();
        }

        SetButtonImages();
    }

    public void MusicMuteButtonPressed()
    {
        AudioManager.Instance.ToggleMuteMusic();

        UpdateMusicSlider();

        SetButtonImages();
    }

    //Update the slider to the correct position. Will call MusicSliderValueChanged()
    void UpdateMusicSlider()
    {
        if (AudioManager.Instance.MusicIsMuted)
            musicSlider.value = 0;
        else
            musicSlider.value = AudioManager.Instance.MusicVolume;
    }
}