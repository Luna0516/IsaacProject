using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventory : MonoBehaviour
{
    int iconLength = 4;

    ActiveItem activeItem;
    Image icon;
    ItemBase[] items = new ItemBase[4];

    private void Awake() {
        icon = transform.GetChild(0).GetComponent<Image>();

        FindItem();
    }

    void FindItem() {
        items = FindObjectsOfType<ItemBase>();
        
        if(items != null) {
            foreach(ItemBase one in items) {
                if(one.activeItem == null) {
                    continue;
                }
                one.setItem += SetActive;
            }
        }
    }

    void SetActive(ActiveItem item) {        
        icon.sprite = item.Icon;
        icon.color = Color.white;

        if(item.CoolTime == 4) {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
