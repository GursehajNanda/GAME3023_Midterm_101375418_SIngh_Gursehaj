using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerClickHandler
{
    [HideInInspector]
    public Item item = null;
  
    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private int count;
    [SerializeField]
    public bool m_isOutputSlot = false;

    public static event Action<ItemSlot> SplitItemEvent;
    

  
    public void Initializtion(int itemCount)
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
            item.SetItemCount(count);
            UpdateGraphic();
        }
    }

    private void Update()
    {
        item = GetComponentInChildren<Item>();
    }

      
    public void AddIteminTheSlotWithItem(Item _item,int itemCount,Transform slot)
    {
        GameObject prefabObj = Instantiate(itemPrefab, slot);
        prefabObj.GetComponent<Item>().scriptableItem = _item.scriptableItem;
        prefabObj.transform.parent.GetComponent<ItemSlot>().Initializtion(itemCount);
    }

    public void AddIteminTheSlotWithScriptableItem(ScriptableItem _item, int itemCount, Transform slot)
    {
        GameObject prefabObj = Instantiate(itemPrefab, slot);
        prefabObj.GetComponent<Item>().scriptableItem = _item;
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

   

   

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = item.scriptableItem.description;
            nameText.text = item.scriptableItem.name;
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

   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_isOutputSlot) return;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            SplitItem();  
        }
    }

    void SplitItem()
    {
        if (item != null && count > 1)
        {
            SplitItemEvent?.Invoke(this);
        }
    }


    public void OnDrop(PointerEventData eventData)
    {   
        GameObject dropped = eventData.pointerDrag;
        Item draggableItem = dropped.GetComponent<Item>();
       
        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
            count = draggableItem.GetItemCount();
        }
        else
        {
            if (m_isOutputSlot) return;
            if (draggableItem.scriptableItem.isStakable)
            {
                if (draggableItem.scriptableItem.name == item.scriptableItem.name)
                {
                    SetItemCount(draggableItem.GetItemCount() + GetItemCount());
                    item.GetItemCountText().text = count.ToString();
                    Destroy(draggableItem.gameObject);
                }
            }
        }
    }

    public void SetItemCount(int itemCount)
    {
        count = itemCount;
        item.SetItemCount(itemCount);
        UpdateGraphic();
    }

    public int GetItemCount()
    {
        return count;
    }

}
