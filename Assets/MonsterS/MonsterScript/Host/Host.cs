using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : EnemyBase
{
    /// <summary>
    /// 터렛
    /// </summary>
    GameObject turret;

    /// <summary>
    /// 애니메이터
    /// </summary>
    Animator animator;

    /// <summary>
    /// 스프라이트 렌더러
    /// </summary>
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// "공격"이라는 문자를 담고있는 int값
    /// </summary>
    int animestate;

    /// <summary>
    /// 무적 상태 판정 (주의 : false일때 무적입니다.)
    /// </summary>
    bool invincivle = false;

    bool attackactiveate = false;

    /// <summary>
    /// Awake 각 변수에 값 넣어주는 작업
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        turret = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animestate = Animator.StringToHash("Attack");
    }

    /// <summary>
    /// 플레이어가 공격범위 내로 들어왔다가 나갈때 트리거
    /// </summary>
    /// <param name="collision">부딫히는 대상</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        ///조건문 : collision의 태그가 Player일 경우
        if (collision.CompareTag("Player"))
        {
            ///공격 작동 함수
            AttackMove();
        }
    }

    protected override void Update()
    {
        base.Update();
        orderInGame(spriteRenderer);
        damageoff(spriteRenderer);
        attackupdate();
    }

    /// <summary>
    /// 몬스터 개체가 공격받는 콜리젼
    /// </summary>
    /// <param name="collision">부딫힌 개체</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ///조건문 : 부딫힌 개체의 태그가 "PlayerBullet"일 경우, 그.리.고. 무적 판정이 True일 경우 작동합니다.
        if (collision.gameObject.CompareTag("PlayerBullet") && invincivle)
        {
            ///collision의 Damage프로퍼티를 불러와서 damage 변수에 넣고 Enemy Base 클래스 변수에 적용합니다.
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            ///공격 받는 함수 호출
            Hitten();
            NuckBack(-HeadTo);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            NuckBack(-HeadTo);
        }
    }

    /// <summary>
    /// 공격 액션 함수
    /// </summary>
    void AttackMove()
    {
        attackCoroutine(invincivle);
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    /// <param name="attackmode"></param>
    void attackCoroutine(bool attackmode)
    {
        if (attackmode != true)
        {
            invincivle = true;
            animator.SetInteger(animestate, 1);
            cooltimeStart(1, 0.8f);
            cooltimeStart(3, 1.4f);
            attackactiveate = true;
        }
    }

    /// <summary>
    /// 업데이트에서 실행되는 감시 함수
    /// </summary>
    void attackupdate()
    {
        if (attackactiveate)
        {
            if (!coolActive3)
            {
                attackactiveate = false;
                animator.SetInteger(animestate, 0);
                invincivle = false;
                allcoolStop();
            }
            if (!coolActive1)
            {
                bulletshotting(invincivle);
            }
        }
    }
    /// <summary>
    /// 총알 발사 함수
    /// </summary>
    /// <param name="shotcool">이곳에 invincivle 논리변수를 활용합니다.</param>
    void bulletshotting(bool shotactive)
    {
        if (!solorActive && shotactive)
        {
            turret.transform.rotation = Quaternion.LookRotation(Vector3.forward, HeadTo);
            GameObject bullet = factory.GetObject(PoolObjectType.EnnemyBullet, turret.transform.position, turret.transform.rotation.z);
            cooltimeStart(5, 0.2f);
        }
    }
    /// <summary>
    /// 맞는 처리 함수
    /// </summary>
    protected override void Hitten()
    {
        base.Hitten();
        //맞았을때 스프라이트 렌더러가 붉은색으로 변합니다.
        damaged(spriteRenderer);
    }
}
