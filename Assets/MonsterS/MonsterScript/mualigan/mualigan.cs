using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mualigan : EnemyBase
{
    Vector3 Headto;
    GameObject head;
    GameObject body;
    SpriteRenderer headsprite;
    SpriteRenderer bodysprite;
    Animator animator;

    protected override void Awake()
    {
        base.Awake();
        head = transform.GetChild(1).gameObject;
        body = transform.GetChild(0).gameObject;
        headsprite = head.GetComponent<SpriteRenderer>();
        bodysprite = body.GetComponent<SpriteRenderer>();
        animator = body.GetComponent<Animator>();
    }


    private void Update()
    {
        Movement();
    }
    protected override void Movement()
    {
        Headto = target.position - transform.position;
        transform.position += Headto.normalized * speed * Time.deltaTime;

        if (Headto.x > 1)
            {
                headsprite.flipX = false;
                bodysprite.flipX = false;
                animator.SetInteger("WalkSideway", 1);
            }
            else if (Headto.x < 1)
            {
                headsprite.flipX = true;
                bodysprite.flipX = true;
                animator.SetInteger("WalkSideway", 1);
            }
    }
    protected override void Hitten()
    {
        base.Hitten();
        StartCoroutine(damaged(headsprite,bodysprite));
    }




}
