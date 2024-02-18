using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OOCManager : MonoBehaviour
{
    public static OOCManager instance;
    public List<GameObject> OOCInteractableObjects = new List<GameObject>();
    public GameObject cardPrefab;
    public List<Vector3> cardPosInitial = new List<Vector3>();
    public GameObject cardParent;
    public GameObject selectedCard;
    public CardInfo selectedCardInfo;
    public GameObject endPrefab;

    private bool cardAreDealt = false;
    public GameObject scalingPagePrefab;

    private List<GameObject> current3Cards = new List<GameObject>();

    //TODO: add reshuffule after adding randomized card generation. remember to reset cardAreDealt.
    //TODO: instead of reshuffling, change to other cards disappearing. 

    [System.Serializable]
    public struct CardInfo
    {
        public string cardName;
        public Vector4 cardColor; //TODO: will be sprite later
        public float cardWorth;
        public GameObject minigamePrefab;
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
        // else
        // {
        //     Reshuffle();//TODO: currently reshuffling on second click b/c it takes one click to set bool to false
        // }
    }

    // public void Reshuffle()
    // {
    //     foreach (GameObject card in current3Cards)
    //     {
    //         OOCInteractableObjects.Remove(card);
    //         Destroy(card);
    //         //LoadAvailableCards();
    //     }
    //     cardAreDealt = false;
    // }

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
        GameObject thisCard = Instantiate(cardPrefab, OOCInteractableObjects[1].transform.position, Quaternion.identity);
        thisCard.transform.SetParent(cardParent.transform);
        thisCard.GetComponent<CardBehavior>().cardInitialPos = cardPosInitial[i];
        thisCard.GetComponent<SpriteRenderer>().color = listOfCards[i].cardColor;
        thisCard.GetComponent<CardBehavior>().thisCard = listOfCards[i];
        thisCard.GetComponent<CardBehavior>().cardReqValue = listOfCards[i].cardWorth;
        thisCard.GetComponent<CardBehavior>().cardName = listOfCards[i].cardName;
        thisCard.GetComponent<CardBehavior>().minigamePrefab = listOfCards[i].minigamePrefab;
        OOCInteractableObjects.Add(thisCard);
        current3Cards.Add(thisCard);
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
        current3Cards.Remove(selectedCard);
        OOCInteractableObjects.Remove(selectedCard);
    }

    public void CardActivated(GameObject cardObj)
    {
        IEnumerator coroutine = TriggerEnding(cardObj);
        StartCoroutine(coroutine);
    }
    
    IEnumerator TriggerEnding(GameObject cardObj)
    {
        DialogueManager.instance.TriggerDialogueOOC("OrlandoEnds");

        yield return new WaitForSeconds(0.5f);
        
        while (DialogueManager.instance.isTalking)
        {
            yield return null;
        }
        cardObj.GetComponent<CardBehavior>().CardActivatedFromMiniGame();
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
                OOCInteractableObjects[1].layer = LayerMask.NameToLayer("Interactable");
                OOCInteractableObjects[2].layer = LayerMask.NameToLayer("Interactable");
                break;
            case false:
                OOCInteractableObjects[1].layer = LayerMask.NameToLayer("Default");
                OOCInteractableObjects[2].layer = LayerMask.NameToLayer("Default");
                break;
        }
    }

    public void EndingAnim()
    {
        Instantiate(endPrefab);
        IEnumerator coroutine = nextScene();
        StartCoroutine(coroutine);
    }

    private IEnumerator nextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
