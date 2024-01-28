using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOCManager : MonoBehaviour
{
    public static OOCManager instance;
    public List<GameObject> OOCInteractableObjects = new List<GameObject>();
    public GameObject cardPrefab;
    public List<Vector3> cardPosInitial = new List<Vector3>();
    public GameObject cardParent;
    public GameObject selectedCard;
    public CardInfo selectedCardInfo;

    private bool cardAreDealt = false;
    public GameObject scalingPagePrefab;

    //TODO: add reshuffule after adding randomized card generation. remember to reset cardAreDealt.

    [System.Serializable]
    public struct CardInfo
    {
        public string cardName;
        public Vector4 cardColor; //will be sprite later
        public float cardWorth;
    }
    public List<CardInfo> listOfCards = new List<CardInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartCardRound()
    {
        if (!cardAreDealt)
        {
            LoadAvailableCards();
            cardAreDealt = true;
        }
    }

    public void LoadAvailableCards()
    {
        //TODO: loading first three for now. Later change to random 3 out of 5
        if (listOfCards.Count >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                GenerateCard(i);
            }
        }
        else
        {
            for (int i = 0; i < listOfCards.Count - 1; i++)
            {
                GenerateCard(i);
            }
        }
        SwitchInteractabilityForAll(true);
    }

    private void GenerateCard(int i)
    {
        GameObject thisCard = Instantiate(cardPrefab, cardPosInitial[i], Quaternion.identity);
        thisCard.transform.SetParent(cardParent.transform);
        thisCard.GetComponent<SpriteRenderer>().color = listOfCards[i].cardColor;
        thisCard.GetComponent<CardBehavior>().thisCard = listOfCards[i];
        thisCard.GetComponent<CardBehavior>().cardReqValue = listOfCards[i].cardWorth;
        thisCard.GetComponent<CardBehavior>().cardName = listOfCards[i].cardName;
        OOCInteractableObjects.Add(thisCard);
    }

    public void CardSubmittedOpenScaling()
    {
        GameObject scalingPage = Instantiate(scalingPagePrefab);
        SwitchInteractabilityForAll(false);
        selectedCardInfo = selectedCard.GetComponent<CardBehavior>().thisCard;
        selectedCard.GetComponent<CardBehavior>().CardSubmission();
    }

    public void RemoveCardFromListOnWinning()
    {
        listOfCards.Remove(selectedCardInfo);
        OOCInteractableObjects.Remove(selectedCard);
        //selectedCard.GetComponent<CardBehavior>().CardGivenToPlayer();
    }

    public void SwitchInteractabilityForAll(bool interactability)
    {
        switch (interactability)
        {
            case true:
                foreach (GameObject obj in OOCInteractableObjects)
                {
                    obj.layer = LayerMask.NameToLayer("Interactable");
                }
                break;
            case false:
                foreach (GameObject obj in OOCInteractableObjects)
                {
                    obj.layer = LayerMask.NameToLayer("Default");
                }
                break;
        }
    }

    public void KeepInteractabilityForUI(bool interactability)
    {
        switch (interactability)
        {
            case true:
                OOCInteractableObjects[2].layer = LayerMask.NameToLayer("Interactable");
                break;
            case false:
                OOCInteractableObjects[2].layer = LayerMask.NameToLayer("Default");
                break;
        }
    }
}
