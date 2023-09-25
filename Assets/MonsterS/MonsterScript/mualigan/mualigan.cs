using System;
using UnityEngine;

public class mualigan : EnemyBase
{
    GameObject head;
    GameObject body;
    SpriteRenderer headsprite;
    SpriteRenderer bodysprite;
    Animator animator;

    Action updateCheacker;

    protected override void Awake()
    {
        base.Awake();
        head = transform.GetChild(1).gameObject;
        body = transform.GetChild(0).gameObject;
        headsprite = head.GetComponent<SpriteRenderer>();
        bodysprite = body.GetComponent<SpriteRenderer>();
        animator = body.GetComponent<Animator>();
        updateCheacker = wewantnoNull;
    }
    protected override void Update()
    {
        base.Update();
        orderInGame(headsprite, bodysprite);
        damageoff(headsprite, bodysprite);
        HeadToCal();
    }
    private void FixedUpdate()
    {
        updateCheacker();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            updateCheacker += Movement;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            updateCheacker -= Movement;
        }
    }
    protected override void Movement()
    {
        transform.Translate(-HeadTo * speed * Time.fixedDeltaTime);
        if (HeadTo.x < 0)
        {
            headsprite.flipX = false;
            bodysprite.flipX = false;
            animator.SetInteger("WalkSideway", 1);
        }
        else
        {
            headsprite.flipX = true;
            bodysprite.flipX = true;
            animator.SetInteger("WalkSideway", 1);
        }
    }
    protected override void Hitten()
    {
        base.Hitten();
        if (this.gameObject.activeSelf)
        {
            damaged(headsprite, bodysprite);
        }
    }
}
