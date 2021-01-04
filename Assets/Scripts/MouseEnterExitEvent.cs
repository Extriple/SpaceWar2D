using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public event EventHandler OnMouseEnter;
    public event EventHandler onMouseExit;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter ?.Invoke(this,EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       onMouseExit ?.Invoke(this, EventArgs.Empty);
    }
}
