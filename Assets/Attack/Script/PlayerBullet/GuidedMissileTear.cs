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

    protected override void OnEnable()
    {
        base.OnEnable();

        target = null;
        isChase = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (target != null)
        {
            isChase = true;
            dir = (target.position - transform.position).normalized;
        }
        else
        {
            isChase = false;
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            target = other.transform;
            if (target == null) 
            {
                target = other.transform;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        TearDie();
        Debug.Log("나갔다.");
    }

    protected override void Init()
    {
        base.Init();
    }
}



