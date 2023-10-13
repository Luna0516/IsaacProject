using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTear : AttackBase
{
    /// <summary>
    /// 추적할 대상
    /// </summary>
    Transform target = null;

    /// <summary>
    /// 추적중이면 true, 아니면 false
    /// </summary>
    bool isChase = false;

    /// <summary>
    /// 추적할 방향
    /// </summary>
    Vector2 targetHoming;

    protected override void OnEnable()
    {
        base.OnEnable();

        target = null;
        isChase = false;
        targetHoming = dir;
    }

    protected override void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + targetHoming * speed * Time.fixedDeltaTime); // 눈물 날아가는 속도 및 방향
        rigidBody.velocity = new Vector3(targetHoming.x * speed, targetHoming.y * speed);        // 눈물 velocity 적용

        if (isChase)    // 추적중이라면
        {
            targetHoming = (target.position - transform.position).normalized;   // 추적 방향을 추적 대상 방향으로 
        }
        else
        {
            targetHoming = dir; // 추적할 대상이 없다면 일반적으로 날아감
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (target == null && other.gameObject.CompareTag("Enemy"))     // 추적 대상이 없고 추적 반경에 적이 있다면
        {
            isChase = true;             // 추적중이라 알리고
            target = other.transform;   // 추적 대상을 설정
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  // 추적 반경에서 대상이 사라지면
    {
        target = null;      // 추적 대상이 없고
        isChase = false;    // 추적중이 아니라고 알림
    }
    protected override void Init()
    {
        base.Init();
    }
}



