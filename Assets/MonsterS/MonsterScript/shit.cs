using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shit : EnemyBase
{
    Rigidbody rig;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 Headto;
    protected override void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Headto = (target.position-transform.position).normalized;
        if(Headto.x<0)
        {
            spriteRenderer.flipX = true;
        }
        else 
        { spriteRenderer.flipX = false; }

    }
    void Attack()
    {
        rig.AddForce(Headto);
    }
    protected override void Die()
    {
        base.Die();

    }
}
