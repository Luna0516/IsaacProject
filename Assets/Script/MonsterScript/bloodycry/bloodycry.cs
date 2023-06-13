using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodycry : EnemyBase
{
    Vector3 Headto;
    GameObject head;
    GameObject body;
    SpriteRenderer headsprite;
    SpriteRenderer bodysprite;
    Animator headanimator;
    Animator bodyanimator;

    bool moveactive=false;

    private void Awake()
    {
        head = transform.GetChild(1).gameObject;
        body = transform.GetChild(0).gameObject;

        headsprite = head.GetComponent<SpriteRenderer>();
        bodysprite = body.GetComponent<SpriteRenderer>();
        headanimator = head.GetComponent<Animator>();
        bodyanimator = body.GetComponent<Animator>();
    }
    private void Update()
    {
        if (moveactive)
        {
            Movement();
        }
    }
    protected override void Movement()
    {
        Headto = target.position - transform.position;

        if (Headto.x > 1)
        {
            headsprite.flipX = false;
            bodysprite.flipX = false;
            bodyanimator.SetInteger("WalkSideway", 1);
            transform.position += Headto.normalized * speed * Time.deltaTime;
        }
        else if (Headto.x < 1)
        {
            headsprite.flipX = true;
            bodysprite.flipX = true;
            bodyanimator.SetInteger("WalkSideway", 1);
            transform.position += Headto.normalized * speed * Time.deltaTime;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            moveactive=true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            StartCoroutine(hittedanime());
        }
    }
    IEnumerator hittedanime()
    {
        headanimator.SetInteger("state", 1);
        yield return new WaitForSeconds(0.2f);
        headanimator.SetInteger("state", 2);
    }

}
