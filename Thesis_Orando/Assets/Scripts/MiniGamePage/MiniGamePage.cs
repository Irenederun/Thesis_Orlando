using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePage : ManagerBehavior
{
    public GameObject cardObj;
    // Start is called before the first frame update
    void Start()
    {
        IEnumerator coroutine = StartDialogue();
        StartCoroutine(coroutine);
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.instance.TriggerDialogueOOC("CardMiniGameStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        OOCManager.instance.KeepInteractabilityForUI(true);
        cardObj.layer = LayerMask.NameToLayer("Interactable");
        Destroy(gameObject);
    }
}
