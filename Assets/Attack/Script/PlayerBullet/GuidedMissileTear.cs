using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
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

    Vector2 targethorming;
    protected override void OnEnable()
    {
        base.OnEnable();

        target = null;
        isChase = false;
        targethorming = dir;
    }

    protected override void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + targethorming * speed * Time.fixedDeltaTime); // 눈물 날아가는 속도 및 방향
        rigidBody.velocity = new Vector3(targethorming.x * speed, targethorming.y * speed);                 // 눈물 velocity 적용

        if (isChase)
        {
            targethorming = (target.position - transform.position).normalized;
        }
        else
        {
            targethorming = dir;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (target == null && other.gameObject.CompareTag("Enemy"))
        {
            isChase = true;
            target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        isChase = false;
    }
    protected override void Init()
    {
        base.Init();
    }
}



