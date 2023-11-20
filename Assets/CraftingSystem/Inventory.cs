using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    GameObject inventoryPanel;

    void Start()
    {
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );

        ItemSlot.SplitItemEvent += HandleSplitItemEvent;
    }

    private void HandleSplitItemEvent(ItemSlot itemSlot)
    {
        foreach(ItemSlot slot in itemSlots)
        {
            if(slot.item == null)
            {
                itemSlot.AddIteminTheSlot(itemSlot.item, itemSlot.GetItemCount()/2,slot.transform);
                itemSlot.SetItemCount(itemSlot.GetItemCount() / 2);
               // slot.UpdateGraphic();
                Debug.Log("Did Split Element");
                break;
            }
        }
    }

    private void RefreshInventory()
    {

    }

    private void Update()
    {
       //foreach
    }
}
