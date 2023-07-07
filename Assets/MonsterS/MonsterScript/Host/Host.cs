using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : EnemyBase
{
    public GameObject bulletPrefab;
    GameObject childe;
    GameObject turret;
    Animator animator;
    SpriteRenderer spriteRenderer;
    int animestate;
    bool invincivle=false;




    protected override void Awake()
    {
		turret=transform.GetChild(0).gameObject;
		childe = transform.GetChild(1).gameObject;
        animator = childe.GetComponent<Animator>();
        spriteRenderer = childe.GetComponent<SpriteRenderer>();
        animestate = Animator.StringToHash("Attack");
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("트리거 작동");
        if (collision.CompareTag("Player"))
        {
            AttackMove();
        }
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
            StartCoroutine(damaged(spriteRenderer));
        }
    }
    void AttackMove()
    {
        StartCoroutine(attackCoroutine());
    }
    IEnumerator attackCoroutine()
    {
        invincivle = true;
        animator.SetInteger(animestate, 1);
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 3; i++)
        {
            bulletshotting(invincivle);
			yield return new WaitForSeconds(0.2f);
		}
		animator.SetInteger(animestate, 0);
        invincivle = false;
    }
    void bulletshotting(bool shotcool)
    {
        if(shotcool)
        { 
        turret.transform.rotation = Quaternion.LookRotation(Vector3.forward,target.position-transform.position);
        GameObject bullet = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
        }
    }
}
