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
        Vector2 roomPos = room.RoomPosition();
        Vector3 movePos = new Vector3(roomPos.x, roomPos.y, transform.position.z);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }
}