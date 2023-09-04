using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_BackAndForward : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 이동 속도
    public float moveDistance = 5.0f; // 이동 거리

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + Vector3.right * moveDistance; // 이동 거리만큼 오른쪽으로 이동한 지점을 목표 지점으로 설정
    }

    private void Update()
    {
        // movingToEnd 값에 따라 이동 방향을 설정
        Vector3 targetPos = movingToEnd ? endPos : startPos;

        // 오브젝트를 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // 목표 지점에 도달했을 경우 이동 방향을 반전
        if (transform.position == targetPos)
        {
            movingToEnd = !movingToEnd;
        }
    }
}
