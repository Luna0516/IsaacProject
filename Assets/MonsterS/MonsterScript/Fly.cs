using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Fly : EnemyBase
{
    Vector2 Rnad;
    public float invincivalTime = 1f;
    bool invincival = false;
    Animator animator;
    SpriteRenderer rneder;
    Collider2D coll;


    /// <summary>
    /// 노이즈 무브 정도
    /// </summary>
    [Header("노이즈 무브")]
    public float noise = 5f;


    float X;
    float Y;
    Action Invic;

    protected override void Start()
    {
        base.Start();
        Invic += wewantnoNull;
        cooltimeStart(1, invincivalTime);
        Invic += invancivalcheck;
        coll = GetComponent<Collider2D>();
        rneder = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        speed = UnityEngine.Random.Range(0.5f, 2f);
        Rnad = Vector2.zero;
    }
    void noisyMove()
    {
        if (coll.isTrigger == false)
        {
            X = UnityEngine.Random.Range(-noise, noise + 0.1f);
            Y = UnityEngine.Random.Range(-noise, noise + 0.1f);
            Rnad.x = X;
            Rnad.y = Y;
            this.gameObject.transform.Translate(Time.deltaTime * speed * Rnad);
        }
    }
    protected override void Update()
    {
        base.Update();
        Invic();
        orderInGame(rneder);
        if (HeadTo.x > 0)
        {
            rneder.flipX = true;
        }
        else
        {
            rneder.flipX = false;
        }
        this.gameObject.transform.Translate(Time.deltaTime * speed * HeadTo);
        noisyMove();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincival)
        {
            if (collision.gameObject.CompareTag("PlayerBullet"))
            {
                damage = collision.gameObject.GetComponent<AttackBase>().Damage;
                Hitten();
                NuckBack(-HeadTo);
            }
        }
    }
    protected override void Die()
    {
        coll.isTrigger = true;
        animator.SetInteger("Dead", 1);
        Destroy(this.gameObject, 0.917f);
    }
    protected override void OnDisable()
    {
        Invic -= wewantnoNull;
    }
    void invancivalcheck()
    {
        if (!coolActive1)
        {
            invincival = !coolActive1;
            allcoolStop();
            Invic -= invancivalcheck;
        }
    }
}
