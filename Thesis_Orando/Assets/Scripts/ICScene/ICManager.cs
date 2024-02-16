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
        icState = ICState.CardUsed;
        DestroyCardInventPrefab();
        //TurnOnScript();
        TurnOnOptions();//load the floating things 
    }

    void DestroyCardInventPrefab()
    {
        //cardInventHandPrefab.GetComponent<CardInventManager>().DestroySelfOnClose(); //TODO: will change to this after drag is switched to non-Fungus
        cardInventHandPrefab.SetActive(false);
    }

    void TurnOnScript()
    {
        Instantiate(scriptPrefab);
        SwitchInteractabilityForICObjects(false);
    }

    void TurnOnOptions()
    {
        
    }

    public void TurnOnInventory()
    {
        //Instantiate(cardInventHandPrefab); //TODO: will change to this after drag is switched to non-Fungus
        cardInventHandPrefab.SetActive(true);
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
        IEnumerator coroutin = CamWait();
        StartCoroutine(coroutin);
    }

    IEnumerator CamWait()
    {
        yield return new WaitForSeconds(1);
        cam.GetComponent<CameraFollow>().CameraFollowing(actress.transform.position);
    }

    public void CameraStopFollow()
    {
        IEnumerator coroutin = CamStopWait();
        StartCoroutine(coroutin);
    }
    
    IEnumerator CamStopWait()
    {
        yield return new WaitForSeconds(1);
        cam.GetComponent<CameraFollow>().CameraStopFollowing();
    }
}
