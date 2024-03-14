using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ICManager : MonoBehaviour
{
    public enum ICState
    {
        Start,
        ExchangeGameOpened,
    }
    public ICState icState;

    public List<GameObject> ICPageInteractableObjects;

    public static ICManager instance;

    public GameObject cardInventHandPrefab;
    public GameObject cam;
    public GameObject actress;
    [HideInInspector] public GameObject itemWithInteractionButtons;

    public GameObject desPosIconPrefab;
    private GameObject desPosIcon;
    public GameObject cardOptionsPrefab;

    public GameObject speechGamePosHolder;

    [SerializeField]
    private TextMeshPro completeSentenceHolder;

    [SerializeField] private GameObject SpeechGameObj;
    [SerializeField] private GameObject UIIcon;
    //[SerializeField] private List<GameObject> parallexObj;

    // public bool inventOn;
    // public bool cardGameOn;
    public GameObject floor;
    public GameObject switchButton;
    public GameObject switchTrigger;

    public bool camFollow;

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

        icState = ICState.Start;
    }

    public void MakeCardDragDesAvailable(string type, GameObject card)
    {
        switch (type)
        {
            case "SpeechCard":
                SpeechGameObj.SetActive(true);
                card.GetComponent<DragBehavior>().availableDesPosHolders.Add(SpeechGameObj.transform.GetChild(0).gameObject);
                break;
        }
    }

    private void Start()
    {
        if (GameManager.instance.isTutorial)
        {
            switchTrigger.SetActive(false);
            MakeSwitchUnavailble();
        }
    }

    private void OnEnable()
    {
        GameManager.instance.tutorialEnded += MakeTriggerAvailable;
    }

    private void OnDisable()
    {
        GameManager.instance.tutorialEnded -= MakeTriggerAvailable;
    }

    public void OpenExchangeGame(int gameNum)
    {
        //ChangeUIActivation(false);
        icState = ICState.ExchangeGameOpened;
        //DestroyCardInventPrefab();
        //GameObject speechObj = Instantiate(cardOptionsPrefab, speechGamePosHolder.transform.position, Quaternion.identity);
        //speechObj.transform.parent = speechGamePosHolder.transform;
        //cardGameOn = true;
        ExchangeGameManager.instance.StartExchangeGame(gameNum);
        //TurnOffInventory();
    }

    // void DestroyCardInventPrefab()
    // {
    //     cardInventHandPrefab.GetComponent<CardInventManager>().DestroySelfOnClose(); //TODO: will change to this after drag is switched to non-Fungus
    //     //cardInventHandPrefab.SetActive(false);
    //     //Mouse.instance.ChangeMouseInteraction(false);
    // }
    
    // public void TurnOnInventory()
    // {
    //     //Instantiate(cardInventHandPrefab); //TODO: will change to this after drag is switched to non-Fungus
    //     //cardInventHandPrefab.SetActive(true);
    //     //inventOn = true;
    //     //Mouse.instance.ChangeMouseInteraction(true);
    // }

    // public void TurnOffInventory()
    // {
    //     //cardInventHandPrefab.SetActive(false);
    //     //inventOn = false;
    // }

    // public void CardGameSubmission()
    // {
    //     cardGameOn = false;
    //     DialogueManager.instance.TriggerDialogueOOC("GameFinished");
    // }

    // public void SwitchInteractabilityForICObjects(bool interactability)
    // {
    //     switch (interactability)
    //     {
    //         case true:
    //             foreach (GameObject obj in ICPageInteractableObjects)
    //             {
    //                 obj.layer = LayerMask.NameToLayer("Interactable");
    //             }
    //             break;
    //         case false:
    //             foreach (GameObject obj in ICPageInteractableObjects)
    //             {
    //                 obj.layer = LayerMask.NameToLayer("Default");
    //             }
    //             break;
    //     }
    // }

    public void CameraFollow()
    {
        //IEnumerator coroutin = CamWait();
        //StartCoroutine(coroutin);
        if (camFollow) cam.GetComponent<CameraFollow>().CameraFollowing(actress.transform.position);
    }

    IEnumerator CamWait()
    {
        yield return new WaitForSeconds(1);
        cam.GetComponent<CameraFollow>().CameraFollowing(actress.transform.position);
    }

    public void CameraStopFollow()
    {
        //IEnumerator coroutin = CamStopWait();
        //StartCoroutine(coroutin);
        cam.GetComponent<CameraFollow>().CameraStopFollowing();
    }
    
    IEnumerator CamStopWait()
    {
        yield return new WaitForSeconds(1);
        cam.GetComponent<CameraFollow>().CameraStopFollowing();
    }

    public void CamAndActressStop()
    {
        cam.GetComponent<CameraFollow>().CameraStopFollowing();
        actress.GetComponent<ActressController>().StopActress();
        DestroyDesPosIcon();
    }

    public Transform ActressWalkingMisc(float xPos)
    {
        if (itemWithInteractionButtons != null)
        {
            // itemWithInteractionButtons.GetComponent<InteractiveItemsManager>().DeleteIcons2();
        }

        if (desPosIcon != null)
        {
            DestroyDesPosIcon();
        }

        desPosIcon = Instantiate(desPosIconPrefab);
        desPosIcon.transform.position = new Vector3(xPos, desPosIcon.transform.position.y, 0);
        desPosIcon.transform.parent = floor.transform;

        return desPosIcon.transform;
    }

    public void DestroyDesPosIcon()
    {
        Destroy(desPosIcon);
    }

    // public void ChangeUIActivation(bool state)
    // {
    //     if (icState != ICState.ExchangeGameOpened)
    //     {
    //         UIIcon.GetComponent<IconBehaviors>().IconState(state);
    //     }
    // }
    
    //new starting from this
    public void StartWordExchange(int gameNum)
    {
        OpenExchangeGame(gameNum);
        //shouldn't be walking
        //actress.GetComponent<ActressController>().DisableWalking();
    }

    public void EndExchange()
    {
        StartCoroutine(EndXChange());
        actress.GetComponent<ActressController>().EnableWalking();
    }

    IEnumerator EndXChange()
    {
        yield return new WaitForSeconds(2);
        ScalingManager.instance.DestroySelfOnClose();
        //adding the skill to game manager inventory later
        //GameManager.CardInventory newCard = new GameManager.CardInventory();
        //newCard.cardColorInvent = new Vector4(0,1,0,1);
        //newCard.cardNameInvent = "SpeechCard";
        //GameManager.instance.cardInventory.Add(newCard);
        
        //do the whole exchange body part animation thing
        //add to inventory anim/visual cue
        
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

    public void ChangeScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void MakeTriggerAvailable()
    {
        switchTrigger.SetActive(true);
    }

    public void MakeSwitchAvailable()
    {
        if (!switchButton.activeSelf) switchButton.SetActive(true);
    }

    public void MakeSwitchUnavailble()
    {
        if (switchButton.activeSelf) switchButton.SetActive(false);
    }
    
}
