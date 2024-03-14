using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class LimbOpenClose : MonoBehaviour
{
    // private void OnEnable()
    // {
    //     // ExchangeGameManager.instance.endGameAction += TurnOn;
    //     // ExchangeGameManager.instance.startGameAction += TurnOff;
    //     ExchangeGameManager.instance.endGameAction.AddListener(TurnOn);
    //     ExchangeGameManager.instance.startGameAction.AddListener( TurnOff);
    //     
    // }
    //
    // private void OnDisable()
    // {
    //     ExchangeGameManager.instance.endGameAction -= TurnOn;
    //     ExchangeGameManager.instance.startGameAction -= TurnOff;
    // }

    private void TurnOn()
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
    }
    
    private void TurnOff()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }
}
