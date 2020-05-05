using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    private RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width * 0.4f, Screen.height * 0.4f);
        rect.anchoredPosition = new Vector2(-(Screen.width * 0.4f) / 2, 0);
    }
}
