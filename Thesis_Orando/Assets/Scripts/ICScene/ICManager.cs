using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ICManager : MonoBehaviour
{
    [SerializeField] private enum ICState
    {
        Start,
        CardUsed,
    }
    [SerializeField]
    private ICState icState;

    public List<GameObject> ICPageInteractableObjects;

    public static ICManager instance;

    public GameObject cardInventHandPrefab;
    public GameObject scriptPrefab;
    public GameObject cam;
    public GameObject actress;
    [HideInInspector] public GameObject itemWithInteractionButtons;

    public GameObject desPosIconPrefab;
    private GameObject desPosIcon;
    public GameObject cardOptionsPrefab;

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

    // Update is called once per frame
    void Update()
    {
        if (icState == ICState.Start)
        {
            while (!DragManagerFungus.instance.dragCompleted)
            {
                return;
            }
            CardUsed();
        }
    }

    void CardUsed()
    {
        //TurnOnScript();
        icState = ICState.CardUsed;
        DestroyCardInventPrefab();
        LoadCardOptions(); 
    }

    void DestroyCardInventPrefab()
    {
        //cardInventHandPrefab.GetComponent<CardInventManager>().DestroySelfOnClose(); //TODO: will change to this after drag is switched to non-Fungus
        cardInventHandPrefab.SetActive(false);
        Mouse.instance.ChangeMouseInteraction(false);
    }

    void TurnOnScript()
    {
        Instantiate(scriptPrefab);
        SwitchInteractabilityForICObjects(false);
    }

    void LoadCardOptions()
    {
        //load the floating things. I think I should load prefabs that carry the options?? canvas?
        Instantiate(cardOptionsPrefab);
    }

    public void TurnOnInventory()
    {
        //Instantiate(cardInventHandPrefab); //TODO: will change to this after drag is switched to non-Fungus
        cardInventHandPrefab.SetActive(true);
        Mouse.instance.ChangeMouseInteraction(true);
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
            itemWithInteractionButtons.GetComponent<InteractiveItemsManager>().DeleteIcons();
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
}
