using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using static MonstroBoss;

public class MonstroBoss : EnemyBase
{
    public GameObject bulletPrefab;
	/// <summary>
	/// 애니메이터
	/// </summary>
	Animator animator;

    /// <summary>
    /// 속도 복사용 변수
    /// </summary>
    float sppeed;

    /// <summary>
    /// 패턴 선택 변수
    /// </summary>
    int randomPatt=0;

    //방향 선택 변수
    Vector2 HeadTo;

    /// <summary>
    /// 스프라이트 렌더러
    /// </summary>
    SpriteRenderer spriteRenderer;

    public Transform turret;

    protected override void Awake()
    {
        base.Awake();
        //애니메이터 불러오기
        animator = transform.GetComponentInChildren<Animator>();

        //스프라이트 렌더러 불러오기
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();

        turret = transform.GetChild(1);
    }
     void Start()
    {
        //속도 복사용 변수에 복사
        sppeed = speed;

        //패턴 선택 함수 실행
        selectpattern();
    }
    private void Update()
    { 
        //이동 함수 실행
        Movement();
    }

    //공격 받을경우 체력 깎임(EnemyBase 오버라이드)
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    /// <summary>
    /// 대기상태 동작 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator IDel()
    {       
        //움직이지 않기 때문에 speed는 0
        speed = 0;

        //애니메이션 플레이 타임만큼 대기(1.167초)
        yield return new WaitForSeconds(1.167f);

        //speed값 sppeed값에 복사
        speed = sppeed;

        //패턴 지정 함수 실행
        selectpattern();
    }

    /// <summary>
    /// 점프패턴 동작 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator jumping()
    {
        //3번 반복 반복문
        for (int i = 0; i < 03; i++)
        {
            //점프 준비 자세를 위한 speed값 0
            speed = 0; 

            //애니메이터 jump 실행
            animator.SetInteger("Jump", 1);

            //점프 대기 시간(0.5초)후 점프 실행
            yield return new WaitForSeconds(0.5f);
            //점프 실행 지금 하늘을 날고 있음

            //플레이어쪽으로 이동하면서 날아야 하니 speed값에 복사해두었던 sppeed값 대입
            speed = sppeed;

            //점프 활공 시간 대기
            yield return new WaitForSeconds(1.750f);
            //바닥에 착지한 상태임

            //바닥에 착지했음으로 speed값은 0
            speed = 0;

            //점프 종료
            animator.SetInteger("Jump", 0);

            //Idel 실행으로 점프 쿨타임
            yield return new WaitForSeconds(0.5f);

            //다음 상태를 위해 speed값에 다시 sppeed를 넣어서 speed 변수 원본으로 되돌린다.
            speed = sppeed;
        }
        //반복 종료 다음 패턴을 위해 패턴 지정 함수 실행
        selectpattern();
    }
    //점프 패턴 코루틴 종료

/// <summary>
/// 슈퍼점프 패턴 동작 코루틴
/// </summary>
/// <returns></returns>
    IEnumerator superJump()
    {
        //기본적인 매커니즘은 점프 패턴과 동일하나 반복은 하지 않는다.
            speed = 0;
            animator.SetInteger("SuperJump", 1);
            //점프 대기
            yield return new WaitForSeconds(0.5f);
            speed = sppeed;
            //점프
            yield return new WaitForSeconds(0.8f);
        //활공 도중에 플레이어 머리 위로 떨어질수 있도록 위치값을 타깃값으로 수정
            transform.position = target.position;
            yield return new WaitForSeconds(0.950f);
            speed = 0;
        //점프 종료
            Splashbullet();
			animator.SetInteger("SuperJump", 0);
            //Idel 실행으로 점프 쿨타임
            yield return new WaitForSeconds(1.167f);
            speed = sppeed;       
        selectpattern();
    }

    /// <summary>
    /// 공격패턴 동작 실행 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        //공격 애니메이션 실행
        animator.SetInteger("Attack", 1);

        //공격을 위해 이동을 멈춘다.
        speed = 0;
        //공격 하는동안 대기
        yield return new WaitForSeconds(0.5f);
        ShatteredBullet();
        yield return new WaitForSeconds(1.5f);

        //애니메이션 종료
        animator.SetInteger("Attack", 0);
        //speed값 복원
        speed = sppeed;

        //다음 패턴 선택 함수 실행
        selectpattern();
    }

    /// <summary>
    /// 몬스트로 이동 함수(오버라이드(베이스 없음))
    /// </summary>
    protected override void Movement()
    {
        //방향값 구하기
        HeadTo = target.position - transform.position;

        //정규화
        HeadTo = HeadTo.normalized;

        //플레이어를 향해 이동하는 식
        transform.position += Time.deltaTime * speed * new Vector3(HeadTo.x,HeadTo.y,0);

        //방향에 따라 스프라이트 렌더러의 Flip값을 수정하는 조건문
        if (HeadTo.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }


    /// <summary>
    /// 다음 패턴 선택 함수
    /// </summary>
    void selectpattern()
    { 
        //아직 끝나지 않은 모든 코루틴 정지
        StopAllCoroutines();

        //랜덤패턴 int 변수에 랜덤값 대입
        randomPatt = Random.Range(0, 101);
        //확률 조정용 변수
        int pattern = 0;

        if (randomPatt <10)
        {
            pattern = 0;
        }
        else if (randomPatt <30) 
        {
            pattern = 1;
        }
        else if(randomPatt<70)
        {
            pattern = 2;
        }
        else
        {
            pattern = 3;
        }
        //스위치에 랜덤값 넣고 각 패턴의 코루틴 실행
        switch (pattern) 
        {
            case 0:
                StartCoroutine(IDel());
                break;
            case 1:
                StartCoroutine(jumping());
                break;
            case 2:
                StartCoroutine(Attack());
                break;
            case 3:
                StartCoroutine(superJump());
                break;
            default:
                StartCoroutine(IDel());
                break;
        }
       
    }
	private void Splashbullet()
	{
		for (int i = 0; i < 10; i++)
		{
			float angle = i * 360f / 10;  // 각도 계산
			Quaternion rotation = Quaternion.Euler(0f, 0f, angle);  // 회전값 계산

			Vector3 spawnPosition = transform.position;  // 생성 위치 계산
			GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);  // 총알 생성
		}
	}
    void ShatteredBullet()
    {
        if (HeadTo.x < 0)
        { turret.rotation = Quaternion.Euler(0, 0, 90); }
        else
        { turret.rotation = Quaternion.Euler(0, 0, -90); }
        int randomshot = Random.Range(7, 15);
        for (int i = 0;  i < randomshot; i++)
        {
            float Shattering = Random.Range(-45, 46);
            Quaternion shotgack = Quaternion.Euler(0,0, Shattering);
            float randx = Random.Range(-0.5f, 0.6f);
            float randy = Random.Range(-0.5f, 0.6f);
            GameObject bullet = Instantiate(bulletPrefab,new Vector3(turret.transform.position.x+ randx, turret.transform.position.y+ randy,0), turret.rotation*shotgack);
        }
    }
	protected override void Hitten()
    {
        base.Hitten();
        StartCoroutine(damaged(spriteRenderer));
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y - 0.5f, 0), new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y - 0.5f, 0));
        Gizmos.DrawLine(new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y - 0.5f, 0), new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y + 0.5f, 0));
        Gizmos.DrawLine(new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y + 0.5f, 0), new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y + 0.5f, 0));
        Gizmos.DrawLine(new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y + 0.5f, 0), new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y - 0.5f, 0));
    }
}
