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
    bool coolbloodcheck = false;
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
        if (coolbloodcheck)
        {
            buserkurcheck = !coolActive1;
        }

        if (buserkurcheck)
        {
            hittedanimeoff();
        }

        if (moveactive)
        {
            Movement();
        }


        damageoff(headsprite, bodysprite);

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
            if (this.gameObject.activeSelf == true && !buserkurcheck)//이 개체의 활성화 확인과 버서커 체크가 거짓일때 실행
            {
                hittedanime();
            }
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


    void hittedanime()
    {
        Debug.Log("크와아아앙 . 블러디 크라이가 울부짖었따");
        moveactive = true;
        headanimator.SetInteger("state", 1);
        cooltimeStart(1, 0.2f);
        coolbloodcheck = true;
    }

    void hittedanimeoff()
    {
        headanimator.SetInteger("state", 2);
    }
}
