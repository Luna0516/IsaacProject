using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int MaxHP = 5;



    /// <summary>
    /// ü�°��� �����ϴ� ������Ƽ
    /// </summary>
    public int HP
    {
       get => MaxHP;
       protected set
        {
            if (MaxHP != value)
            {
                MaxHP = value;

                if (MaxHP <= 0)
                { 
                    MaxHP = 0;
                    Die();
                    //MaxHP�� -�� ���͹����� �׳� 0���� �����ϰ� �ش� ��ü�� ���̴� �Լ� ����
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
