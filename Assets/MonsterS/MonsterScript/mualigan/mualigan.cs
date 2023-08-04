using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mualigan : EnemyBase
{
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


    protected override void Update()
    {
        base.Update();
        Movement();
        Debug.Log($"{HeadTo}");
    }
    protected override void Movement()
    {
        transform.Translate(HeadTo * speed * Time.deltaTime);
        if (HeadTo.x > 1)
            {
                headsprite.flipX = false;
                bodysprite.flipX = false;
                animator.SetInteger("WalkSideway", 1);
            }
            else if (HeadTo.x < 1)
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
        { StartCoroutine(damaged(headsprite, bodysprite)); }
    }
}
