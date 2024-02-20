using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class ICManager : MonoBehaviour
{
    public enum ICState
    {
        Start,
        CardUsed,
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
    [SerializeField] private List<GameObject> parallexObj;

    public bool inventOn;
    public bool cardGameOn;

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

    public void CardUsed()
    {
        ChangeUIActivation(false);
        icState = ICState.CardUsed;
        TurnOnSpeechGame();
    }

    void TurnOnSpeechGame()
    {
        LoadCardOptions(); 
        //DestroyCardInventPrefab();
        TurnOffInventory();
    }

    void DestroyCardInventPrefab()
    {
        cardInventHandPrefab.GetComponent<CardInventManager>().DestroySelfOnClose(); //TODO: will change to this after drag is switched to non-Fungus
        //cardInventHandPrefab.SetActive(false);
        //Mouse.instance.ChangeMouseInteraction(false);
    }

    void LoadCardOptions()
    {
        cardGameOn = true;
        GameObject speechObj = Instantiate(cardOptionsPrefab, speechGamePosHolder.transform.position, Quaternion.identity);
        speechObj.transform.parent = speechGamePosHolder.transform;
    }

    public void TurnOnInventory()
    {
        //Instantiate(cardInventHandPrefab); //TODO: will change to this after drag is switched to non-Fungus
        cardInventHandPrefab.SetActive(true);
        inventOn = true;
        //Mouse.instance.ChangeMouseInteraction(true);
    }

    public void TurnOffInventory()
    {
        cardInventHandPrefab.SetActive(false);
        inventOn = false;
    }

    public void CardGameSubmission()
    {
        cardGameOn = false;
        DialogueManager.instance.TriggerDialogueOOC("GameFinished");
    }

    public void SwitchInteractabilityForICObjects(bool interactability)
    {
        switch (interactability)
        {
            case true:
                foreach (GameObject obj in ICPageInteractableObjects)
                {
                    obj.layer = LayerMask.NameToLayer("Interactable");
                }
                break;
            case false:
                foreach (GameObject obj in ICPageInteractableObjects)
                {
                    obj.layer = LayerMask.NameToLayer("Default");
                }
                break;
        }
    }

    public void CameraFollow()
    {
        //IEnumerator coroutin = CamWait();
        //StartCoroutine(coroutin);
        cam.GetComponent<CameraFollow>().CameraFollowing(actress.transform.position);
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

    public void ActressWalkingMisc(float xPos)
    {
        if (itemWithInteractionButtons != null)
        {
            itemWithInteractionButtons.GetComponent<InteractiveItemsManager>().DeleteIcons2();
        }

        if (desPosIcon != null)
        {
            DestroyDesPosIcon();
        }

        desPosIcon = Instantiate(desPosIconPrefab);
        desPosIcon.transform.position = new Vector3(xPos, desPosIcon.transform.position.y, 0);
    }

    public void DestroyDesPosIcon()
    {
        Destroy(desPosIcon);
    }

    public void ChangeUIActivation(bool state)
    {
        if (icState != ICState.CardUsed)
        {
            UIIcon.GetComponent<IconBehaviors>().IconState(state);
        }
    }

    public void ChangeParallex(bool moving, bool isLeft)
    {
        foreach (var obj in parallexObj)
        {
            obj.GetComponent<ParallexScrolling>().changeMovingBool(moving, isLeft);
        }
    }
}
