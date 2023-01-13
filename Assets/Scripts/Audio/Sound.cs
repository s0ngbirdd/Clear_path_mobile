using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // Public
    public string name;
    public AudioClip clip;
    public bool playOnAwake;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    [HideInInspector]
    public AudioSource source;
}
