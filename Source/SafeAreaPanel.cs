using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaPanel : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        RefreshPanel(Screen.safeArea);
    }

    private void OnEnable()
    {
        SafeAreaDetection.OnSafeAreaChanged += RefreshPanel;
    }

    private void OnDisable()
    {
        SafeAreaDetection.OnSafeAreaChanged -= RefreshPanel;
    }

    void RefreshPanel(Rect safearea)
    {
        Vector2 anchorMin = safearea.position;
        Vector2 anchorMax = safearea.position + safearea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
