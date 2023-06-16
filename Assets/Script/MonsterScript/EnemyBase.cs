using System;
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
            int damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            HP-= damage;
            Debug.Log($"{gameObject.name}이 {damage}만큼 공격받았다. 남은 체력: {HP}");
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
