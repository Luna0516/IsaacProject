using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationTear : AttackBase
{
    //is trigger ���� 
    // ��ü ���� (����) 

    protected override void Awake()
    {
        base.Awake();
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

    public bool isPenetrate = true;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(isPenetrate)
            {
                Penetration--;

                if(Penetration <= 0)
                {
                    TearDie(collision);
                    base.OnCollisionEnter2D(collision);
                }
            }
        }


    }

}
