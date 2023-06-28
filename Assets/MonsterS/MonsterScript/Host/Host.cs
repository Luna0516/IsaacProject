using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : EnemyBase
{
    GameObject childe;
    Animator animator;
    SpriteRenderer spriteRenderer;
    AnimatorStateInfo stateInfo;
    int animestate;
    public Player player;
    bool invincivel = false;


    protected override void Awake()
    {
        childe = transform.GetChild(0).gameObject;
        animator = childe.GetComponent<Animator>();
        spriteRenderer = childe.GetComponent<SpriteRenderer>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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
        if (collision.transform.CompareTag("PlayerBullet") && invincivel)
        {
            Debug.Log("호스트가 공격받았다.");
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
            StartCoroutine(damaged(spriteRenderer));
        }
    }
    void AttackMove()
    {
        StartCoroutine(attackCoroutine());
        invincivel = true;
    }
    IEnumerator attackCoroutine()
    {
        animator.SetInteger(animestate, 1);
        yield return new WaitForSeconds(1.9f);
        animator.SetInteger(animestate, 0);
        invincivel = false;
    }

}
