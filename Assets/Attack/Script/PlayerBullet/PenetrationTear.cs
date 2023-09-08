using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationTear : AttackBase
{
    //is trigger 눈물 
    // 전체 관통 (적만) 
    // 트리거 

    public bool isPenetrate = true;

    protected override void Awake()
    {
        base.Awake();
        //circleCollider = GetComponent<CircleCollider2D>();
    }

    public int penetration = 3;
    public int Penetration
    {
        get => penetration;

        set
        {
            penetration = value;

            if (isPenetrate)
            {
                if (penetration <= 0)
                {
                    isPenetrate = false;
                    TearDie();
                }
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            penetration--;
            //damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            
            if (!isPenetrate)
            {
                TearExplosion();               
            }
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

  


}

// OnCollision override? Trigger로 설정되어있어서 어차피 작동 안됨.

// 수정할 것 
// Trigger로만 만든다
// Trigger 스크립트 재거
// 원본 트리거 설정