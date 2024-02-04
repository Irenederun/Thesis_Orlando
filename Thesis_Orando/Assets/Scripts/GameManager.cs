using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentLevel = 0;

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
}
