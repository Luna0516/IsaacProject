using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : EnemyBase
{
    Vector3 headto;
    float invincivalTime = 2f;
    bool invincival=true;
    Animator animator;

    float InvincivalTime
    { 
        get 
        { 
            return invincivalTime; 
        } 
        set 
        {
            if(invincivalTime != value)
            {
            invincivalTime = value;
            if (invincivalTime < 0)
            {
            invincival = false;
            }
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();    
        speed = Random.Range(0.5f, 2.5f);
    }
    private void Update()
    {
        InvincivalTime -= Time.deltaTime;
        headto = (target.position - transform.position).normalized;
        this.gameObject.transform.Translate(Time.deltaTime*speed* headto);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(!invincival)
        {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
        }
        }
    }
    protected override void Die()
    {
        animator.SetInteger("Dead", 1);
        Destroy(this.gameObject,0.917f);
    }
}
