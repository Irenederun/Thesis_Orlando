using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    public void GoMakeSomeSound(string audioName)
    {
        AudioManager.instance.PlayAudio(audioName, GetComponent<AudioSource>());
    }

    public void GoMakeSomeLoopingSound(string audioName)
    {
        AudioManager.instance.PlayAudioLoop(audioName, GetComponent<AudioSource>());
    }

    public void GoStopTheSound()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            //GetComponent<AudioSource>().Stop();
            StartCoroutine(AudioSmoothOut(GetComponent<AudioSource>()));
        }
        AudioManager.instance.StopAudio();
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

    public void StartMenu()
    {
        AudioManager.instance.gameObject.GetComponent<PlayAmbienceAudio>().MenuMusic();
    }

    public void StopAmb()
    {
        AudioManager.instance.gameObject.GetComponent<PlayAmbienceAudio>().StopAmb(1);
    }
    
    public void PlayIC()
    {
        AudioManager.instance.gameObject.GetComponent<PlayAmbienceAudio>().PlayIC(1);
    }

    public void PlayOOC()
    {
        AudioManager.instance.gameObject.GetComponent<PlayAmbienceAudio>().PlayOOC(1);    }
}
