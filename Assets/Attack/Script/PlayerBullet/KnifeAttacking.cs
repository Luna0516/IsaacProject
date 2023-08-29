using System.Collections;
using UnityEngine;

public class KnifeAttacking : AttackBase
{
    private Vector2 startPos;
    private Vector2 targetPos;
    private bool isReturning = false;

    protected override void Init()
    {
        base.Init();

        if(gameObject.CompareTag("Player"))
        {
            startPos = transform.position;
        }
       
        targetPos = startPos + dir * player.Range;

        // 초기화 시 발사 방향과 반대 방향으로 설정
        dir = -dir * startPos;
    }

    protected override void OnEnable()
    {
       
    }

    protected override void FixedUpdate()
    {
        Vector2 targetEnd = isReturning ? startPos : targetPos;

        float distanceToTarget = Vector3.Distance(transform.position, targetEnd);

        if (distanceToTarget > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetEnd, speed * Time.deltaTime);
        }
        else
        {
           
            isReturning = true;
            targetPos = startPos;
            
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
