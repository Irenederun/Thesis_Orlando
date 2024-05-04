using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAmbienceAudio : MonoBehaviour
{
    public AudioSource theatreSource;
    public AudioSource zenSource;
    public AudioSource endingSource;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneChange;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChange;
    }

    void SceneChange(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("IC1"))
        {
            PlayOpening(20f);
        }
        else if (scene.name.Contains("IC"))
        {
            PlayIC(2f);
        }
        else if (scene.name.Contains("OOC"))
        {
            PlayOOC(2f);
        }
        else if (scene.name.Contains("Interstitial"))
        {
            PlayInterstitial(0f);
        }
        else if (scene.name.Contains("Ending"))
        {
            PlayEnding(0f);
        }
    }

    void PlayOpening(float waitTime)
    {
        StartCoroutine(playOpening(waitTime));
    }

    IEnumerator playOpening(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(1);
        PlayZenNoise(0.4f);
    }
    
    void PlayIC(float waitTime)
    {
        StartCoroutine(playIC(waitTime));
    }

    IEnumerator playIC(float waitTime)
    {
        PlayTheatreNoise(0.4f);
        PlayZenNoise(0.4f);
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(1);
        PlayZenNoise(0.4f);
    }

    void PlayOOC(float waitTime)
    {
        StartCoroutine(playOOC(waitTime));
    }

    IEnumerator playOOC(float waitTime)
    {
        PlayTheatreNoise(0.4f);
        PlayZenNoise(0.4f);
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(0.4f);
        PlayZenNoise(1f);
    }
    
    void PlayInterstitial(float waitTime)
    {
        StartCoroutine(playInter(waitTime));
    }
    
    IEnumerator playInter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(0f);
        PlayZenNoise(0f);
    }
    
    void PlayEnding(float waitTime)
    {
        StartCoroutine(playEnding(waitTime));
    }
    
    IEnumerator playEnding(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(0f);
        PlayZenNoise(0f);
        endingSource.Play();   
    }

    void PlayTheatreNoise(float targetVolume)
    {
        if (!theatreSource.isPlaying) theatreSource.Play();
        StartCoroutine(VolumeChange(theatreSource, targetVolume));
    }

    
    void PlayZenNoise(float targetVolume)
    {
        if (!zenSource.isPlaying) zenSource.Play();
        StartCoroutine(VolumeChange(zenSource, targetVolume));
    }
    
    IEnumerator VolumeChange(AudioSource audioSource, float targetVol)
    {
        float startVol = audioSource.volume;
        float t = 0f;
        while (t <= 1f)
        {
            t += 2f * Time.deltaTime;
            float vol = Mathf.Lerp(startVol, targetVol, t);
            audioSource.volume = vol;
            yield return new WaitForEndOfFrame();
        }
    }
}
