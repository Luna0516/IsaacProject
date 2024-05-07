using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 100.0f;

    private void OnEnable()
    {
        RoomManager.Inst.onChangeRoom += MoveCamera;
    }

    private void OnDisable()
    {
        if (RoomManager.Inst != null)
        {
            RoomManager.Inst.onChangeRoom -= MoveCamera;
        }
    }

    /// <summary>
    /// 카메라 이동 함수
    /// </summary>
    /// <param name="room"></param>
    void MoveCamera(Room room)
    {
        StopAllCoroutines();
        StartCoroutine(CameraMove(room));
    }

    /// <summary>
    /// 카메라 이동 코루틴
    /// </summary>
    /// <param name="room">카메라를 이동시킬 방</param>
    IEnumerator CameraMove(Room room)
    {
        Vector2 roomPos = room.RoomPosition();
        Vector3 movePos = new Vector3(roomPos.x, roomPos.y, transform.position.z);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * moveSpeed);
            yield return null;

            if(transform.position == movePos) { break; }
        }
    }
}