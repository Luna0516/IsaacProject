using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 여러명이 걸려도 한명이 먼저 걸리면 그 친구만 따라가게 수정 필요 
// bool 값을 사용하여 true 일떄는 다른 적을 추적하지 않는다 
public class GuidedMissileTear : AttackBase
{

    Transform target = null;
    bool isChase = false;

    protected override void  OnEnable()
    {
        base.OnEnable();

        target = null;    
        isChase = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (target !=null)
        {
            dir = (target.position - transform.position).normalized;
        } 
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(!isChase) 
            { 
                target = other.transform;
                isChase = true;
            }  
        }
    }
}



