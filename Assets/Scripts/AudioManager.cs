using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager;
    public Sound[] sounds;
    public Sound[] musics;

    private void Awake()
    {
        if (audioManager == null)
            audioManager = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        foreach (Sound s in musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        if (LevelGenerator.levelGenerator)
            if (LevelGenerator.levelGenerator.soundOn)
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                if (s == null)
                    return;
                s.source.Play();
            }
            else
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                if (s == null)
                    return;
                s.source.Play();

            }
    }
    public void PlayMusic(string name)
    {
        if (LevelGenerator.levelGenerator.musicOn)
        { 
            Sound s = Array.Find(musics, sound => sound.name == name);
                if (s == null)
                    return;
                s.source.Play();
        }
    }

    public void StopMusic()
    {
        for (int i = 0; i < musics.Length; i++)
            musics[i].source.Pause();
    }
}
