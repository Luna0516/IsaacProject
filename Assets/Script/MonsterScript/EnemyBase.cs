using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int MaxHP = 5;
    int hp = 5;


    public int HP
    {
        get => hp;
    protected set 
        {            
            if (hp != value)
            {
                hp = value;
                if(hp <=0)
                {
                    hp = 0;

                    Die();
                }
            }

        }
    }

    protected virtual void Awake()
    {
     
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))   // 총알만 충돌 처리
        {
            Debug.Log($"{this.gameObject.name} 이 공격받았다. 남은 체력 {hp}");
            HP--;
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }


    protected virtual void Movement()
    {

    }
    protected virtual void OnDisable()
    {

    }
    

    


}
