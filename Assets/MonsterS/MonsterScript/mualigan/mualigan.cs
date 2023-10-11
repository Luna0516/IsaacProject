using System;
using UnityEngine;

public class mualigan : EnemyBase
{
    /// <summary>
    /// 머리 오브젝트
    /// </summary>
    GameObject head;

    /// <summary>
    /// 몸 오브젝트
    /// </summary>
    GameObject body;

    /// <summary>
    /// 머리 스프라이트 렌더러
    /// </summary>
    SpriteRenderer headsprite;

    /// <summary>
    /// 몸 스프라이트 렌더러
    /// </summary>
    SpriteRenderer bodysprite;

    /// <summary>
    /// 애니메이터
    /// </summary>
    Animator animator;

    /// <summary>
    /// Update에서 돌아가는 델리게이트
    /// </summary>
    Action updateCheacker;

    /// <summary>
    /// head,body 게임 오브젝트를 자식에서 찾고 바로 스프라이트 렌더러를 자식으로부터 가져온다. 
    /// 그외 다른 컴포넌트들도 가져온다.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        head = transform.GetChild(1).gameObject;
        body = transform.GetChild(0).gameObject;
        headsprite = head.GetComponent<SpriteRenderer>();
        bodysprite = body.GetComponent<SpriteRenderer>();
        animator = body.GetComponent<Animator>();
        updateCheacker = wewantnoNull; // 업데이트에서 Null 방지용 빈 함수.
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

    /// <summary>
    /// 트리거에 플레이어가 들어오면 move함수 실행
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            updateCheacker += Movement;
        }
    }

    /// <summary>
    /// 트리거에서 플레이어가 나가면 move 함수를 델리게이트에서 제외
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            updateCheacker -= Movement;
        }
    }

    /// <summary>
    /// 플레이어 반대방향으로 이동하는 함수.(물리건만.)
    /// </summary>
    protected override void Movement()
    {
        transform.Translate(-HeadToNormal * speed * Time.fixedDeltaTime);
        if (HeadToNormal.x < 0)
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
    public override void Hitten()
    {
        base.Hitten();
        if (this.gameObject.activeSelf)
        {
            damaged(headsprite, bodysprite);
        }
    }
}
