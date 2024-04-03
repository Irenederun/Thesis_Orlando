using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioLibrary audioLibrary;
    public static AudioManager instance;
    public List<AudioSource> backupAudioSource;
    private int backupNo;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private AudioClip GetAudio(string audioName)
    {
        return audioLibrary.GetAudio(audioName);
    }

    private float GetVolume(string audioName)
    {
        return audioLibrary.GetVolume(audioName);
    }

    public void PlayAudio(string audioName, AudioSource aSource)
    {
        if (!aSource.isPlaying)
        {
            aSource.PlayOneShot(GetAudio(audioName), GetVolume(audioName));
        }
        else
        {
            backupAudioSource[backupNo].PlayOneShot(GetAudio(audioName), GetVolume(audioName));
            backupNo++;
            if (backupNo == backupAudioSource.Count) backupNo = 0;
        }
    }
}
