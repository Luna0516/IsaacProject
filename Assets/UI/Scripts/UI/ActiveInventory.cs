using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventory : MonoBehaviour {
    Image icon;

    private void Awake() {
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start() {
        GameManager.Inst.Player.getActiveItem += SetActive;
    }

    void SetActive(ActiveItemData item) {
        transform.GetChild(1).gameObject.SetActive(false);

        icon.sprite = item.icon;
        icon.color = Color.white;

        transform.GetChild(1).gameObject.SetActive(true);
    }
}
