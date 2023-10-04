using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 문의 종류
/// </summary>
public enum DoorType
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class Door : MonoBehaviour
{
    /// <summary>
    /// 문의 종류
    /// </summary>
    DoorType doorType;
    /// <summary>
    /// 문의 종류 설정용 프로퍼티
    /// </summary>
    public DoorType DoorType
    {
        get => doorType;
        set
        {
            doorType = value;
            if(doorType == DoorType.None)
            {
                gameObject.SetActive(false);
            }
        }
    }

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

                if (DoorType != DoorType.None)
                {
                    sprite.sprite = openDoor[(int)DoorType - 1];
                }
            }
        }
    }

    /// <summary>
    /// 열려 있는 문의 스프라이트
    /// </summary>
    public Sprite[] openDoor;

    /// <summary>
    /// 닫혀 있는 문의 스프라이트
    /// </summary>
    public Sprite[] closeDoor;

    /// <summary>
    /// 플레이어가 문을 통해 이동 한 후 문의 콜라이더 비활성화 시간
    /// </summary>
    WaitForSeconds wait = new WaitForSeconds(0.5f);

    /// <summary>
    /// 플레이어가 문을 통해 이동했다는 것을 알리는 델리게이트
    /// </summary>
    public System.Action<DoorType> onPlayerMove;

    // 컴포넌트들
    BoxCollider2D coll;
    SpriteRenderer sprite;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 문이 열려있고 충돌한 콜라이더의 태그가 플레이어 이면
        if (IsOpen && collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerMoveRoutine());
        }
    }

    /// <summary>
    /// 플레이어 이동 시킨후 일시적으로 문의 콜라이더를 비활성화 시키는 코루틴
    /// </summary>
    IEnumerator PlayerMoveRoutine()
    {
        onPlayerMove?.Invoke(DoorType);

        coll.enabled = false;

        yield return wait;

        coll.enabled = true;
    }
}
