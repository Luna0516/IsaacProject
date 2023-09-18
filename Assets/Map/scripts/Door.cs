using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 문의 타입
/// </summary>
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
    /// 자신의 문의 타입
    /// </summary>
    DoorType doorType = DoorType.None;
    /// <summary>
    /// 자신의 문의 타입을 설정하는 프로퍼티(처음 생성될 때 한번만 설정 가능)
    /// </summary>
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
