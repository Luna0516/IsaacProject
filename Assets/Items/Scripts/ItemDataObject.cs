using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataObject : MonoBehaviour
{
    /// <summary>
    /// 아이템 펙토리에서 생성되어 사용될 아이템 정보
    /// </summary>
    ItemData itemData = null;

    /// <summary>
    /// 외부에서 사용될 아이템데이터 프로퍼티(생성시 한번만 설정)
    /// </summary>
    public ItemData ItemData {
        get => itemData;
        set {
            if (itemData == null) {
                itemData = value;
            }
        }
    }
}
