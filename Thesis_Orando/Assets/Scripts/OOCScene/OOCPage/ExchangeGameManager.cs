using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeGameManager : MonoBehaviour
{
    public static ExchangeGameManager instance;
    public LimbSync oocMainCharLimbSync;
    public List<ExchangeGame> exchangeGames = new List<ExchangeGame>();
    //public bool isTutorial = true;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        foreach (ExchangeGame xGame in exchangeGames)
        {
            xGame.gameObject.SetActive(false);
        }
    }

    public void StartExchangeGame(int number)
    {
        exchangeGames[number].gameObject.SetActive(true);
        exchangeGames[number].Init();
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
                break;
            }
        }
        
        DialogueManager.instance.TriggerDialogueOOC("ExitExchange");
    }

    
}
