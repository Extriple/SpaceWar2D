using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance { get; private set; }
    [SerializeField] private RectTransform canvasReactTransform;

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshProUGUI;
    private RectTransform backgroundRectTransform;
    private ToolTipTimer toolTipTimer;

    private void Awake()
    {
        Instance = this;
        
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        
        
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();

        if (toolTipTimer != null)
        {
            toolTipTimer.timer -= Time.deltaTime;
            if (toolTipTimer.timer < 0)
            {
                Hide();
            }

        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasReactTransform.localScale.x;
        rectTransform.anchoredPosition = anchoredPosition;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasReactTransform.rect.width)
        {
            anchoredPosition.x = canvasReactTransform.rect.width - backgroundRectTransform.rect.width;
            
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasReactTransform.rect.height)
        {
            anchoredPosition.y = canvasReactTransform.rect.height - backgroundRectTransform.rect.height;
            
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string toolTipText)
    {
        textMeshProUGUI.SetText(toolTipText);
        textMeshProUGUI.ForceMeshUpdate();

        Vector2 textSize = textMeshProUGUI.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;

    }

    public void Show(string ToolTipText, ToolTipTimer toolTipTimer = null)
    {
        this.toolTipTimer = toolTipTimer;
        gameObject.SetActive(true);
        SetText(ToolTipText);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class ToolTipTimer
    {
        public float timer;
    }
}
