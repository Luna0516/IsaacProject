using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shit : EnemyBase
{
    Rigidbody rig;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 Headto;

    public float coolTime = 0.5f;

    float CoolTime
    {
        get 
        {          
            return coolTime - Time.deltaTime; 
        }
        set {
            float CTimeing = coolTime;
            if (coolTime < 0f)
            {
                coolTime = CTimeing;
            }
            }

    }
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
        animator.SetTrigger("Attack");
    }

    protected override void Die()
    {
        bloodshatter();
        gameObject.SetActive(false);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }

}
