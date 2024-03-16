using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OOCManager : MonoBehaviour
{
    public static OOCManager instance;
    public GameObject switchButton;

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

    private void Start()
    {
        if (GameManager.instance.isTutorial)
        {
            //switchTrigger.SetActive(false);
            MakeSwitchUnavailble();
        }
        else
        {
            MakeSwitchAvailable();
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
        ExchangeGameManager.instance.StartExchangeGame(gameNum);
    }
    
    // public void CameraFollow()
    // {
    //     cam.GetComponent<CameraFollow>().CameraFollowing(actress.transform.position);
    // }
    //
    // public Transform ActressWalkingMisc(float xPos)
    // {
    //     if (desPosIcon != null)
    //     {
    //         DestroyDesPosIcon();
    //     }
    //
    //     desPosIcon = Instantiate(desPosIconPrefab);
    //     desPosIcon.transform.position = new Vector3(xPos, desPosIcon.transform.position.y, 0);
    //     desPosIcon.transform.parent = floor.transform;
    //
    //     return desPosIcon.transform;
    // }
    //
    // public void DestroyDesPosIcon()
    // {
    //     Destroy(desPosIcon);
    // }

    //new starting from this
    public void StartWordExchange(int gameNum)
    {
        OpenExchangeGame(gameNum);
    }
    
    public void MakeTriggerAvailable()
    {
        //switchTrigger.SetActive(true);
        MakeSwitchAvailable();
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
