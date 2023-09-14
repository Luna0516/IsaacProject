using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    left,
    right,
    top,
    bottom,
    None,
}
public class Door : MonoBehaviour
{
    /// <summary>
    /// 이 문의 타입 설정
    /// </summary>
    DoorType doorType = DoorType.None;
    public DoorType DoorType
    {
        get => doorType;
        set
        {
            if(doorType == DoorType.None)
            {
                doorType = value;
            }
        }
    }

    /// <summary>
    /// 플레이어가 문을 통해 이동했다는 것을 알리는 델리게이트
    /// </summary>
    public Action<DoorType> onPlayerMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onPlayerMove?.Invoke(doorType);
        }
    }
}
