using System.Collections;
using System.Collections.Generic;
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
        if (GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Stop();
        AudioManager.instance.StopAudio();
    }
}
