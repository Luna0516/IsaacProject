using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager
{
    /// <summary>
    /// 아이템 생성 함수
    /// </summary>
    /// <param name="itemData">생성할 아이템 데이터</param>
    /// <returns></returns>
    public static GameObject CreateItem(ItemData itemData) {
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
    public static GameObject CreateItem(ItemData itemData, Vector3 position) {
        GameObject itemObj = CreateItem(itemData);

        itemObj.transform.position = position;

        return itemObj;
    }
}