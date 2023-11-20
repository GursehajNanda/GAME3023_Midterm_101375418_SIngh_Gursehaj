using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    Transform inventoryPanel;

    float timer;
   
    private void Awake()
    {
        ItemSlot.SplitItemEvent += HandleSplitItemEvent;
    }

    private void InitializeInventory()
    {
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
    }

    void Start()
    {
        InitializeInventory();
    }



    private void HandleSplitItemEvent(ItemSlot itemSlot)
    {
        RefreshInventory();

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null)
            {
                itemSlot.AddIteminTheSlotWithItem(itemSlot.item, itemSlot.GetItemCount() / 2, slot.transform);
                itemSlot.SetItemCount(itemSlot.GetItemCount() / 2);
                Debug.Log("Did Split Element");
                break;
            }
        }

        
    }

    private void RefreshInventory()
    {
        itemSlots.Clear();
        InitializeInventory();
    }

   
    
}
