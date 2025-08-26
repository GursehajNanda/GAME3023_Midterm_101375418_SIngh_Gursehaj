using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform rt;
    Rect lastApplied = Rect.zero;

    void OnEnable()
    {
        rt = GetComponent<RectTransform>();
        Apply();
    }

    // Unity can call this very early, even before Awake/OnEnable sometimes.
    void OnRectTransformDimensionsChange()
    {
        if (!isActiveAndEnabled) return;
        Apply();
    }

    void Apply()
    {
        // Lazy-init in case a very-early call happened
        if (rt == null) rt = GetComponent<RectTransform>();
        if (rt == null) return; // nothing we can do

        // Avoid divide-by-zero during early frame
        if (Screen.width <= 0 || Screen.height <= 0) return;

        var sa = Screen.safeArea; // in absolute pixels

        // If nothing changed, skip
        if (sa == lastApplied) return;
        lastApplied = sa;

        // Convert to normalized anchors
        Vector2 min = sa.position;
        Vector2 max = sa.position + sa.size;
        min.x /= Screen.width; min.y /= Screen.height;
        max.x /= Screen.width; max.y /= Screen.height;

        rt.anchorMin = min;
        rt.anchorMax = max;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
