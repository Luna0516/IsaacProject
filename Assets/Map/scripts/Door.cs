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
    /// 문이 열려 있는지 확인하는 변수
    /// </summary>
    bool isOpen = false;
    /// <summary>
    /// 문의 열림 상태를 설정하는 프로퍼티
    /// </summary>
    public bool IsOpen
    {
        get => isOpen;
        set
        {
            if (!isOpen)
            {
                isOpen = value;

                sprite.sprite = openDoor[(int)doorType];
            }
        }
    }

    public Sprite[] openDoor;
    public Sprite[] closeDoor;

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

    SpriteRenderer sprite;
    BoxCollider2D coll;

    WaitForSeconds wait = new WaitForSeconds(0.5f);

    /// <summary>
    /// 플레이어가 문을 통해 이동했다는 것을 알리는 델리게이트
    /// </summary>
    public Action<DoorType> onPlayerMove;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOpen && collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerMoveRoutine(collision));
        }
    }

    IEnumerator PlayerMoveRoutine(Collider2D collision)
    {
        onPlayerMove?.Invoke(doorType);

        collision.enabled = false;
        yield return wait;
        collision.enabled = true;
    }
}
