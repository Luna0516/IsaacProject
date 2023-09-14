using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 100.0f;

    private void Start()
    {
        RoomManager.Inst.onChangeRoom += (room) => 
        {
            StopAllCoroutines();

            StartCoroutine(CameraMove(room));
        };
    }

    IEnumerator CameraMove(Room room)
    {
        Vector3 movePos = MovePosition(room);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// 현재 방의 위치 구하는 함수
    /// </summary>
    private Vector3 MovePosition(Room currentRoom)
    {
        Vector3 currentRoomPos = new Vector3(currentRoom.MyPos.x * currentRoom.width, currentRoom.MyPos.y * currentRoom.height, transform.position.z);

        return currentRoomPos;
    }
}