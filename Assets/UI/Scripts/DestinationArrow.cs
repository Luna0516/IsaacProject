using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationArrow : MonoBehaviour
{
    Vector2Int bossPos;

    /// <summary>
    ///  회전할 각도
    /// </summary>
    float angle;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        RoomManager.Inst.onChangeRoom += ChangeDirection;
    }

    private void Start()
    {
        bossPos = RoomManager.Inst.BossRoom.MyPos;

        ChangeDirection(RoomManager.Inst.CurrentRoom);
    }

    private void OnDisable()
    {
        if (RoomManager.Inst != null)
        {
            RoomManager.Inst.onChangeRoom -= ChangeDirection;
        }
    }

    void ChangeDirection(Room currentRoom)
    {
        Vector2Int dir = bossPos - currentRoom.MyPos;

        // 초기 이미지 상태가 위를 향하고 있어서 z축으로 -90도 회전 시켜야 한다.
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        rect.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
