using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CardBehavior : BasicBehavior 
{
    public float cardReqValue;
    public string cardName;
    public OOCManager.CardInfo thisCard;
    private GameObject selectedEffect;
    private int clickTimes = 0;
    public Vector3 cardInitialPos;
    public Transform cardTarget;
    private bool cardIsOwned = false;
    public GameObject minigamePrefab;

    //TODO: cheng to ini sprite later
    private Color cardinicolor;

    [SerializeField] private enum CardState
    {
        Dealing,
        Default,
        Selected,
        Submitted,
        GivenToPlayer,
        Owned,
        Activated,
    }
    [SerializeField]
    private CardState cardState;

    private Vector3 velocity = Vector3.zero;
    private float smoothTimeDealing = 0.3f;
    private float smoothTimeOwning = 0.5f;
    float speed = 90;

    void Start()
    {
        cardinicolor = GetComponent<SpriteRenderer>().color;//TODO: change to getcompoent sp.sprite later. 
        selectedEffect = transform.GetChild(0).gameObject;
        
        cardState = CardState.Dealing;
        IEnumerator coroutine = SwitchState();
        StartCoroutine(coroutine);
    }

    IEnumerator SwitchState()
    {
        yield return new WaitForSeconds(1);
        cardState = CardState.Default;
        GetComponent<Collider2D>().enabled = true;
    }


    private void Update()
    {
        if (DialogueManager.instance != null)
        {
            if (DialogueManager.instance.isTalking)
            {
                return;
            }
        }
        
        switch (cardState)
        {
            case CardState.Dealing:
                transform.position = Vector3.SmoothDamp(transform.position, cardInitialPos, ref velocity, smoothTimeDealing, Mathf.Infinity);
                break;
            case CardState.GivenToPlayer:
                transform.position = Vector3.SmoothDamp(transform.position, cardTarget.position, ref velocity, smoothTimeOwning, Mathf.Infinity);
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, cardTarget.rotation, step);
                break;
        }
    }

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();

        if (!cardIsOwned)
        {
            switch (clickTimes)
            {
                case 0:
                    CardSelecion();
                    break;
                case 1:
                    CardUnselection();
                    break;
            }
        }
        else
        {
            switch (clickTimes)
            {
                case 0:
                    clickTimes++;
                    CardFlip("face");
                    break;
                case 1:
                    clickTimes--;
                    CardFlip("back");
                    break;
            }
        }
    }

    public void CardSelecion()
    {
        clickTimes++;
        if (OOCManager.instance.selectedCard != null)
        {
            OOCManager.instance.selectedCard.GetComponent<CardBehavior>().CardUnselection();
        }
        cardState = CardState.Selected;
        OOCManager.instance.selectedCard = gameObject;
        selectedEffect.SetActive(true);
    }

    public void CardUnselection()
    {
        clickTimes--;
        cardState = CardState.Default;
        OOCManager.instance.selectedCard = null;
        selectedEffect.SetActive(false);
    }

    public void CardSubmission()
    {
        cardState = CardState.Submitted;
    }

    public void CardGivenToPlayer()
    {
        CardUnselection();
        cardTarget = transform.parent.GetChild(0);
        cardState = CardState.GivenToPlayer;
        OOCManager.instance.SwitchInteractabilityForAll(false);
        OOCManager.instance.KeepInteractabilityForUI(true);//TODO: this really should be made better in the future
        IEnumerator coroutine = CardOwned();
        StartCoroutine(coroutine);
    }

    private IEnumerator CardOwned()
    {
        yield return new WaitForSeconds(3);
        cardState = CardState.Owned;
        cardIsOwned = true;
        clickTimes = 0;
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        CardAddToInventory();//TODO: need to decide when this happens exactly
    }

    private void CardAddToInventory()
    {
        //TODO: need to decide when this happen exactly
        GameManager.CardInventory thisCard = new GameManager.CardInventory
        {
            cardNameInvent = cardName,
            cardColorInvent = cardinicolor,
        };
        GameManager.instance.cardInventory.Add(thisCard);
    }

    private void CardFlip(string side)
    {
        switch (side)
        {
            case "face":
                //print("flipped to face up");
                GetComponent<SpriteRenderer>().color = Color.magenta;//TODO: will be animation of flipping later
                EnterMiniGame();
                break;
            case "back":
                //print("fipped to face down");
                GetComponent<SpriteRenderer>().color = cardinicolor;//TODO: will be animation of flipping later
                break;
        }
    }
    
    public void EnterMiniGame()
    {
        GameObject minigame = Instantiate(minigamePrefab);
        OOCManager.instance.SwitchInteractabilityForAll(false);
        minigame.GetComponent<MiniGamePage>().cardObj = gameObject;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void CardActivatedFromMiniGame()
    {
        cardState = CardState.Activated;
        GetComponent<Collider2D>().enabled = false;
        Vector4 curCol = GetComponent<SpriteRenderer>().color;
        Color endCol = new Vector4(curCol.x, curCol.y, curCol.z, 0);
        IEnumerator coroutine = ChangeColor(curCol, endCol, 1f);
        StartCoroutine(coroutine);
    }

    private IEnumerator ChangeColor(Color start, Color end, float duration)
    {
        Color someColorValue;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            someColorValue = Color.Lerp(start, end, normalizedTime);
            GetComponent<SpriteRenderer>().color = someColorValue;
            yield return null;
        }
        someColorValue = end;
        GetComponent<SpriteRenderer>().color = someColorValue;
        
        OOCManager.instance.EndingAnim();
        Destroy(gameObject);
    }
}
