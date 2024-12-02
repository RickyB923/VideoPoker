using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] public AudioSource musicSource, effectSource;
    [SerializeField] public AudioClip[] musicTracks;
    public void PlaySoundEffect(AudioClip clip)
    {
        effectSource.volume = 1f;
        effectSource.PlayOneShot(clip);
    }
    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        effectSource.volume = volume;
        effectSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}