using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ExchangeGame : ManagerBehavior
{
    public List<Transform> selfWordDesPos;
    public GameObject wordPrefab;
    private GameObject mouseRayCast;
    public GameObject myWordsHoder;
    public GameObject confirmPrompt;
    public LimbSync mainCharLimbSync;
    public GameObject changeSceneButton;

    public enum state
    {
        selecting, exchanging
    }

    public state curState;

    private UIWordBehavior currentOtherWord;
    private string currentOtherWordText;
    private UIWordBehavior currentMyWord;
    private string currentMyWordText;
    private UIWordBehavior currentMyLimb;

    // Start is called before the first frame update
    void Start()
    {
        //init();
    }

    public void Init()
    {
        // load current words
        LoadSelfWords();
        
        // tell all UIWordBehavior who their exchangeGameManager is to avoid singleton
        UIWordBehavior[] buttons = GetComponentsInChildren<UIWordBehavior>(true);
        foreach (UIWordBehavior b in buttons)
        {
            b.SetExchangeGameManager(this);
        }
        
        //disable SwitchScene Button
        changeSceneButton.SetActive(false);
        
        // disable other mouse input
        mouseRayCast = MouseRayCast.instance.gameObject;
        mouseRayCast.SetActive(false);

        // refresh the limbs
        mainCharLimbSync.Sync();
    }

    public void End()
    {
        // reset everything since it might get opened again
        clearSelfWords();
        
        mouseRayCast.SetActive(true);
        changeSceneButton.SetActive(true);
        
        if (currentOtherWord != null)
        {
            currentOtherWord.CallResetEvent();
            currentOtherWord = null;
            currentMyWordText = "";
        }
        if (currentMyWord != null)
        {
            currentMyWord.CallResetEvent();
            currentMyWord = null;
            currentMyWordText = "";
        }
        if (currentMyLimb != null)
        {
            currentMyLimb.CallResetEvent();
            currentMyLimb = null;
        }
    }

    private void Update()
    {
        keepCenter();
        
        if (curState == state.selecting)
        {
            // show the confirmation prompt when all three selections are made
            if (currentOtherWord != null && currentMyWord != null && currentMyLimb != null)
            {
                if (confirmPrompt.activeSelf == false) confirmPrompt.SetActive(true);
            }
            else
            {
                if (confirmPrompt.activeSelf == true) confirmPrompt.SetActive(false);
            }
        }
        else if (curState == state.exchanging)
        {
            
        }
    }

    void clearSelfWords()
    {
        for (int i = myWordsHoder.transform.childCount-1; i >= 0; i--)
        {
            Destroy(myWordsHoder.transform.GetChild(i).gameObject);
        }
    }

    void LoadSelfWords()
    {
        for (int i = 0; i < GameManager.instance.wordBank.Count; i++)
        {
            GameObject thisword = Instantiate(wordPrefab);
            thisword.transform.position = selfWordDesPos[i].position;
            thisword.GetComponentInChildren<TMP_Text>().text = GameManager.instance.wordBank[i];
            thisword.transform.parent = myWordsHoder.transform;
        }
    }

    public void ClickableTracker(string group, UIWordBehavior script, string text)
    {
        if (curState != state.selecting) return;
        
        switch (group)
        {
            case "targetObj":
                if (currentOtherWord != null) currentOtherWord.CallResetEvent();
                if (currentOtherWord == script)
                {
                    currentOtherWord = null;
                    currentOtherWordText = "";
                }
                else
                {
                    currentOtherWord = script;
                    currentOtherWordText = text;
                    script.CallButtonEvent();
                }

                break;
            case "myWord":
                if (currentMyWord != null)
                {
                    currentMyWord.CallResetEvent();
                    if (currentMyLimb != null)
                    {
                        currentMyLimb.CallResetEvent();
                        currentMyLimb = null;
                    }
                }

                if (currentMyWord == script)
                {
                    currentMyWord = null;
                    currentMyWordText = "";
                    if (currentMyLimb != null)
                    {
                        currentMyLimb.CallResetEvent();
                        currentMyLimb = null;
                    }
                }
                else
                {
                    currentMyWord = script;
                    currentMyWordText = text;
                    script.CallButtonEvent();
                }

                break;
            case "myLimb":
                if (currentMyLimb != null) currentMyLimb.CallResetEvent();
                if (currentMyLimb == script)
                {
                    currentMyLimb = null;
                    if (currentMyWord != null)
                    {
                        MyWord myWord = currentMyWord.myUIBehavior as MyWord;
                        myWord.Select();
                    }
                }
                else
                {
                    if (currentMyWord != null)
                    {
                        // connect selected myword and this limb
                        currentMyLimb = script;
                        MyWord myWord = currentMyWord.myUIBehavior as MyWord;
                        MyLimb myLimb = currentMyLimb.myUIBehavior as MyLimb;
                        myWord.Connect(myLimb.connectionPos);
                        script.CallButtonEvent();
                    }
                }

                break;
        }
    }

    // importanto big function that handles limb exchange and notifies everybodei
    public void Exchange()
    {
        curState = state.exchanging;
        confirmPrompt.SetActive(false);
        
        // tell GameManager about word exchange
        GameManager.instance.SwitchWord(currentMyWordText, currentOtherWordText);
        
        // tell GameManager about limb exchange
        string curLimbSprite = currentMyLimb.GetComponent<SpriteRenderer>().sprite.name;
        string newLimbSprite =
            currentOtherWord.GetComponent<OtherWord>().limb.GetComponent<SpriteRenderer>().sprite.name;
        GameManager.instance.SwitchLimb(curLimbSprite, newLimbSprite);
        
        //**TODO: play the exchange animation
        StartCoroutine(exchangeWordsAndLimbs());
        
        
        
    }

    IEnumerator exchangeWordsAndLimbs()
    {
        //todo: make the actual animation
        // simple text swap
        currentMyWord.text.text = currentOtherWordText;
        currentOtherWord.text.text = currentMyWordText;
        // simple limb swap
        string myLimb = currentMyLimb.GetComponent<SpriteRenderer>().sprite.name;
        SpriteRenderer otherWordLimb =
            currentOtherWord.GetComponent<OtherWord>().limb.GetComponent<SpriteRenderer>();
        currentMyLimb.GetComponent<SpriteRenderer>().sprite = otherWordLimb.sprite;
        otherWordLimb.sprite = GameManager.instance.GetLimb(myLimb);
        yield return null;

        // cleanup variables
        currentMyWord.CallResetEvent();
        currentMyWord = null;
        currentOtherWord.CallResetEvent();
        currentOtherWord = null;
        currentMyLimb.CallResetEvent();
        currentMyLimb = null;

        currentMyWordText = "";
        currentOtherWordText = "";
        
        // back to selecting
        curState = state.selecting;
    }

    // public override void DestroySelfOnClose()
    // {
    //     base.DestroySelfOnClose();
    //     
    //     mouseRayCast.SetActive(true);
    //     changeSceneButton.SetActive(true);
    // }

    private void keepCenter()
    {
        // make sure it's in front of the cam
        Vector3 pos = Camera.main.transform.position;
        pos.z = 0f;
        transform.position = pos;
    }
    
}
