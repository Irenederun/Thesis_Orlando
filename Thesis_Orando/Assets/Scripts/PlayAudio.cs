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
}
