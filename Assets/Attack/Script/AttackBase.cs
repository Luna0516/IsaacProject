using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;
    public int damage = 2; //임시 데미지
    public int Damage
    {
        get { return damage; }
    }


    public Action<int> GiveDamage;//임시

    Animator anim;
    GameObject tearExplosion;

    protected virtual void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.right); // 위, 아래, 양 옆으로 Input에 따라 공격 변경 예정 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Wall"))
        {
            tearExplosion.transform.SetParent(null);
            tearExplosion.transform.position = collision.contacts[0].point;
            tearExplosion.SetActive(true);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag ("Floor"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))//임시 작성 총알 피격
        {
            GiveDamage?.Invoke(damage);
            Destroy(this.gameObject);

        }
    }

}
