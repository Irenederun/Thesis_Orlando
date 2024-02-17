using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInventManager : ManagerBehavior
{
    // [System.Serializable]
    // public struct CardInvent
    // {
    //     public string cardName;
    //     public Vector4 cardColor; //TODO: will be sprite later
    // }
    // public List<CardInvent> cardInvent = new List<CardInvent>();
    
    public GameObject cardInventPrefab;
    public List<Transform> cardInventPositions;

    public void Start()
    {
        // for (int i = 0; i < GameManager.instance.cardInventory.Count; i++)
        // {
        //     var card = new CardInvent();
        //     card.cardName = GameManager.instance.cardInventory[i].cardNameInvent;
        //     card.cardColor = GameManager.instance.cardInventory[i].cardColorInvent;
        //     cardInvent.Add(card);
        // }
        
        //LoadInventoryItems();//应该是这个but for the sake of my sanity先用fungus所以实际在用下面这行
        GameObject cardObj = transform.GetChild(0).gameObject;
        cardObj.GetComponent<SpriteRenderer>().color = GameManager.instance.cardInventory[0].cardColorInvent;
        //这个地方为什么他妈的不用我原来写的inventory的代码？？？？
    }
    
    public void LoadInventoryItems()
    {
        if (GameManager.instance.cardInventory.Count != 0)
        {
            for (int i = 0; i < GameManager.instance.cardInventory.Count; i++)
            {
                GameObject newCardInvent = Instantiate(cardInventPrefab, cardInventPositions[i].position, cardInventPositions[i].rotation);
                newCardInvent.GetComponent<SpriteRenderer>().color = GameManager.instance.cardInventory[i].cardColorInvent;
                newCardInvent.name = GameManager.instance.cardInventory[i].cardNameInvent;
                newCardInvent.transform.SetParent(gameObject.transform);
            }
        }
    }

    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        Destroy(gameObject);
    }
}
