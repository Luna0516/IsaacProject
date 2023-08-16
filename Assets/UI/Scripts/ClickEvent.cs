using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickEvent : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    Image arrowImage;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        arrowImage = child.GetComponent<Image>();
        
    }

    void Start()
    {
        arrowImage.enabled = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        arrowImage.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
