using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    // Public
    public static AudioSystem Instance;

    // Serialize
    [SerializeField] private Sound[] _sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
        }
    }

    private void Update()
    {
        if (!ReturnAudioSource("GameLoop").isPlaying)
        {
            Play("GameLoop");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public AudioSource ReturnAudioSource(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        return s.source;
    }
}
