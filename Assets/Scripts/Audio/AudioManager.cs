using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public bool SFXIsMuted { get; private set; }
    private float _sfxVolume = 1;
    public float SFXVolume
    {
        get => _sfxVolume;
        set
        {
            if (value >= 0 && value <= 1)
            {
                _sfxVolume = value;
                AdjustSFXVolumes();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Volume must be between 0 and 1");
            }
        }
    }

    public bool MusicIsMuted { get; private set; }
    private float _musicVolume = 1;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            if (value >= 0 && value <= 1)
            {
                _musicVolume = value;
                AdjustMusicVolume();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Volume must be between 0 and 1");
            }
        }
    }

    //Declare sole instance of AudioManager
    public static AudioManager Instance;

    void Awake()
    {
        SetupInstance();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        SFXIsMuted = false;
        MusicIsMuted = false;
    }

    void AdjustSFXVolumes()
    {
        foreach (Sound s in sounds)
        {
            if (!s.isMusic)
            {
                s.volume = SFXVolume;
                s.source.volume = SFXVolume;
            }
        }
    }

    void AdjustMusicVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.volume = MusicVolume;
                s.source.volume = MusicVolume;
            }
        }
    }

    public void ToggleMuteSFX()
    {
        SFXIsMuted = !SFXIsMuted;

        foreach (Sound s in sounds)
        {
            if (!s.isMusic)
            {
                s.source.mute = SFXIsMuted;
            }
        }
    }

    public void ToggleMuteMusic()
    {
        MusicIsMuted = !MusicIsMuted;

        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.source.mute = MusicIsMuted;
            }
        }
    }

    #region Scene persistence
    void SetupInstance()
    {
        //After first launch, destroy additional instances of PlayerData
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    //Play a specified sound
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s is null)
        {
            Debug.Log("Sound \"" + name + "\" was not found!");
            return;
        }

        s.source.Play();
    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound is null) Debug.Log("Sound: " + name);

        return sound;
    }
}