using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveInventory : MonoBehaviour {
    public const int passiveLength = 16;

    Image[] icons;

    int count = 0;

    private void Awake() {
        icons = new Image[passiveLength];
        for (int i = 0; i < passiveLength; i++) {
            icons[i] = transform.GetChild(i).GetChild(0).GetComponent<Image>();
        }
    }

    private void Start() {
        GameManager.Inst.Player.getPassiveItem += GetItem;
    }

    void GetItem(PassiveItemData itemData) {
        icons[count].sprite = itemData.icon;

        count++;
        count %= passiveLength;
    }
}