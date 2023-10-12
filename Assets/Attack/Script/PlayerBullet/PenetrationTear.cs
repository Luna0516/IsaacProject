using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationTear : AttackBase
{
    /// <summary>
    /// 관통이 가능한 상태인지 확인
    /// </summary>
    protected bool isPenetrate = true;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// 관통 가능한 적 개수
    /// </summary>
    protected int penetration = 3;
    public int Penetration
    {
        get => penetration;

        set
        {
            penetration = value;

            if (isPenetrate)            // 관통 가능한 상태라면
            {
                if (penetration <= 0)   // 더이상 관통할 수 없을 때
                {
                    isPenetrate = false;    // 관통 불가라 알림
                    TearDie();              // 눈물 사라짐
                }
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   // 대상이 적 태그가 되어있다면
        {
            
            penetration--;  // 관통 가능 개수 감소

            if (!isPenetrate)   // 관통 불가 상태라면
            {
                TearExplosion();    // 눈물 터짐               
            }
        }
    }

}
