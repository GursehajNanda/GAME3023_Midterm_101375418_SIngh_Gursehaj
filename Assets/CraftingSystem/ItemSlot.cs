using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerDownHandler
{
    [HideInInspector]
    public Item item = null;
  
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;
    [SerializeField]
    private GameObject itemPrefab;

    public static event Action<ItemSlot> SplitItemEvent;
    private int count;

    private void Initializtion(int itemCount)
    {
        item = GetComponentInChildren<Item>();
        if (item != null)
        {
            item.InitializeItem();
            SetItemCount(itemCount);
            UpdateGraphic();
        }
    }

    private void Awake()
    {
        item = GetComponentInChildren<Item>();
        if (item != null)
        {
            item.InitializeItem();
            SetItemCount(item.scriptableItem.itemcount);
            UpdateGraphic();
        }
    }

    private void Update()
    {
        item = GetComponentInChildren<Item>();
    }

      
    public void AddIteminTheSlot(Item _item,int itemCount,Transform slot)
    {
        GameObject prefabObj = Instantiate(itemPrefab, slot);
        prefabObj.GetComponent<Item>().scriptableItem = _item.scriptableItem;
        prefabObj.GetComponent<Item>().InitializeItem();
        prefabObj.transform.parent.GetComponent<ItemSlot>().Initializtion(itemCount);
    }
  
    public void UpdateGraphic()
    {
        if (count < 1)
        {
            Destroy(item.gameObject);
        }
        else
        {
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
              //  Count--;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null  && count > 1)
            {
                SplitItemEvent?.Invoke(this);
            }
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
                    SetItemCount(draggableItem.scriptableItem.itemcount + GetItemCount());
                    item.GetItemCountText().text = count.ToString();
                    Destroy(draggableItem.gameObject);
                }
            }
        }
    }

    public void SetItemCount(int itemCount)
    {
        count = itemCount;
        UpdateGraphic();
    }

    public int GetItemCount()
    {
        return count;
    }

}
