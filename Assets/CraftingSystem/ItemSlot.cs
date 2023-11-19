using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector]
    public Item item = null;
    private int count = 0;
  
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;
    
   

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            UpdateGraphic();
        }
    }

   
    // Start is called before the first frame update
    void Start()
    {
       item = GetComponentInChildren<Item>();
       if(item)
       {
         count = item.scriptableItem.itemcount;
         UpdateGraphic();
       }
        
    }

    private void Update()
    {
        //Refresh Item
        item = GetComponentInChildren<Item>();
    }

    //Change Icon and count
    void UpdateGraphic()
    {
        

        if (count < 1)
        {
            item = null;
            item.GetIcon().gameObject.SetActive(false);
            item.GetItemCountText().gameObject.SetActive(false);
        }
        else
        {
            //set sprite to the one from the item
            item.GetIcon().sprite = item.scriptableItem.icon;
            item.GetIcon().gameObject.SetActive(true);
            item.GetItemCountText().gameObject.SetActive(true);
            item.GetItemCountText().text = count.ToString();
        }
    }

    public void UseItemInSlot()
    {
        if (CanUseItem())
        {
            item.scriptableItem.Use();
            if (item.scriptableItem.isConsumable)
            {
                Count--;
            }
        }
    }

    private bool CanUseItem()
    {
        return (item != null && count > 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = item.scriptableItem.description;
            nameText.text = item.name;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Item draggableItem = dropped.GetComponent<Item>();
        draggableItem.parentAfterDrag = transform;
    }

   
}
