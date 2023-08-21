using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackBase : PooledObject
{
    
    public float speed = 1.0f;          // 총알 속도
    public float lifeTime = 5.0f;       // 총알 사거리 (지속시간)
    public float rangeToLife = 2.5f;    // 플레이어 눈물 사거리 lifeTime(초)으로 나눌 수치 
    public float dropDuration = 10.0f;  // 밑으로 떨어지는 시간
    public float startGravity = 0.8f;   // 중력적용 시점
    public float gravityScale = 3.0f;   // 중력 정도

    public Vector2 moveDir = Vector2.zero;  // 이동 방향
    public Vector2 dir = Vector2.right;     // 발사 방향
    protected Vector3 scale; //P.s총알의 크기를 저장할 Vector3 변수


    /// <summary>
    /// 컴포넌트들
    /// </summary>
    protected Player player;

    protected Rigidbody2D rigidBody;


    public float damage;    // 눈물 데미지

    /// <summary>
    /// 눈물 데미지 프로퍼티
    /// </summary>
    public float Damage     
    {
        get => damage;
        set
        {
            if(value < 0)
            {
                damage = 0; // 데미지 - 값으로 떨어지는 것 방지
            }
            
            damage = value;
        }
    }

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        scale = Vector3.one;// P.S생성시 눈물 폭발의 sclae값을 1,1,1로 담는 변수입니다.
    }
    protected virtual void OnEnable()
    {
        player = GameManager.Inst.Player;
        scale = Vector3.one;//P.s눈물 폭발 오브젝트의 sclae값을 1,1,1로 초기화를 해줍니다.
        Init();                                     // 눈물 세부사항 초기화
        StartCoroutine(Gravity_Life(lifeTime));     // 눈물 중력, 발사시간 코루틴
    }

    protected virtual void FixedUpdate()
    {                      
        rigidBody.MovePosition(rigidBody.position + dir * speed * Time.fixedDeltaTime); // 눈물 날아가는 속도 및 방향
        rigidBody.velocity = new Vector3(dir.x * speed, dir.y * speed);                 // 눈물 velocity 적용
    }

    /// <summary>
    /// 눈물 충돌 처리
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
        {
            StopAllCoroutines();
            TearDie();
        }
    }

    /// <summary>
    /// 눈물 삭제 처리 함수
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void TearDie()
    {
        this.TearExplosion();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 눈물 중력과 발사 적용 코루틴
    /// </summary>
    /// <param name="delay">발사 인터벌</param>
    /// <returns></returns>
    protected override IEnumerator Gravity_Life(float delay = 0.0f)
    {
        dropDuration = lifeTime * startGravity;                 // 눈물 중력 적용 되는 시간 (길이)
        yield return new WaitForSeconds(dropDuration);          
        
        rigidBody.gravityScale = gravityScale;                          // 눈물에 적용될 중력 수치
        yield return new WaitForSeconds(delay - dropDuration);

        TearExplosion();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 총알 세부정보 초기화
    /// </summary>
    protected virtual void Init()
    {
        speed = player.TearSpeed;
        this.Damage = player.Damage;
        lifeTime =  (player.Range/rangeToLife);
        moveDir = player.MoveDir;
        dir = player.AttackDir;
        rigidBody.gravityScale = 0.0f;
        dir += moveDir * 0.3f;
    }

    protected void TearExplosion()
    {      
        //P.s
        Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position, scale);
        //펙토리에서 크기값도 수정해서 뽑아줍니다. 그래서 폭발 오브젝트는 눈물의 크기와 동일하게 나와요.
    }

}

// + 속력이 빠를수록 gravity 추가 되는 함수 추가
// 속력, 중력, 중력적용 시점, 중력적용 길이
// 속력이 올라가면, 중력 +, 중력적용시점 -, 중력적용 길이 + 

// 1. 유도 기능, 관통 기능(먼저)
// 2. 플레이어 이동 방향에 따른 총알의 날아가는 패턴 변화..
// 3. 칼 