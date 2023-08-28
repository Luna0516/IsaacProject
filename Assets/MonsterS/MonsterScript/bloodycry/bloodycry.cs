using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodycry : EnemyBase
{
    GameObject head;
    GameObject body;
    SpriteRenderer headsprite;
    SpriteRenderer bodysprite;
    Animator headanimator;
    Animator bodyanimator;
    bool moveactive = false;
    bool buserkurcheck = false;
    protected override void Awake()
    {
        base.Awake();
        head = transform.GetChild(1).gameObject;
        body = transform.GetChild(0).gameObject;
        headsprite = head.GetComponent<SpriteRenderer>();
        bodysprite = body.GetComponent<SpriteRenderer>();
        headanimator = head.GetComponent<Animator>();
        bodyanimator = body.GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        orderInGame(headsprite, bodysprite);
        if (moveactive)
        {
            Movement();
        }
    }
    protected override void Movement()
    {
        transform.Translate(speed * Time.deltaTime * HeadTo);
        if (HeadTo.x > 0)
        {
            headsprite.flipX = false;
            bodysprite.flipX = false;
            bodyanimator.SetInteger("WalkSideway", 1);

        }
        else
        {
            headsprite.flipX = true;
            bodysprite.flipX = true;
            bodyanimator.SetInteger("WalkSideway", 1);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moveactive = true;
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (gameObject.activeSelf == true)
            {
                hittedanime();
            }
            else
            { }
        }
    }
    protected override void Hitten()
    {
        base.Hitten();
        buserkurcheck = true;
        if (this.gameObject.activeSelf)
        {
            dmaged(headsprite, bodysprite);
        }
    }


    void hittedanime()
    {
        moveactive = true;
        headanimator.SetInteger("state", 1);
        if(!buserkurcheck)
        {
            buserkurcheck = true;
            cooltimeStart(1);
            if (cooltimer1 > 0.2f)
            {
                headanimator.SetInteger("state", 2);
                cooltimeStop(1);
            }

        }

    }
}
