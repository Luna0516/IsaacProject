using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : EnemyBase
{
    /// <summary>
    /// 적이 사용할 총알 프리펩
    /// </summary>
    public GameObject bulletPrefab;

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
    bool invincivle=false;

    /// <summary>
    /// Awake 각 변수에 값 넣어주는 작업
    /// </summary>
    protected override void Awake()
    {
		turret=transform.GetChild(0).gameObject;
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

    /// <summary>
    /// 몬스터 개체가 공격받는 콜리젼
    /// </summary>
    /// <param name="collision">부딫힌 개체</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ///조건문 : 부딫힌 개체의 태그가 "PlayerBullet"일 경우, 그.리.고. 무적 판정이 True일 경우 작동합니다.
        if (collision.gameObject.CompareTag("PlayerBullet")&&invincivle)
        {
            ///collision의 Damage프로퍼티를 불러와서 damage 변수에 넣고 Enemy Base 클래스 변수에 적용합니다.
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            ///공격 받는 함수 호출
            Hitten();
        }
    }

    /// <summary>
    /// 공격 액션 함수
    /// </summary>
    void AttackMove()
    {
        ///공격 코루틴 실행
        StartCoroutine(attackCoroutine(invincivle));
    }


/// <summary>
/// 공격액션용 코루틴
/// </summary>
/// <param name="attackmode">이곳에 무적 상태 논리변수를 활용합니다.</param>
/// <returns></returns>
    IEnumerator attackCoroutine(bool attackmode)
    {
        //조건문 : attackmode 즉 invincivle값이 참이 아닐때(무적일때) 실행됩니다.
        if (attackmode != true)
        {
        //무적 상태를 풀고 
        invincivle = true;
        //애니메이터를 공격 상태로 만들고
        animator.SetInteger(animestate, 1);
        //0.8초 대기합니다.
        yield return new WaitForSeconds(0.8f);
        //대기 후에 3발의 탄환을 발사하는 반복문입니다.
        for (int i = 0; i < 3; i++)
        {
            bulletshotting(invincivle);
			yield return new WaitForSeconds(0.2f);
		}
        //발사 후에 공격 상태를 해제하고
		animator.SetInteger(animestate, 0);
        //무적상태로 돌입합니다.
        invincivle = false;
        }
    }
    /// <summary>
    /// 총알 발사 함수
    /// </summary>
    /// <param name="shotcool">이곳에 invincivle 논리변수를 활용합니다.</param>
    void bulletshotting(bool shotcool)
    {
        //무적이 아닐경우 총알을 발사합니다.
        if(shotcool)
        { 
            //터렛의 각도 지정 : 터렛은 플레이어를 겨냥합니다.
        turret.transform.rotation = Quaternion.LookRotation(Vector3.forward,target.position-transform.position);
            //총알 프리펩으로부터 게임 오브젝트를 생성하여 터렛의 위치와 각도에서 총알을 만들어냅니다.
        GameObject bullet = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
        }
    }

    /// <summary>
    /// 맞는 처리 함수
    /// </summary>
    protected override void Hitten()
    {
        base.Hitten();
        //맞았을때 스프라이트 렌더러가 붉은색으로 변합니다.
        StartCoroutine(damaged(spriteRenderer));
    }
}
