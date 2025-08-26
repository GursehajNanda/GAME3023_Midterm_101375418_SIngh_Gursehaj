using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    Transform inventoryPanel;

   
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
        if (itemSlot.GetItemCount() <= 1) return;

        RefreshInventory();

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null)
            {
                int total = itemSlot.GetItemCount();

                int half1 = Mathf.CeilToInt(total / 2f); 
                int half2 = total - half1;             

                itemSlot.AddIteminTheSlotWithItem(itemSlot.item, half2, slot.transform);
                itemSlot.SetItemCount(half1);
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
