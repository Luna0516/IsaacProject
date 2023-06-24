using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int MaxHP = 5;
    float hp = 5.0f;

    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp <= 0)
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
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            float damage = collision.gameObject.GetComponent<Bullet>().damage;
            HP-= damage;
            Debug.Log($"{gameObject.name}�� {damage}��ŭ ���ݹ޾Ҵ�. ���� ü��: {HP}");
        }
    }

    protected virtual void Movement()
    {

    }
    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
