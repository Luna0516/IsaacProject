using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;
    public int damage = 2; //�ӽ� ������
    public int Damage
    {
        get { return damage; }
    }


    public Action<int> GiveDamage;//�ӽ�

    Animator anim;
    GameObject tearExplosion;

    protected virtual void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.right); // ��, �Ʒ�, �� ������ Input�� ���� ���� ���� ���� 
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
        if (collision.gameObject.CompareTag("Enemy"))//�ӽ� �ۼ� �Ѿ� �ǰ�
        {
            GiveDamage?.Invoke(damage);
            Destroy(this.gameObject);

        }
    }

}
