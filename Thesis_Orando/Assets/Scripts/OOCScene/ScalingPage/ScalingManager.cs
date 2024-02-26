using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingManager : ManagerBehavior
{
    public static ScalingManager instance;
    public List<GameObject> ScalingPageObjs = new List<GameObject>();
    [HideInInspector] public List<GameObject> selectedChips = new List<GameObject>();
    [SerializeField] private List<Vector3> chipPosInitial = new List<Vector3>();
    public GameObject chipPrefab;
    [HideInInspector] public GameObject chipParent;
    [HideInInspector] public float allChipsWorthThisTurn;
    public GameObject scalePagePrefab;
    [HideInInspector] public float currentCardWorth;

    private float selectedChipWeight = 0;//combine w allchipworththisturn later.
    private bool coroutineHasStarted = false;
    public GameObject scale;

    [SerializeField]
    private float timeNeededForEnableButton = 2f;

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
       
    void Start()
    {
        StartThisTurn();
        currentCardWorth = OOCManager.instance.selectedCardInfo.cardWorth;
    }

    private void StartThisTurn()
    {
        LoadAvailableChips();
    }

    public void LoadAvailableChips()
    {
        for (int i = 0; i < GameManager.instance.myChips.Count; i++)
        {
            GameObject thisChip = Instantiate(chipPrefab, chipPosInitial[i], Quaternion.identity);
            thisChip.transform.SetParent(chipParent.transform);
            thisChip.GetComponent<SpriteRenderer>().color = GameManager.instance.myChips[i].ChipColor;
            thisChip.GetComponent<ChipBehavior>().thisChip = GameManager.instance.myChips[i];
            thisChip.GetComponent<ChipBehavior>().chipCategory = GameManager.instance.myChips[i].ChipCategory;
            ScalingPageObjs.Add(thisChip);
        }

        SwitchInteractabilityForAll(true);
    }

    public void TiltScale(float chipWeight)
    {
        selectedChipWeight += chipWeight;

        if (selectedChipWeight < currentCardWorth)
        {
            if (selectedChips.Count != 0)
            {
                scale.transform.eulerAngles = new Vector3(0, 0, 5);
            }
            else
            {
                scale.transform.eulerAngles = new Vector3(0, 0, 10);
            }
        }
        else if (selectedChipWeight == currentCardWorth)
        {
            scale.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (selectedChipWeight > currentCardWorth)
        {
            scale.transform.eulerAngles = new Vector3(0, 0, -10);
        }
    }

    public void ChipsSubmission()
    {
        SwitchInteractabilityForAll(false);

        if (selectedChips.Count != 0)
        {
            for (int i = 0; i < selectedChips.Count; i++)
            {
                if (selectedChips[i] != null)
                {
                    selectedChips[i].GetComponent<ChipBehavior>().ChipSubmission(i);    
                }
            }
        }
        else
        {
            CardNotWon();
        }
    }

    public void CalculateWorth(float chipworth)
    {
        allChipsWorthThisTurn += chipworth;
        if (!coroutineHasStarted)
        {
            StartCoroutine("WorthCalculated");
            coroutineHasStarted = true;
        }
    }

    IEnumerator WorthCalculated()
    {
        yield return new WaitForSeconds(0.5f);
        ComparisonWithCardValue(allChipsWorthThisTurn);
    }

    public void ComparisonWithCardValue(float chipValue)
    {
        if (chipValue >= currentCardWorth)
        {
            print("you won this card. " + "chip=" + chipValue + ", card= " + currentCardWorth);
            CardWon();

        }
        else
        {
            print("you didnt win this card" + "chip=" + chipValue + ", card= " + currentCardWorth);
            CardNotWon();
        }
    }

    private void CardWon()
    {
        if (OOCManager.instance != null)
        {
            OOCManager.instance.RemoveCardFromListOnWinning();
            IEnumerator coroutine = CardWonCoroutine();
            StartCoroutine(coroutine);
        }
        else if (ICManager.instance != null)
        {
            ICManager.instance.EndExchange();
        }
    }

    IEnumerator CardWonCoroutine()
    {
        yield return new WaitForSeconds(timeNeededForEnableButton);
        DialogueManager.instance.TriggerDialogueOOC("CardWon");
        while (DialogueManager.instance.isTalking)
        {
            yield return null;
        }
        DestroySelfOnClose();
        OOCManager.instance.selectedCard.GetComponent<CardBehavior>().CardGivenToPlayer();
    }

    private void CardNotWon()
    {
        IEnumerator coroutine = CardNotWonCoroutine();
        StartCoroutine(coroutine);
    }

    IEnumerator CardNotWonCoroutine()
    {
        yield return new WaitForSeconds(timeNeededForEnableButton);
        DialogueManager.instance.TriggerDialogueOOC("CardLost");
        while (DialogueManager.instance.isTalking)
        {
            yield return null;
        }
        StartOver();
    }

    public void SwitchInteractabilityForAll(bool interactability)
    {
        foreach (GameObject obj in ScalingPageObjs)
        {
            if (!interactability)
            {
                obj.layer = LayerMask.NameToLayer("Default");
            }
            else
            {
                obj.layer = LayerMask.NameToLayer("Interactable");
            }
        }
    }

    public void StartOver()
    {
        print("starting over");
        SwitchInteractabilityForAll(true);
        ScalingPageObjs[0].GetComponent<SubmissionButtonBehavior>().ChangeColor();
        coroutineHasStarted = false;
    }

    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        if (OOCManager.instance != null)
        {
            OOCManager.instance.SwitchInteractabilityForAll(true);
        }
        Destroy(gameObject);
    }
}
