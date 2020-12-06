using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustGridToSafeArea : MonoBehaviour
{
    RectTransform panel;
    Rect lastSafeArea = new Rect(0, 0, 0, 0);

    // Start is called before the first frame update
    void Awake()
    {
        panel = GetComponent<RectTransform>();
        Refresh();
    }

    void Refresh()
    {
        Rect safeArea = Screen.safeArea;
        if(safeArea != lastSafeArea)
        {
            ApplySafeArea(safeArea);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void ApplySafeArea(Rect r)
    {
        lastSafeArea = r;

        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }
}
