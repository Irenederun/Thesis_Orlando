using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ExchangeGameManager : MonoBehaviour
{
    public static ExchangeGameManager instance;
    public LimbSync oocMainCharLimbSync;
    public List<ExchangeGame> exchangeGames = new List<ExchangeGame>();
    //public bool isTutorial = true;
    public OtherWordLibrary otherWordDictionary;
    public UnityEvent endGameAction;
    public UnityEvent startGameAction;

    public delegate void namenotimportanto(int gameID);

    public event namenotimportanto endGameEvent;
    public event namenotimportanto startGameEvent;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += Init;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Init;
    }

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
    
    // private void Start()
    // {
    //     foreach (ExchangeGame xGame in exchangeGames)
    //     {
    //         xGame.gameObject.SetActive(false);
    //     }
    // }

    void Init(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("OOC"))
        {
            for (int i = 0; i < exchangeGames.Count; i++)
            {
                if (exchangeGames[i] == null)
                {
                    exchangeGames[i] = GameObject.Find("ExchangeGame" + i).GetComponent<ExchangeGame>();
                }
            }
            foreach (ExchangeGame xGame in exchangeGames)
            {
                xGame.gameObject.SetActive(false);
            }
        }
    }

    public void StartExchangeGame(int number)
    {
        exchangeGames[number].gameObject.SetActive(true);
        exchangeGames[number].Init();
        //startGameAction.Invoke();
        startGameEvent?.Invoke(number);
    }

    public void EndExchangeGame()
    {
        // there should always be just one active exchange game so this is fine
        for (int i = 0; i < exchangeGames.Count; i++)
        {
            if (exchangeGames[i].gameObject.activeSelf)
            {
                exchangeGames[i].End();
                exchangeGames[i].gameObject.SetActive(false);
                oocMainCharLimbSync.Sync();
                oocMainCharLimbSync.gameObject.GetComponent<ActressController>().EnableWalking();
                MouseRayCast.instance.gameObject.SetActive(true);
                endGameEvent?.Invoke(i);
                break;
            }
        }
        DialogueManager.instance.TriggerDialogueOOC("ExitExchange");
        //endGameAction.Invoke();
    }

    
}
