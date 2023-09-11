using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 카메라 이동 속도
    /// </summary>
    float moveSpeed = 200.0f;

    private void Start()
    {
        RoomManager.Inst.onChangeRoom += (currentRoom) =>
        {
            Vector3 moveRoomVec = MovePosition(currentRoom);
            
            StopAllCoroutines();

            StartCoroutine(MoveCamera(moveRoomVec));
        };
    }

    IEnumerator MoveCamera(Vector3 moveRoomVec)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveRoomVec, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// 카메라 이동 목적지 구하는 함수
    /// </summary>
    /// <returns>이동 목적지 벡터값</returns>
    private Vector3 MovePosition(Room currentRoom)
    {
        Vector3 targetPos = currentRoom.GetRoomCentre();

        targetPos.z = transform.position.z;

        return targetPos;
    }
}
