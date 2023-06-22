using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;

    Animator anim;
    public GameObject tearExplosion;

    public Vector2 dir = Vector2.right;

    protected virtual void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
        
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * dir); // 위, 아래, 양 옆으로 Input에 따라 공격 변경 예정 
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TearDie(collision);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            TearDie(collision);
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(LifeOver(lifeTime)); 
    }
    public void TearDie(Collision2D collision)
    {
        if(lifeTime < 0) 
        {
            Destroy(gameObject);
        }
        else 
        { 
        tearExplosion.transform.SetParent(null);
        tearExplosion.transform.position = collision.contacts[0].point;
        tearExplosion.SetActive(true);
        Destroy(gameObject);
        }
    }

    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay); 
        Destroy(gameObject);            
    }
}
