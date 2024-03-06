// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class InventoryManager : ManagerBehavior
// {
//     public GameObject cardInventPrefab;
//     public List<Transform> cardInventPositions;
//
//     void Start()
//     {
//         LoadInventoryItems();
//     }
//
//     public void LoadInventoryItems()
//     {
//         if (GameManager.instance.cardInventory.Count != 0)
//         {
//             for (int i = 0; i < GameManager.instance.cardInventory.Count; i++)
//             {
//                 GameObject newCardInvent = Instantiate(cardInventPrefab, cardInventPositions[i].position, cardInventPositions[i].rotation);
//                 newCardInvent.GetComponent<SpriteRenderer>().color = GameManager.instance.cardInventory[i].cardColorInvent;
//                 newCardInvent.name = GameManager.instance.cardInventory[i].cardNameInvent;
//                 newCardInvent.transform.SetParent(gameObject.transform);
//             }
//         }
//     }
//
//     public override void DestroySelfOnClose()
//     {
//         base.DestroySelfOnClose();
//         OOCManager.instance.SwitchInteractabilityForAll(true);
//         OOCManager.instance.KeepInteractabilityForUI(true);
//         Destroy(gameObject);
//     }
// }
