using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveInventory : MonoBehaviour
{
    int iconLength = 16;

    PassiveItem[] passiveItems;
    GameObject[] icons;
    ItemBase[] item;

    int count = 0;

    private void Awake() {
        passiveItems = new PassiveItem[iconLength];
        icons = new GameObject[iconLength];
        item = new ItemBase[4];

        GameManager.Inst.LoadItem += FindItem;

        for(int i = 0; i< iconLength; i++) {
            icons[i] = transform.GetChild(i).GetChild(0).gameObject;
        }

        FindItem();
    }

    void FindItem() {
        item = FindObjectsOfType<ItemBase>();
        if (item != null) {
            foreach (ItemBase one in item) {
                one.getItem += GetPassive;
            }
        }
    }

    void GetPassive(PassiveItem item) {
        if(count >= iconLength) {
            count = 0;
        }
        passiveItems[count] = item;
        icons[count].GetComponent<Image>().sprite = passiveItems[count].Icon;

        count += 1;
    }
}