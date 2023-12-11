using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : Singleton<AudioController>
{
    [Header("Main Settings:")]
    [Range(0, 1)]
    public float musicVolume = 0.0f;
    /// the sound fx volume
    [Range(0, 1)]
    public float sfxVolume = 1f;

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game sounds and musics: ")]
    public AudioClip jump;
    public AudioClip gotCollectable;
    public AudioClip gameover;
    public AudioClip[] backgroundMusics;

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clips">Array of sounds</param>
    /// <param name="aus">Audio Source</param>
    /// 

    //public void SetMusicVolume()
    //{
    //    musicVolume = musicSlider.value;
    //    musicAus.volume = musicVolume;
    //}
    
    public void PlaySound(AudioClip[] clips, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clips != null && clips.Length > 0 && aus)
        {
            var randomIdx = Random.Range(0, clips.Length);
            if (PlayerPrefs.HasKey("sfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            else
                sfxVolume = 0.7f;
            aus.PlayOneShot(clips[randomIdx], sfxVolume);
        }
    }

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    /// <param name="clip">Sounds</param>
    /// <param name="aus">Audio Source</param>
    public void PlaySound(AudioClip clip, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }

        if (clip != null && aus)
        {
            if (PlayerPrefs.HasKey("sfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            else
                sfxVolume = 0.7f;
            aus.PlayOneShot(clip, sfxVolume);
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="musics">Array of musics</param>
    /// <param name="loop">Can Loop</param>
    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus && musics != null && musics.Length > 0)
        {
            var randomIdx = Random.Range(0, musics.Length);

            musicAus.clip = musics[randomIdx];
            musicAus.loop = loop;
            if (PlayerPrefs.HasKey("musicVolume"))
                musicAus.volume = PlayerPrefs.GetFloat("musicVolume");
            else
                musicAus.volume = 0.7f;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Play Music
    /// </summary>
    /// <param name="music">music</param>
    /// <param name="canLoop">Can Loop</param>
    public void PlayMusic(AudioClip music, bool canLoop)
    {
        if (musicAus && music != null)
        {
            musicAus.clip = music;
            musicAus.loop = canLoop;
            if (PlayerPrefs.HasKey("musicVolume"))
                musicAus.volume = PlayerPrefs.GetFloat("musicVolume");
            else
                musicAus.volume = 0.7f;
            musicAus.Play();
        }
    }

    /// <summary>
    /// Set volume for audiosource
    /// </summary>
    /// <param name="vol">New Volume</param>
    //public void SetMusicVolume(float vol)
    //{
    //    if (musicAus) musicAus.volume = vol;
    //}

    /// <summary>
    /// Stop play music or sound effect
    /// </summary>
    public void StopPlayMusic()
    {
        if (musicAus) musicAus.Stop();
    }

    public void PlayBackgroundMusic()
    {
        PlayMusic(backgroundMusics, true);
    }
}
