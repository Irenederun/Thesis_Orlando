using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIWordBehavior : BasicBehavior
{
    public string group;
    public TMP_Text text;
    public UIBehavior myUIBehavior;

    public UnityEvent buttonEvent;
    public UnityEvent resetEvent;

    private ExchangeGame _exchangeGame;
    
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        string word = "";
        if (text != null) word = text.text;
        _exchangeGame.ClickableTracker(group, this, word);
    }

    public void CallButtonEvent()
    {
        buttonEvent?.Invoke();
    }

    public void CallResetEvent()
    {
        resetEvent?.Invoke();
    }

    public void SetExchangeGameManager(ExchangeGame egm)
    {
        _exchangeGame = egm;
    }
}
