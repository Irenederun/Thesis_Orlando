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
    //public PlayAmbienceAudio playAmbScript;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //playAmbScript = GetComponent<PlayAmbienceAudio>();
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
    
    public void PlayAudioLoop(string audioName, AudioSource aSource)
    {
        if (!aSource.isPlaying)
        {
            aSource.clip = GetAudio(audioName);
            aSource.volume = GetVolume(audioName);
            aSource.loop = true;
            aSource.Play();
        }
        else
        {
            backupAudioSource[backupNo].clip = GetAudio(audioName);
            backupAudioSource[backupNo].volume = GetVolume(audioName);
            backupAudioSource[backupNo].loop = true;
            backupAudioSource[backupNo].Play();
            backupNo++;
            if (backupNo == backupAudioSource.Count) backupNo = 0;
        }
    }

    public void StopAudio()
    {
        foreach (AudioSource aS in backupAudioSource)
        {
            if (aS.isPlaying)
            {
                //aS.Stop();
                StartCoroutine(AudioSmoothOut(aS));
            }
        }
    }
    
    IEnumerator AudioSmoothOut(AudioSource audioSource)
    {
        float startVol = audioSource.volume;
        float t = 0f;
        while (t <= 1f)
        {
            t += 1f * Time.deltaTime;
            float vol = Mathf.Lerp(startVol, 0, t);
            audioSource.volume = vol;
            yield return new WaitForEndOfFrame();
        }
    }
}
