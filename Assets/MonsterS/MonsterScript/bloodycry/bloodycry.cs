using System;
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
    bool buserkurcheck = false;
    float plusdistance = 100;
    Action stateChanger;
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
     protected override void Start()
    {
        base.Start();
        stateChanger += distanceChack;
    }
    protected override void Update()
    {
        base.Update();
        orderInGame(headsprite, bodysprite);
        stateChanger();
        damageoff(headsprite, bodysprite);
        HeadToCal();
    }
    void distanceChack()
    {
        plusdistance = calcHeadTo.sqrMagnitude/distance;
        if (plusdistance <= distance)
        {
            stateChanger += Movement;
            stateChanger -= distanceChack;
        }
    }
    void bloodCryStates()
    {
        if (!coolActive1)
        {
            hittedanimeoff();
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
    /*    protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D (collision);
            if (collision.CompareTag("Player"))
            {
                moveactive = true;
            }
        }*/
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (this.gameObject.activeSelf && !buserkurcheck)//이 개체의 활성화 확인과 버서커 체크가 거짓일때 실행
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
        cooltimeStart(1, 0.2f);
        stateChanger += bloodCryStates;
        stateChanger += Movement;
        headanimator.SetInteger("state", 1);
    }

    void hittedanimeoff()
    {
        allcoolStop();
        headanimator.SetInteger("state", 2);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, plusdistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, distance);
    }
#endif
}
