using System;
using UnityEngine;

public class Host : EnemyBase
{
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
    bool Invincivle
    {
        get 
        {
            return invincivle; 
        }
        set 
        {
            if (invincivle != value) 
            {
                attackCoroutine(invincivle);
            }
        }
    }


    float angle = 0.0f;
    Vector3 hunt;


    Action<SpriteRenderer> UpdateCheckerSP;
    Action UpdateChecker;
    /// <summary>
    /// Awake 각 변수에 값 넣어주는 작업
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animestate = Animator.StringToHash("Attack");
    }

    protected override void OnEnable()
    {
        allcoolStop();
        base.OnEnable();
        invincivle = false;
        UpdateCheckerSP += orderInGame;
        UpdateCheckerSP += damageoff;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        UpdateChecker = wewantnoNull;
        UpdateCheckerSP -= orderInGame;
        UpdateCheckerSP -= damageoff;
        allcoolStop();
    }
    protected override void Update()
    {
        base.Update();
        HeadToCal();
        UpdateCheckerSP(spriteRenderer);
        UpdateChecker();
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
            NuckBack(-HeadToNormal);
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            NuckBack(-HeadToNormal);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invincivle = true;
        }
    }
    /// <summary>
    /// 공격 함수
    /// </summary>
    /// <param name="attackmode"></param>
    void attackCoroutine(bool attackmode)
    {
        if (!attackmode)
        {
            invincivle = true;
            animator.SetInteger(animestate, 1);
            hunt = Quaternion.LookRotation(Vector3.forward, HeadToNormal).eulerAngles;
            angle = hunt.z;
            cooltimeStart(1, 0.8f);
            cooltimeStart(3, 1.4f);
            UpdateChecker += attackupdate;
        }
    }
    /// <summary>
    /// 업데이트에서 실행되는 감시 함수
    /// </summary>
    void attackupdate()
    {
        if (!coolActive3)
        {
            animator.SetInteger(animestate, 0);
            invincivle = false;
            UpdateChecker -= attackupdate;
            allcoolStop();
        }
        if (!coolActive1)
        {
            bulletshotting(invincivle, angle);
        }
    }
    /// <summary>
    /// 총알 발사 함수
    /// </summary>
    /// <param name="shotcool">이곳에 invincivle 논리변수를 활용합니다.</param>
    void bulletshotting(bool shotactive, float angle)
    {
        if (!solorActive && shotactive)
        {
            factory.GetObject(PoolObjectType.EnemyBullet, this.transform.position, Vector3.one * 1.3f, angle);
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
