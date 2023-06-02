using UnityEngine;

//Set volumes in AudioManager from sliders on value changed
public class VolumeAdjuster : MonoBehaviour
{
    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SFXVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.MusicVolume = volume;
    }
}