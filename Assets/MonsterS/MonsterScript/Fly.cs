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
        coll = GetComponent<Collider2D>();
        rneder = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        Invic += invancivalcheck;
        base.OnEnable();
        speed = UnityEngine.Random.Range(0.5f, 2f);
        Invic += wewantnoNull;
        cooltimeStart(1, invincivalTime);
        Rnad = Vector2.zero;
    }
    protected override void OnDisable()
    {
        base.onDisable();
        Invic -= invancivalcheck;
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
        HeadToCal();
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
            }
        }
    }
    protected override void Die()
    {
        coll.isTrigger = true;
        animator.SetInteger("Dead", 1);
        cooltimeStart(2, 0.917f);
        Invic += Dieying;  
    }
    void Dieying()
    {
        if(!coolActive2)
        {
            this.gameObject.SetActive(false);
        }
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
