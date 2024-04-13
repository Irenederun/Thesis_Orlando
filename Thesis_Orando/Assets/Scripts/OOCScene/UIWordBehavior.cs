using UnityEngine.Events;
using TMPro;

public class UIWordBehavior : BasicBehavior
{
    public string group;
    public TMP_Text text;
    public UIBehavior myUIBehavior;

    public UnityEvent buttonEvent;
    public UnityEvent resetEvent;

    public ExchangeGame _exchangeGame;
    public NPCBodyPart myBodyPartManager;

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        if (group.Contains("target")) GetComponent<OtherWord>().UpdatePos();
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
    
    public void ChangeWordText()
    {
        GetComponent<OtherWord>().ChangeWordText();
    }
}
