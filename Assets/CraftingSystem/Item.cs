using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private TextMeshProUGUI itemCountText;
    private int count = 0;

    [HideInInspector] public Transform parentAfterDrag;
    public ScriptableItem scriptableItem;

    public void InitializeItem()
    {
        image = GetComponent<Image>();
        itemCountText = GetComponentInChildren<TextMeshProUGUI>();
        SetItemCount(scriptableItem.itemcount);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public Image GetIcon()
    {
        return image;
    }

    public TextMeshProUGUI GetItemCountText()
    {
        return itemCountText;
    }

    public void SetIcon(Image icon)
    {
        image = icon;
    }

    public void SetCountText(string text)
    {
        itemCountText.SetText(text);
    }

    public void SetItemCount(int itemCount)
    {
        count = itemCount;
    }

    public int GetItemCount()
    {
        return count;
    }
}
