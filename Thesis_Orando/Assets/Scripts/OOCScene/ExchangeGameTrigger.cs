using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExchangeGameTrigger : BasicBehavior
{
    [SerializeField] private List<SpriteRenderer> sprs;
    public int associatedGameNo = 0;

    public override void ClickedByMouse()
    {
        OOCManager.instance.StartWordExchange(associatedGameNo);
    }

    private void Start()
    {
        ExchangeGameManager.instance.endGameEvent += LayerMoveDown;
        ExchangeGameManager.instance.startGameEvent += LayerMoveUp;
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
    }
    
    private void LayerMoveUp(int gameID)
    {
        if (gameID != associatedGameNo) return;
        foreach (SpriteRenderer sp in sprs)
        {
            sp.sortingLayerName = "UIOverlay";
        }
    }
}
