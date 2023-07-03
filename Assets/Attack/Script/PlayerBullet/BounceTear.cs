using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTear : AttackBase
{
    public Rigidbody2D rigid;
    float randomX, randomY;

    public int bounceNum = 3;

    
    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody2D>();
        StopAllCoroutines();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        
        randomX = Random.Range(-1.0f, 1.0f);
        randomY = Random.Range(-1.0f, 1.0f);

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))    
        {
                bounceNum--;
                Vector2 normal = Vector2.one * randomX * randomY;
                Vector2.Reflect(normal, collision.contacts[0].normal);
            
                
                //collision.contacts[0].normal;
                
                
                //Vector2 dir = new Vector2(randomX, randomY).normalized;
                rigid.AddForce(dir * speed);

        }
    }

    protected override void TearDie(Collision2D collision)
    {
        if(bounceNum == 0) 
        { 
            base.TearDie(collision);
        }
    }
}
