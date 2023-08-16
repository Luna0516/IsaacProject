using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveInven : MonoBehaviour
{
    /// <summary>
    /// 패시브 슬롯 길이(고정값)
    /// </summary>
    const int passiveSlotsLength = 12;

    Slot[] slots;

    int count = 0;

    private void Start()
    {
        slots = new Slot[passiveSlotsLength];

        Transform child;
        for (int i = 0; i < passiveSlotsLength; i++)
        {
            child = transform.GetChild(i);
            slots[i] = child.GetComponent<Slot>();
        }

        GameManager.Inst.Player.getPassiveItem += GetItem;
    }

    /// <summary>
    /// 플레이어가 패시브아이템을 먹으면 실행할 함수
    /// </summary>
    /// <param name="itemData">플레이어가 먹은 패시브 아이템</param>
    private void GetItem(PassiveItemData itemData)
    {
        slots[count].SetItemData(itemData);

        count++;
        count %= passiveSlotsLength;
    }

    /// <summary>
    /// 패시브 인벤토리 초기화 함수
    /// </summary>
    public void RestoreSlot()
    {
        for (int i = 0; i < passiveSlotsLength; i++)
        {
            slots[i].ResetSlot();
        }
    }
}
