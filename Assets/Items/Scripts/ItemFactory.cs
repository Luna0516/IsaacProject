using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    /// <summary>
    /// 액티브 아이템 생성 함수
    /// </summary>
    /// <param name="activeItemCode">생성할 액티브 아이템 코드</param>
    /// <returns>액티브 아이템 프리펩</returns>
    public GameObject CreateActiveItem(ActiveItem activeItemCode)
    {
        ActiveItemData activeItem = ItemDataManager.Inst.GetActiveItemData(activeItemCode);

        GameObject activeItemObj = GameObject.Instantiate(activeItem.itemPrefab);

        ItemDataObject itemDataObject = activeItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = activeItem;

        return activeItemObj;
    }

    /// <summary>
    /// 액티브 아이템 생성 함수 (위치 지정)
    /// </summary>
    /// <param name="activeItemCode">생성할 액티브 아이템 코드</param>
    /// <param name="pos">생성할 액티브 아이템 위치</param>
    /// <returns>액티브 아이템 프리펩</returns>
    public GameObject CreateActiveItem(ActiveItem activeItemCode, Vector2 pos)
    {
        GameObject activeItemObj = CreateActiveItem(activeItemCode);

        activeItemObj.transform.position = pos;

        return activeItemObj;
    }

    /// <summary>
    /// 패시브 아이템 생성 함수
    /// </summary>
    /// <param name="passiveItemCode">생성할 패시브 아이템 코드</param>
    /// <returns>패시브 아이템 프리펩</returns>
    public GameObject CreatePassiveItem(PassiveItem passiveItemCode)
    {
        PassiveItemData passiveItem = ItemDataManager.Inst.GetPassiveItemData(passiveItemCode);

        GameObject passiveItemObj = GameObject.Instantiate(passiveItem.itemPrefab);

        ItemDataObject itemDataObject = passiveItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = passiveItem;

        return passiveItemObj;
    }

    /// <summary>
    /// 패시브 아이템 생성 함수 (위치 지정)
    /// </summary>
    /// <param name="activeItemCode">생성할 패시브 아이템 코드</param>
    /// <param name="pos">생성할 패시브 아이템 위치</param>
    /// <returns>패시브 아이템 프리펩</returns>
    public GameObject CreatePassiveItem(PassiveItem passiveItemCode, Vector2 pos)
    {
        GameObject passiveItemObj = CreatePassiveItem(passiveItemCode);

        passiveItemObj.transform.position = pos;

        return passiveItemObj;
    }

    /// <summary>
    /// 하트 아이템 생성 함수
    /// </summary>
    /// <param name="heartItemCode">생성할 하트 아이템 코드</param>
    /// <returns>하트 아이템 프리펩</returns>
    public GameObject CreateHeartItem(HeartItem heartItemCode)
    {
        HeartItemData heartItem = ItemDataManager.Inst.GetHeartItemData(heartItemCode);

        GameObject heartItemObj = GameObject.Instantiate(heartItem.itemPrefab);

        ItemDataObject itemDataObject = heartItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = heartItem;

        return heartItemObj;
    }

    /// <summary>
    /// 하트 아이템 생성 함수 (위치 지정)
    /// </summary>
    /// <param name="heartItemCode">생성할 하트 아이템 코드</param>
    /// <param name="pos">생성할 하트 아이템 위치</param>
    /// <returns>하트 아이템 프리펩</returns>
    public GameObject CreateHeartItem(HeartItem heartItemCode, Vector2 pos)
    {
        GameObject heartItemObj = CreateHeartItem(heartItemCode);

        heartItemObj.transform.position = pos;

        return heartItemObj;
    }

    /// <summary>
    /// 기타 아이템 생성 함수
    /// </summary>
    /// <param name="propsItemCode">생성할 기타 아이템 코드</param>
    /// <returns>기타 아이템 프리펩</returns>
    public GameObject CreatePropsItem(PropsItem propsItemCode)
    {
        PropsItemData propsItem = ItemDataManager.Inst.GetPropsItemData(propsItemCode);

        GameObject propsItemObj = GameObject.Instantiate(propsItem.itemPrefab);

        ItemDataObject itemDataObject = propsItemObj.GetComponent<ItemDataObject>();
        itemDataObject.ItemData = propsItem;

        return propsItemObj;
    }

    /// <summary>
    /// 기타 아이템 생성 함수 (위치 지정)
    /// </summary>
    /// <param name="propsItemCode">생성할 기타 아이템 코드</param>
    /// <param name="pos">생성할 기타 아이템 위치</param>
    /// <returns>기타 아이템 프리펩</returns>
    public GameObject CreatePropsItem(PropsItem propsItemCode, Vector2 pos)
    {
        GameObject propsItemObj = CreatePropsItem(propsItemCode);

        propsItemObj.transform.position = pos;

        return propsItemObj;
    }

    public GameObject CreateChest(Vector2 pos)
    {
        GameObject chestItemObj = Instantiate(ItemDataManager.Inst.chest);

        chestItemObj.transform.position = pos;

        return chestItemObj;
    }
}