using System.Collections.Generic;
using UnityEngine;

public class SpeechCardManager : ManagerBehavior
{
    public List<string> sentence;
    public static SpeechCardManager instance;
    
    [HideInInspector]public GameObject you;
    [HideInInspector]public GameObject me;

    [HideInInspector]public List<GameObject> destinations;
    [HideInInspector]public List<GameObject> words;
    public GameObject reload;
    public GameObject submit;

    private string finalSentence;

    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void AddToSentence(string word, int position)
    {
        sentence[position] = word;
        
        if (!reload.activeSelf)
        {
            reload.SetActive(true);
        }

        if (!sentence.Contains(""))
        {
            if (!submit.activeSelf)
            {
                submit.SetActive(true);
            }
        }
    }

    public void Submission()
    {
        for (int i = 0; i < 4; i++)
        {
            finalSentence += sentence[i] + " ";
        }
        print(finalSentence);
    }
    
    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        Destroy(gameObject);
    }
}
