using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [HideInInspector]
    public Item item = null;
  
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    private void Awake()
    {
        item = GetComponentInChildren<Item>();
        if (item != null)
        {
            item.InitializeItem();
            UpdateGraphic();
        }
    }


    private void Update()
    {
        item = GetComponentInChildren<Item>();
    }

  
    void UpdateGraphic()
    {
        if (item.GetItemCount() < 1)
        {
            Destroy(item.gameObject);
        }
        else
        {
            item.GetIcon().sprite = item.scriptableItem.icon;
            item.GetIcon().gameObject.SetActive(true);
            item.GetItemCountText().gameObject.SetActive(true);
            item.GetItemCountText().text = item.GetItemCount().ToString();
    
        }
    }

    public void UseItemInSlot()
    {
        if (CanUseItem())
        {
            item.scriptableItem.Use();
            if (item.scriptableItem.isConsumable)
            {
              //  Count--;
            }
        }
    }

    private bool CanUseItem()
    {
        return (item != null && item.GetItemCount() > 0);
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
        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
        }
        else
        {
            if (draggableItem.scriptableItem.isStakable)
            {
                if (draggableItem.scriptableItem.name == item.scriptableItem.name)
                {
                    item.SetItemCount(draggableItem.scriptableItem.itemcount + item.GetItemCount());
                    item.GetItemCountText().text = item.GetItemCount().ToString();
                    Destroy(draggableItem.gameObject);
                }
            }
        }
    }

   
}
