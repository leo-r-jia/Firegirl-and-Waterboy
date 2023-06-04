using UnityEngine.Audio;
using UnityEngine;

//Class for storing a sound
[System.Serializable]
public class MusicTrack
{
    public string name;

    public AudioClip clip;

    [Range(0f, .1f)]
    public float volume = .05f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
