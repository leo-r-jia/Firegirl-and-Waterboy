using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private MusicTrack[] musicTracks;

    public bool SFXIsMuted { get; private set; }
    private float _sfxVolume = 1f;
    public float SFXVolume
    {
        get => _sfxVolume;
        set
        {
            if (value >= 0f && value <= 1f)
            {
                _sfxVolume = value;
                AdjustSFXVolumes();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Volume for sounds must be between 0 and 1");
            }
        }
    }

    public bool MusicIsMuted { get; private set; }
    private float _musicVolume = .05f;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            if (value >= 0f && value <= .1f)
            {
                _musicVolume = value;
                AdjustMusicVolume();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Volume for music must be between 0 and 0.1");
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
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }

        foreach (MusicTrack m in musicTracks)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
            m.source.volume = m.volume;
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
            s.volume = SFXVolume;
            s.source.volume = SFXVolume;
        }
    }

    void AdjustMusicVolume()
    {
        foreach (MusicTrack m in musicTracks)
        {
            m.volume = MusicVolume;
            m.source.volume = MusicVolume;
        }
    }

    public void ToggleMuteSFX()
    {
        SFXIsMuted = !SFXIsMuted;

        foreach (Sound s in sounds)
        {
            s.source.mute = SFXIsMuted;
        }
    }

    public void ToggleMuteMusic()
    {
        MusicIsMuted = !MusicIsMuted;

        foreach (MusicTrack m in musicTracks)
        {
            m.source.mute = MusicIsMuted;
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

    //Play a specified sound effect
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s is null)
        {
            Debug.Log("Sound effect \"" + name + "\" was not found!");
            return;
        }

        s.source.Play();
    }

    //Play a music track and stop other playing music tracks
    public void PlayMusic(string name)
    {
        MusicTrack m = Array.Find(musicTracks, music => music.name == name);

        if (m is null)
        {
            Debug.Log("Music track \"" + name + "\" was not found!");
            return;
        }

        foreach(MusicTrack music in musicTracks)
        {
            if (music.source.isPlaying)
                music.source.Stop();
        }

        m.source.Play();
    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if (sound is null) Debug.Log("Sound: " + name);

        return sound;
    }
}