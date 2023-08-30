using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    PassiveItemData itemData;

    Image image;
    CanvasGroup group;

    private void Awake()
    {
        image = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();
        ResetSlot();
    }

    /// <summary>
    /// 패시브 아이템을 먹으면 슬롯 세팅 함수
    /// </summary>
    /// <param name="_itemData">세팅할 아이템</param>
    public void SetItemData(PassiveItemData _itemData)
    {
        itemData = _itemData;
        image.sprite = itemData.icon;
        group.alpha = 1;
    }

    /// <summary>
    /// 슬롯에 저장된 정보 초기화
    /// </summary>
    public void ResetSlot()
    {
        itemData = null;
        image.sprite = null;
        group.alpha = 0;
    }
}