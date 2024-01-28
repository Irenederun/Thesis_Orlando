using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavior : BasicBehavior 
{
    public float cardReqValue;
    public string cardName;
    public OOCManager.CardInfo thisCard;
    private GameObject selectedEffect;
    private int clickTimes = 0;
    public Transform cardTarget;
    private bool cardIsOwned = false;

    //TODO: delete later
    private Color cardinicolor;

    [SerializeField]
    private enum CardState
    {
        Dealing,
        Default,
        Selected,
        Submitted,
        GivenToPlayer,
        Owned,
    }
    [SerializeField]
    private CardState cardState;

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.5f;
    float speed = 90;

    void Start()
    {
        cardinicolor = GetComponent<SpriteRenderer>().color;//TODO: change to getcompoennt sp.sprite later. 

        cardState = CardState.Default;
        selectedEffect = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        switch (cardState)
        {
            case CardState.Dealing:
                print("cardmoving");
                break;
            case CardState.GivenToPlayer:
                transform.position = Vector3.SmoothDamp(transform.position, cardTarget.position, ref velocity, smoothTime, Mathf.Infinity);
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
        //CardUnselection();
        cardState = CardState.Submitted;
    }

    public void CardGivenToPlayer()
    {
        CardUnselection();
        cardTarget = transform.parent.GetChild(0);
        cardState = CardState.GivenToPlayer;
        OOCManager.instance.SwitchInteractabilityForAll(false);
        OOCManager.instance.KeepInteractabilityForUI(true);//this really should be made better in the future
        IEnumerator coroutine = CardOwned();
        StartCoroutine(coroutine);
    }

    private IEnumerator CardOwned()
    {
        yield return new WaitForSeconds(2);
        cardState = CardState.Owned;
        cardIsOwned = true;
        clickTimes = 0;
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        CardAddToInventory();//need to decide when this happens exactly
    }

    private void CardAddToInventory()
    {
        //needn to decide when this happen exactly
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
                print("flipped to face up");
                GetComponent<SpriteRenderer>().color = Color.magenta;//TODO: will be animation of flipping later
                break;
            case "back":
                print("fipped to face down");
                GetComponent<SpriteRenderer>().color = cardinicolor;//TODO: will be animation of flipping later
                break;
        }

        //each card, due to its category, should have another script attached to it, containing the specific interaction of that card.
        //e.g., select words. that would be another UI overlay.
    }
}
