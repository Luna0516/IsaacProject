using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;
    public float damage = 2; //�ӽ� ������
    public Vector3 dir = Vector3.zero;
    public float Damage
    {
        get { return damage; }
    }


    public Action<float> GiveDamage;//�ӽ�

    Animator anim;
    GameObject tearExplosion;

    protected virtual void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
    }

    public void Update()
    {
        dir = new Vector3(transform.position.x * speed * Time.deltaTime, transform.position.y * speed * Time.deltaTime, 0.0f); // ��, �Ʒ�, �� ������ Input�� ���� ���� ���� ���� 
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
