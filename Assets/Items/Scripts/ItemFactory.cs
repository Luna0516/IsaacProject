using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    /// <summary>
    /// 아이템 생성 함수
    /// </summary>
    /// <param name="itemData">생성할 아이템 데이터</param>
    /// <returns></returns>
    public GameObject CreateItem(ItemData itemData) {
        GameObject itemObj = GameObject.Instantiate(itemData.itemPrefab);

        ItemDataObject itemDataObject = itemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = itemData;

        return itemObj;
    }

    /// <summary>
    /// 아이템 생성 함수
    /// </summary>
    /// <param name="itemData">생성할 아이템 데이터</param>
    /// <param name="position">아이템 생성 위치</param>
    /// <returns></returns>
    public GameObject CreateItem(ItemData itemData, Vector3 position) {
        GameObject itemObj = CreateItem(itemData);

        itemObj.transform.position = position;

        return itemObj;
    }

    public GameObject CreateActiveItem(ActiveItem activeItemCode)
    {
        ActiveItemData activeItem = GameManager.Inst.ItemData.GetActiveItemData(activeItemCode);

        GameObject activeItemObj = GameObject.Instantiate(activeItem.itemPrefab);

        ItemDataObject itemDataObject = activeItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = activeItem;

        return activeItemObj;
    }

    public GameObject CreatePassiveItem(PassiveItem passiveItemCode)
    {
        PassiveItemData passiveItem = GameManager.Inst.ItemData.GetPassiveItemData(passiveItemCode);

        GameObject activeItemObj = GameObject.Instantiate(passiveItem.itemPrefab);

        ItemDataObject itemDataObject = activeItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = passiveItem;

        return activeItemObj;
    }

    public GameObject CreateHeartItem(HeartItem heartItemCode)
    {
        HeartItemData heartItem = GameManager.Inst.ItemData.GetHeartItemData(heartItemCode);

        GameObject activeItemObj = GameObject.Instantiate(heartItem.itemPrefab);

        ItemDataObject itemDataObject = activeItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = heartItem;

        return activeItemObj;
    }
}