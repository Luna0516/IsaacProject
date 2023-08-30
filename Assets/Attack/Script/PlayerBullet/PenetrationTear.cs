using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationTear : AttackBase
{
    //is trigger 눈물 
    // 전체 관통 (적만) 
    // 트리거 
    
    public CircleCollider2D circleCollider;

    public bool isPenetrate = true;

    protected override void Awake()
    {
        base.Awake();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        circleCollider.enabled = false;
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
                }
            }
           
        }
    }

/*    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {           

            if (isPenetrate)
            {
               
                if(Penetration <= 0)
                {
                    base.OnCollisionEnter2D(collision);                   
                    TearDie();
                }
            }
        }
    }*/


}
