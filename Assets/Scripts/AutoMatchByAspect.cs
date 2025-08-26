using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class AutoMatchByAspect : MonoBehaviour
{
    [Header("Aspect range (W/H)")]
    public float minAspect = 1.333f;  // ~4:3 / iPad
    public float maxAspect = 2.167f;  // ~19.5:9 / 20:9 phones

    [Header("Match range (0=width, 1=height)")]
    public float matchAtMinAspect = 0.35f; // more width bias on squarish screens
    public float matchAtMaxAspect = 0.9f;  // more height bias on tall phones

    [Header("Smoothing")]
    public float dampTime = 0.1f;          // seconds (0 = instant)

    CanvasScaler scaler;
    float vel;

    void Awake()
    {
        scaler = GetComponent<CanvasScaler>();
        Apply(immediate: true);
    }

    void OnEnable() => Apply(immediate: true);
    void Update() => Apply();

    void OnRectTransformDimensionsChange() => Apply();

    void Apply(bool immediate = false)
    {
        if (!scaler) return;
        if (Screen.width <= 0 || Screen.height <= 0) return;

        float aspect = (float)Screen.width / Screen.height;
        float t = Mathf.InverseLerp(minAspect, maxAspect, aspect);
        float target = Mathf.Lerp(matchAtMinAspect, matchAtMaxAspect, t);

        if (immediate || dampTime <= 0f)
            scaler.matchWidthOrHeight = target;
        else
            scaler.matchWidthOrHeight = Mathf.SmoothDamp(
                scaler.matchWidthOrHeight, target, ref vel, dampTime);
    }
}
