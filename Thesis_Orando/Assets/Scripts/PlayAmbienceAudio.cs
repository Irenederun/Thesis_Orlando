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
    public AudioSource interAmbSource;
    public AudioSource interMusicSource;
    public AudioSource menuMusicSource;

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
            if (scene.name.Contains("1"))
            {
                PlayInterstitial(1f,true);
            }
            else
            {
                PlayInterstitial(1f,false);
            }
        }
        else if (scene.name.Contains("Ending"))
        {
            PlayEnding(1f);
        }
        else if (scene.name.Contains("Menu"))
        {
            StopAmb(0f);
        }
    }

    void PlayOpening(float waitTime)
    {
        StartCoroutine(playOpening(waitTime));
    }

    IEnumerator playOpening(float waitTime)
    {
        PlayMenuMusic(0);
        print("menu stop");
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(2f);
        PlayZenNoise(0.2f);
    }
    
    void PlayIC(float waitTime)
    {
        StartCoroutine(playIC(waitTime));
    }

    IEnumerator playIC(float waitTime)
    {
        if (interAmbSource.isPlaying)PlayInterstitialAmbience(0f);
        if (interMusicSource.isPlaying)PlayInterstitialMusic(0f);
        PlayTheatreNoise(0.5f);
        PlayZenNoise(0.2f);
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(2f);
        PlayZenNoise(0.2f);
    }

    void PlayOOC(float waitTime)
    {
        StartCoroutine(playOOC(waitTime));
    }

    IEnumerator playOOC(float waitTime)
    {
        PlayTheatreNoise(0.5f);
        PlayZenNoise(0.2f);
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(2f);
        PlayZenNoise(0.6f);
    }
    
    void PlayInterstitial(float waitTime, bool playAmb)
    {
        StartCoroutine(playInter(waitTime, playAmb));
    }
    
    IEnumerator playInter(float waitTime, bool playAmb)
    {
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(0f);
        PlayZenNoise(0f);
        if (playAmb)
        {
            yield return new WaitForSeconds(waitTime);
            PlayInterstitialAmbience(1f);
            PlayInterstitialMusic(0.15f);
        }
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

    void PlayInterstitialAmbience(float targetVol)
    {
        if (!interAmbSource.isPlaying) interAmbSource.Play();
        StartCoroutine(VolumeChange(interAmbSource, targetVol));
    }
    
    void PlayInterstitialMusic(float targetVol)
    {
        if (!interMusicSource.isPlaying) interMusicSource.Play();
        StartCoroutine(VolumeChange(interMusicSource, targetVol));
    }
    
    void PlayMenuMusic(float targetVolume)
    {
        if (!menuMusicSource.isPlaying) menuMusicSource.Play();
        StartCoroutine(VolumeChange2(menuMusicSource, targetVolume));
    }

    
    void StopAmb(float waitTime)
    {
        StartCoroutine(StopAmbience(waitTime));
    }
    
    IEnumerator StopAmbience(float waitTime)
    {
        PlayMenuMusic(0);
        yield return new WaitForSeconds(waitTime);
        PlayTheatreNoise(0f);
        PlayZenNoise(0f);
    }

    public void MenuMusic()
    {
        StartCoroutine(PlayMenuMusic());
    }

    IEnumerator PlayMenuMusic()
    {
        yield return new WaitForSeconds(0);
        PlayMenuMusic(0.3f);
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
    
    IEnumerator VolumeChange2(AudioSource audioSource, float targetVol)
    {
        float startVol = audioSource.volume;
        float t = 0f;
        while (t <= 1f)
        {
            t += 1f * Time.deltaTime;
            float vol = Mathf.Lerp(startVol, targetVol, t);
            audioSource.volume = vol;
            yield return new WaitForEndOfFrame();
        }
    }
}
