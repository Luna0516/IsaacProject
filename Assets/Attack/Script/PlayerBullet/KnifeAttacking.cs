using System.Collections;
using UnityEngine;

public class KnifeAttacking : AttackBase
{
    private Vector2 startPos;    // 투사체 발사 시 초기 위치
    private Vector2 targetPos;   // 투사체 목표 위치

    bool isMoving = true;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
    }
    protected override void Init()
    {
        base.Init();

        startPos = transform.position;
        targetPos = startPos * dir * player.Range;
        
        dir = (targetPos - startPos).normalized;
    }

    protected override void OnEnable()
    {
        //base.OnEnable();       
    }

    protected override void FixedUpdate()
    {
        Vector2 targetEnd = isMoving ? targetPos : startPos;

        float distanceToTarget = Vector3.Distance(transform.position, targetEnd);

        if (distanceToTarget > 0.1f) // 움직일 거리가 남았을 때만 이동
        {
            transform.position = Vector3.MoveTowards(transform.position, targetEnd, speed * Time.deltaTime);
        }
        else
        {
            transform.position = -dir * Time.deltaTime; // 이동할 거리가 없으면 방향을 바꾸어 줍니다.
        }
    }


    protected override IEnumerator Gravity_Life(float delay = 0)
    {
        return null;
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        //base.OnCollisionEnter2D(collision);
    }

    
}
