using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTear : AttackBase
{

    Transform target = null;
   

    private void OnEnable()
    {
        target = null;    
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
            target = other.transform;
        }
    }
}



