using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IconBehaviors : BasicBehavior
{
    //[SerializeField] private GameObject actress;
    [SerializeField] private List<SpriteRenderer> sprs;
    //private bool canInteract;
    //private int clickTimes = 0;
    public int associatedGameNo = 0;

    public override void ClickedByMouse()
    {
        ICManager.instance.StartWordExchange(associatedGameNo);
    }
    
    public void IconState(bool state)
    {
        print("whomst???");
    }
    
    private void Start()
    {
        ExchangeGameManager.instance.endGameEvent += LayerMoveDown;
        ExchangeGameManager.instance.startGameEvent += LayerMoveUp;
        //ExchangeGameManager.instance.endGameAction.AddListener(TurnOn);
        //ExchangeGameManager.instance.startGameAction.AddListener( TurnOff);
    }
    
    private void OnDisable()
    {
        ExchangeGameManager.instance.endGameEvent -= LayerMoveDown;
        ExchangeGameManager.instance.startGameEvent -= LayerMoveUp;
    }

    private void LayerMoveDown(int gameID)
    {
        if (gameID != associatedGameNo) return;

        foreach (SpriteRenderer sp in sprs)
        {
            sp.sortingLayerName = "Default";
        }
        //gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        print("gfjsdkjhgsfkdghkd");
    }
    
    private void LayerMoveUp(int gameID)
    {
        if (gameID != associatedGameNo) return;
        foreach (SpriteRenderer sp in sprs)
        {
            sp.sortingLayerName = "UIOverlay";
        }
        //gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UIOverlay";
        print("1245435678438");
    }
}
