using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private TextMeshProUGUI itemCountText;
    [HideInInspector] public Transform parentAfterDrag;
    public ScriptableItem scriptableItem;
    private int itemCount;

    public void InitializeItem()
    {
        image = GetComponent<Image>();
        itemCountText = GetComponentInChildren<TextMeshProUGUI>();
    }

   

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

    }


    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
        
    }

    

    

    public TextMeshProUGUI GetItemCountText()
    {
        return itemCountText;
    }

    public Image GetIcon()
    {
        return image;
    }

    public void SetIcon(Image icon)
    {
        image = icon;
    }

    public void SetCountText(string text)
    {
        itemCountText.SetText(text);
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public void SetItemCount(int count)
    {
        itemCount = count;
    }
}
