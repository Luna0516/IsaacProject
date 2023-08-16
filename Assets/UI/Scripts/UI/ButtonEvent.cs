using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : EventTrigger {
    Image image;

    private void Awake() {
        image = transform.GetComponent<Image>();
    }

    public override void OnSelect(BaseEventData eventData) {
        base.OnSelect(eventData);
        //image.color.a = 0.1f;
    }

    public override void OnDeselect(BaseEventData eventData) {
        base.OnDeselect(eventData);
        //image.color = normalColor;
    }
}