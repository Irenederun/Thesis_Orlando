using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<string> wordBank = new List<string>();
    public List<string> limbBank = new List<string>();
    public LimbLibrary limbLibrary;
    public bool isTutorial = true;
    public Action tutorialEnded;


    public void Awake()
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

    public void SwitchWord(string oldWord, string newWord)
    {
        for (int i=0; i<wordBank.Count; i++)
        {
            if (wordBank[i] == oldWord)
            {
                wordBank[i] = newWord;
                break;
            }
        }
    }

    public void SwitchLimb(string oldLimb, string newLimb)
    {
        for (int i = 0; i < limbBank.Count; i++)
        {
            if (limbBank[i] == oldLimb)
            {
                limbBank[i] = newLimb;
                break;
            }
        }
    }

    public Sprite GetLimb(string limbName)
    {
        return limbLibrary.GetLimb(limbName);
    }

    public List<Sprite> GetAllLimbs()
    {
        List<Sprite> limbs = new List<Sprite>();
        for (int i = 0; i < limbBank.Count; i++)
        {
            limbs.Add(limbLibrary.GetLimb(limbBank[i]));
        }

        return limbs;
    }
    
    public void TutorialOver()
    {
        isTutorial = false;
        tutorialEnded();
    }
}
