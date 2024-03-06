using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentLevel = 0;
    public List<string> wordBank = new List<string>();
    public List<string> limbBank = new List<string>();
    public LimbLibrary limbLibrary;


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
      
    [System.Serializable]
    public struct chipsInfo
    {
        public Vector4 ChipColor;//TODO: will be sprite later
        public string ChipCategory;

        public chipsInfo(Vector4 color, string chipCategory)
        {
            this.ChipColor = color;
            this.ChipCategory = chipCategory;
        }
    }
    public List<chipsInfo> myChips = new List<chipsInfo>();

    [System.Serializable]
    public struct chipWorthEachRound
    {
        public string RoundName;
        public string Category1;
        public float Value1;
        public string Category2;
        public float Value2;
        public string Category3;
        public float Value3;
        public string Category4;
        public float Value4;
     }
    public List<chipWorthEachRound> chipWorthChart;

    [System.Serializable]
    public struct CardInventory
    {
        public string cardNameInvent;
        public Vector4 cardColorInvent; //TODO: will be sprite later
    }
    public List<CardInventory> cardInventory = new List<CardInventory>();

    public void ChangeLevel()
    {
        currentLevel++;
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
}
