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
    public float dropDistance = 1.0f;   // 밑으로 떨어지는 거리
    public float startGravity = 0.8f;   // 중력적용 시점

    public Vector2 moveDir = Vector2.zero;  // 이동 방향
    public Vector2 dir = Vector2.right;     // 발사 방향
    
    /// <summary>
    /// 컴포넌트들
    /// </summary>
    Player player;
    Rigidbody2D rigidBody;

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
    }
    private void OnEnable()
    {
        player = GameManager.Inst.Player;
        
        Init();                                     // 눈물 세부사항 초기화
        StartCoroutine(Gravity_Life(lifeTime));     // 눈물 중력, 발사시간 코루틴
    }

    private void FixedUpdate()
    {
        Rigidbody2D bullertRB = this.GetComponent<Rigidbody2D>();                       // 눈물 rigidbody
        bullertRB.MovePosition(bullertRB.position + dir * speed * Time.fixedDeltaTime); // 눈물 날아가는 속도 및 방향
        bullertRB.velocity = new Vector3(dir.x * speed, dir.y * speed);                 // 눈물 velocity 적용
    }

    /// <summary>
    /// 눈물 충돌 처리
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TearDie(collision);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            TearDie(collision);
        }
    }

    /// <summary>
    /// 눈물 삭제 처리 함수
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void TearDie(Collision2D collision)
    {
        if (lifeTime < 0)
        {
            Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position);
            gameObject.SetActive(false);
        }
        else
        {
            Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position);
            gameObject.SetActive(false);
        }
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
        
        rigidBody.gravityScale = 3.0f;                          // 눈물에 적용될 중력 수치
        yield return new WaitForSeconds(delay - dropDuration);
        
        Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 총알 세부정보 초기화
    /// </summary>
    private void Init()
    {
        this.Damage = player.Damage;
        lifeTime =  (player.range/rangeToLife);
        moveDir = player.MoveDir;
        dir = player.AttackDir;
        rigidBody.gravityScale = 0.0f; 
    }
}

// + 속력이 빠를수록 gravity 추가 되는 함수 추가