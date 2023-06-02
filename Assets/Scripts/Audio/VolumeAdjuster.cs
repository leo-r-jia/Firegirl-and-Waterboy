using UnityEngine;
using UnityEngine.UI;

//Set volumes in AudioManager from sliders on value changed
public class VolumeAdjuster : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;

    public void SetSFXVolume()
    {
        AudioManager.Instance.SFXVolume = sfxSlider.normalizedValue;
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.MusicVolume = musicSlider.normalizedValue;
    }
}