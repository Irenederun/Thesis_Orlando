using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGamePage : ManagerBehavior
{
    public GameObject cardObj;
    public TextMeshPro textBlock;
    public GameObject canvas;
    private int wordsCollected = 0;
    public GameObject cardSprite;

    void Start()
    {
        IEnumerator coroutine = StartDialogue();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        if (DialogueManager.instance.isTalking)
        {
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
            }
        }
        else
        {
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
    }

    public void Button_I(string buttonTag)
    {
        textBlock.text = textBlock.text.Replace("<color=#FF0000>I</color>", "I");
        textBlock.text = textBlock.text.Replace("<color=#FF0000>me</color>", "me");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag(buttonTag);
        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        CheckCollection();
    }

    public void Button_You(string buttonTag)
    {
        textBlock.text = textBlock.text.Replace("<color=#FF0000>you</color>", "you");
        textBlock.text = textBlock.text.Replace("<color=#FF0000>You</color>", "You");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag(buttonTag);
        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        CheckCollection();
    }
    
    public void Button_Cruel(string buttonTag)
    {
        textBlock.text = textBlock.text.Replace("<color=#FF0000>cruel</color>", "cruel");
        textBlock.text = textBlock.text.Replace("<color=#FF0000>cruelty</color>", "cruelty");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag(buttonTag);
        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        CheckCollection();
    }
    
    public void Button_Deceit(string buttonTag)
    {
        textBlock.text = textBlock.text.Replace("<color=#FF0000>deceit</color>", "deceit");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag(buttonTag);
        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = false;
        }
        CheckCollection();
    }
    
    private void CheckCollection()
    {
        wordsCollected++;
        if (wordsCollected == 4)
        {
            CardActivated();
        }
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.TriggerDialogueOOC("CardMiniGameStart");
    }

    private void CardActivated()
    {
        DialogueManager.instance.TriggerDialogueOOC("CardMiniGameWon");
        cardSprite.GetComponent<SpriteRenderer>().color = Color.magenta;//TODO: will be sprite change - flipping?
        StartCoroutine(TriggerEnding());
    }

    IEnumerator TriggerEnding()
    {
        yield return new WaitForSeconds(1);
        
        while (DialogueManager.instance.isTalking)
        {
            yield return null;
        }
        
        OOCManager.instance.CardActivated(cardObj);
        DestroySelfOnClose();
    }


    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        OOCManager.instance.KeepInteractabilityForUI(true);
        cardObj.layer = LayerMask.NameToLayer("Interactable");
        Destroy(gameObject);
    }
}
