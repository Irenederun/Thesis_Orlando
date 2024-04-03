using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    [System.Serializable]
    public struct AudioData
    {
        public string name;
        public AudioClip audio;
        public float volume;
    }
    public List<AudioData> audioList = new List<AudioData>();

    public AudioClip GetAudio(string clipName)
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].name == clipName)
            {
                if (audioList[i].audio != null)
                    return audioList[i].audio;
            }
        }
        return null;
    }
    
    public float GetVolume(string clipName)
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].name == clipName)
            {
                if (audioList[i].volume != null)
                    return audioList[i].volume;
            }
        }
        return -10f;
    }
}
