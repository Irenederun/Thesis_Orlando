using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeGame : ManagerBehavior
{
    public List<Transform> selfWordDesPos;
    public GameObject wordPrefab;
    private GameObject mouseRayCast;
    public GameObject myWordsHoder;
    public GameObject confirmPrompt;
    public LimbSync mainCharLimbSync;
    public GameObject changeSceneButton;
    public GameObject selfMaskWords;
    public GameObject selfMaskLimbs;
    public GameObject otherMask;

    public bool dontCenter;
    
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

    public int positionInList;
    [SerializeField]public Button closeButton;

    // Start is called before the first frame update
    void Start()
    {
        //init();
        //closeButton.clicked += ExchangeGameManager.instance.EndExchangeGame;
        closeButton.onClick.AddListener(ExchangeGameManager.instance.EndExchangeGame);
    }

    public void Init()
    {
        // load current words
        LoadSelfWords();

        // tell all UIWordBehavior who their exchangeGameManager is to avoid singleton
        UIWordBehavior[] buttons1 = GetComponentsInChildren<UIWordBehavior>(true);
        UIWordBehavior[] buttons2 = mainCharLimbSync.gameObject.GetComponentsInChildren<UIWordBehavior>(true);
        foreach (UIWordBehavior b in buttons1)
        {
            b.SetExchangeGameManager(this);
        } 
        foreach (UIWordBehavior b in buttons2)
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

        //tutorial
        if (GameManager.instance.isTutorial)
        {
            SwitchMask( selfMaskWords, selfMaskLimbs, otherMask);
            DialogueManager.instance.TriggerDialogueOOC("ExchangeStarted");
            print("started");
        }
        else
        {
            selfMaskWords.SetActive(false);
            selfMaskLimbs.SetActive(false);
            otherMask.SetActive(false);
        }
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
        
        selfMaskWords.SetActive(false);
        selfMaskLimbs.SetActive(false);
        otherMask.SetActive(false);
    }

    private void Update()
    {
        if (!dontCenter) keepCenter();
        
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
                
                if (currentOtherWord != null) currentOtherWord.CallResetEvent();//if another word is selected before, reset
                if (currentOtherWord == script)//if clicking the same word
                {
                    currentOtherWord = null;
                    currentOtherWordText = "";
                }
                else//if first time clicking, or if clicking a different word
                {
                    //if tutorial
                    if (GameManager.instance.isTutorial && currentOtherWord == null)
                    {
                        SwitchMask(otherMask, selfMaskLimbs, selfMaskWords);
                        DialogueManager.instance.TriggerDialogueOOC("TargetChosen");
                    }
                    //
                    
                    currentOtherWord = script;
                    currentOtherWordText = text;
                    script.CallButtonEvent();
                }


                break;
            case "myWord":
                if (currentMyWord != null)//if selected a word before
                {
                    currentMyWord.CallResetEvent();//reset
                    if (currentMyLimb != null)//if selected a limb
                    {
                        currentMyLimb.CallResetEvent();//reset associated limb
                        currentMyLimb = null;
                    }
                }

                if (currentMyWord == script)//if selecting the same word
                {
                    currentMyWord = null;
                    currentMyWordText = "";
                    if (currentMyLimb != null)//if already associated a limb
                    {
                        currentMyLimb.CallResetEvent();
                        currentMyLimb = null;
                    }
                }
                else //if first time selecting, or if selecting a new word 
                {
                    if (currentOtherWord != null) // only when a target word has been selected
                    {
                        //if tutorial
                        if (GameManager.instance.isTutorial && currentMyWord == null)
                        {
                            SwitchMask(otherMask, selfMaskWords, selfMaskLimbs);
                            DialogueManager.instance.TriggerDialogueOOC("MyWordChosen");
                        }
                        //
                        
                        currentMyWord = script;
                        currentMyWordText = text;
                        script.CallButtonEvent();
                    }
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
                        //if tutorial
                        if (GameManager.instance.isTutorial && currentMyLimb == null)
                        {
                            SwitchMask( selfMaskLimbs, null, null);
                            DialogueManager.instance.TriggerDialogueOOC("MyLimbChosen");
                        }
                        //
                        
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
        if (GameManager.instance.isTutorial)
        {
            GameManager.instance.TutorialOver();
            DialogueManager.instance.TriggerDialogueOOC("ExchangeClicked");
            selfMaskWords.SetActive(false);
            selfMaskLimbs.SetActive(false);
            otherMask.SetActive(false);
        }
        
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
        currentOtherWord.GetComponent<OtherWord>().limbName = myLimb;
        currentOtherWord.ChangeWordText();
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

    private void SwitchMask(GameObject unclickable1, GameObject unclickable2, GameObject clickable)
    {
        if (unclickable1 != null) unclickable1.SetActive(true);
        if (unclickable2 != null) unclickable2.SetActive(true);
        if (clickable != null) clickable.SetActive(false);
    }
}
