using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    /// <summary>
    /// 몬스터 데미지
    /// </summary>
    [Header("몬스터 데미지")]
    public float MonsterDamage = 1;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager Manager;

    /// <summary>
    /// 플레이어
    /// </summary>
    Player player = null;

    /// <summary>
    /// 이동 목표(플레이어)
    /// </summary>
    protected Transform target;

    /// <summary>
    /// 방향 벡터
    /// </summary>
    protected Vector2 HeadTo;

    /// <summary>
    /// 몬스터 이동 속도
    /// </summary>
    [Header("몬스터 이동 속도")]
    public float speed = 5f;

    /// <summary>
    /// 넉백 파워
    /// </summary>
    [Header("넉백 파워")]
    public float NuckBackPower = 1f;

    /// <summary>
    /// 최대 체력
    /// </summary>
    [Header("최대 체력")]
    public float MaxHP = 5;

    /// <summary>
    /// 실제 체력
    /// </summary>
    float hp;

    /// <summary>
    /// 몬스터에게 들어오는 데미지
    /// </summary>
    protected float damage;

    /// <summary>
    /// 스폰 이펙트
    /// </summary>
    protected GameObject spawneffect;

    /// <summary>
    /// 리지디 바디
    /// </summary>
    protected Rigidbody2D rig;
    /// <summary>
    /// 체력값을 정의하는 프로퍼티
    /// </summary>
    public float HP
    {
        get => hp;
        protected set
        {
            if (hp != value)
            {
                hp = value;

                if (hp <= 0)
                {
                    hp = 0;
                    Die();
                    //MaxHP가 -가 나와버리면 그냥 0으로 지정하고 해당 개체를 죽이는 함수 실행
                }
            }
        }
    }

    public float hiittenEfeect = 0.3f;

    



    [Header("쿨타임 타이머 관련")]
    public float cooltimer1 = 0.0f;
    public float cooltimer2 = 0.0f;
    public float cooltimer3 = 0.0f;
    public float damagetimer = 0.0f;
    public bool coolActive1 = false;
    public bool coolActive2 = false;
    public bool coolActive3 = false;
    public bool damageActive = false;


    //에너미 베이스 Awake : 리지디 바디, 게임매니저 , 플레이어 , 타깃 위치 , 스폰 이펙트를 찾음
    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        Manager = GameManager.Inst;
        if (player == null)
        {
            player = Manager.Player; // 플레이어를 찾아서 할당
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("플레이어를 찾을수가 없습니다.");
        }
        spawneffect = transform.GetChild(2).gameObject;
    }

    //모든 적들은 콜리전이 부딫혔을때 그것이 총알이라면 데미지를 받고 넉백됨
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
            NuckBack(collision.contacts[0].normal);
        }
    }

    /// <summary>
    /// 관통 총알 (PenetrationTear)용 OnTrigger2D. (필요에 따라 유지 혹은 삭제 가능성 있습니다)
    /// </summary>
    /// <param name="collision"></param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
        }
    }

    protected virtual void OnEnable()
    {
        HPInitial();
        spawneffect.SetActive(true);
    }

    private void HPInitial()
    {
        hp = MaxHP;
    }

    protected virtual void Movement()
    {

    }
    protected virtual void Die()
    {
        bloodshatter();
        meatshatter();
        Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }

    protected virtual void bloodshatter()//피를 흩뿌리는 함수
    {
        int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EnemyBlood, bloodpos);
            //GameObject bloodshit = Instantiate(blood, bloodpos, Quaternion.identity);//bloodshit이라는 게임 오브젝트 생성 종류는 빈 게임 오브젝트, 위치는 bloodpos, 각도는 기존 각도
        }
    }
    void meatshatter()//고기를 흩뿌리는 함수
    {
        int meatCount = UnityEngine.Random.Range(3, 6);
        for (int i = 0; i < meatCount; i++)
        {
            GameObject meatshit = Factory.Inst.GetObject(PoolObjectType.EnemyMeat);
            meatshit.transform.position = this.transform.position;
            //GameObject meatshit = Instantiate(meat, transform.position, Quaternion.identity);
        }
    }
    protected virtual void Hitten()
    {
        HP -= damage;
        Debug.Log($"{gameObject.name}이 {damage}만큼 공격받았다. 남은 체력: {HP}");
    }
    protected virtual void Update()
    {
        coolTimeSystem(coolActive1, coolActive2, coolActive3, damageActive);
        HeadTo = (target.transform.position - this.gameObject.transform.position).normalized;
    }
    protected void NuckBack(Vector2 HittenHeadTo)
    {
        rig.isKinematic = false;
        rig.AddForce(HittenHeadTo * NuckBackPower, ForceMode2D.Impulse);
    }


    /// <summary>
    /// 피격 이펙트 시작 함수
    /// </summary>
    /// <param name="sprite">몬스터의 스프라이트렌더러</param>
    /// <param name="sprite1"></param>
    /// <returns></returns>
    /// 
    protected void damaged(SpriteRenderer sprite, SpriteRenderer sprite1)
    {
        cooltimeStart(4, hiittenEfeect);
        sprite.color = Color.red;
        sprite1.color = Color.red;
    }
    protected void damaged(SpriteRenderer sprite)
    {
        cooltimeStart(4, hiittenEfeect);
        sprite.color = Color.red;
    }

    /// <summary>
    /// 피격 이펙트 종료 함수 (업데이트에서 실행)
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="sprite1"></param>
    protected void damageoff(SpriteRenderer sprite, SpriteRenderer sprite1)
    {
        if(!damageActive)
        {
        sprite.color = Color.white;
        sprite1.color = Color.white;
        }
    }
    protected void damageoff(SpriteRenderer sprite)
    {
        if (!damageActive)
        {
            sprite.color = Color.white;
        }
    }

    protected void orderInGame(SpriteRenderer render)
    {
        float Yhi = this.transform.position.y;
        int total;
        if (Yhi < 0)
        {
            Yhi *= -1;
            total = (int)Mathf.Clamp(Mathf.Floor(Yhi + 20), 20, 30);
        }
        else
        {
            total = (int)Mathf.Clamp(Mathf.Floor(-Yhi + 20), 10, 20);
        }


        render.sortingOrder = total;
    }
    protected void orderInGame(SpriteRenderer head, SpriteRenderer body)
    {
        float Yhi = this.transform.position.y;
        int total;
        if (Yhi < 0)
        {
            Yhi *= -1;
            total = (int)Mathf.Clamp(Mathf.Floor(Yhi + 20), 20, 30);
        }
        else
        {
            total = (int)Mathf.Clamp(Mathf.Floor(-Yhi + 20), 10, 20);
        }


        head.sortingOrder = total + 1;
        body.sortingOrder = total;
    }



    protected void coolTimeSystem(bool time1, bool time2, bool time3 , bool damagecheck )
    {
        if (time1)
        {
            Debug.Log("1번 쿨타임 가동중");
            cooltimer1 -= Time.deltaTime;
            if(cooltimer1<=0)
            {
                coolActive1 = false;
                Debug.Log("1번 쿨타임 종료");
            }
        }
         if (time2)
        {
            Debug.Log("2번 쿨타임 가동중");
            cooltimer2 -= Time.deltaTime;
            if (cooltimer2 <= 0)
            {
                coolActive2 = false;
                Debug.Log("2번 쿨타임 종료");
            }
        }
         if(time3)
        {
            Debug.Log("3번 쿨타임 가동중");
            cooltimer3 -= Time.deltaTime;
            if (cooltimer3 <= 0)
            {
                coolActive3 = false;
                Debug.Log("3번 쿨타임 종료");
            }
        }
        if (damagecheck)
        {
            damagetimer -= Time.deltaTime;
            if (damagetimer <= 0)
            {
                damageActive = false;
            }
        }
    }

    /// <summary>
    /// 몬스터 쿨타임 시작 시스템
    /// </summary>
    /// <param name="cas">1번에서 3번까지 쿨타임 카운터가 있습니다.</param>
    /// <param name="time">쿨타임 시간을 지정할수 있습니다.</param>
    protected void cooltimeStart(int cas,float time)
    {
        switch (cas)
        {
            case 1:
                coolActive1 = true;
                cooltimer1 = time;
                Debug.Log("1번 쿨타임 시작");
                break;
            case 2:
                coolActive2 = true;
                cooltimer2 = time;
                Debug.Log("2번 쿨타임 시작");
                break;
            case 3:
                coolActive3 = true;
                cooltimer3 = time;
                Debug.Log("3번 쿨타임 시작");
                break;
            case 4:
                damageActive = true;
                damagetimer = time;
                /*Debug.Log("데미지 쿨타임 시작");*/
                break;
            default:
                Debug.LogWarning("쿨타임 시작 실패");
                break;
        }
    }
    protected void cooltimeStop(int cas)
    {
        switch (cas)
        {
            case 1:
                coolActive1 = false;
                cooltimer1 = 0f;
                Debug.Log("1번 쿨타임 종료");
                break;
            case 2:
                coolActive2 = false;
                cooltimer2 = 0f;
                Debug.Log("2번 쿨타임 종료");
                break;
            case 3:
                coolActive3 = false;
                cooltimer3 = 0f;
                Debug.Log("3번 쿨타임 종료");
                break;
            case 4:
                damageActive = false;
                damagetimer = 0f;
                Debug.Log("데미지 쿨타임 종료");
                break;
            default:
                Debug.LogWarning("쿨타임 초기화 실패");
                break;
        }
    }


}



