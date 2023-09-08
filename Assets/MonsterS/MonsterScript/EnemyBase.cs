using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : PooledObject
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
    public Factory factory;
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

    [Header("피격시 빨간색 유지 시간")]
    public float hiittenEfeect = 0.1f;

    /// <summary>
    /// 몬스터 사망을 알리는 델리게이트
    /// </summary>
    public System.Action<bool> IsDead;


    /// <summary>
    /// 쿨타임 작동 델리게이트
    /// </summary>
    public Action UpdateCooltimer;

    /// <summary>
    /// 시간
    /// </summary>
    protected float timecounter = 0f;

    /// <summary>
    /// 쿨타임 설정 시간
    /// </summary>
    protected float cooltimer1 = 0.0f;
    protected float cooltimer2 = 0.0f;
    protected float cooltimer3 = 0.0f;

    /// <summary>
    /// 피격 이펙트용 쿨타임
    /// </summary>
    protected float damagetimer = 0.0f;

    /// <summary>
    /// 시간 차감형 쿨타임
    /// </summary>
    protected float solotimer = 0.0f;

    /// <summary>
    /// 쿨타임 체크용 bool값
    /// </summary>
    protected bool coolActive1 = false;
    protected bool coolActive2 = false;
    protected bool coolActive3 = false;

    /// <summary>
    /// 피격 확인용 bool 값
    /// </summary>
    protected bool damageActive = false;

    /// <summary>
    /// 시간 차감형 쿨타임 bool값
    /// </summary>
    protected bool solorActive = false;


    //에너미 베이스 Awake : 리지디 바디, 게임매니저 , 플레이어 , 타깃 위치 , 스폰 이펙트를 찾음
    protected virtual void Awake()
    {
        UpdateCooltimer += wewantnoNull;
        rig = GetComponent<Rigidbody2D>();
        spawneffect = transform.GetChild(2).gameObject;
    }

    private void Start()
    {
        EnemyInithialize();
    }

    // 플레이어 확인 / HP 확인 / 스폰 이펙트 관련
    protected virtual void OnEnable()
    {
        HPInitial();
        spawneffect.SetActive(true);
    }

    //쿨타임 델리게이트, 적을 향한 방향 계산하는 update
    protected virtual void Update()
    {
        UpdateCooltimer();
        HeadToCal();
    }

    //모든 적들은 콜리전이 부딫혔을때 그것이 총알이라면 데미지를 받고 넉백됨
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            AttackBase attackBase = collision.gameObject.GetComponent<AttackBase>();
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();

            Vector2 nuckBackDir = attackBase.dir;
            NuckBack(nuckBackDir.normalized);
        }
    }

    /// <summary>
    /// 관통 총알 (PenetrationTear)용 OnTrigger2D. (필요에 따라 유지 혹은 삭제 가능성 있습니다)
    /// </summary>
    /// <param name="collision"></param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            AttackBase attackBase = collision.GetComponent<AttackBase>();
            damage = collision.GetComponent<AttackBase>().Damage;
            Hitten();

            Vector2 nuckBackDir = attackBase.dir;
            NuckBack(nuckBackDir.normalized);

        }
    }


    private void HPInitial()
    {
        hp = MaxHP;
    }

    /// <summary>
    /// 이동을 담당하는 가상함수 
    /// </summary>
    protected virtual void Movement()
    {

    }

    /// <summary>
    /// 죽었을때 실행될 가상함수
    /// </summary>
    protected virtual void Die()
    {
        bloodshatter();
        meatshatter();
        Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }

    /// <summary>
    /// 델리게이트 null 방지용 함수
    /// </summary>
    protected void wewantnoNull()
    {

    }

    /// <summary>
    /// 
    /// </summary>
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
        if (!damageActive)
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


    void timecouting()
    {
        timecounter += Time.deltaTime;
    }
    void coolTimerSys1()
    {
        /*Debug.Log("1번 쿨타임 가동중");*/
        if (cooltimer1 <= timecounter)
        {
            coolActive1 = false;
            cooltimer1 = 0;
            /*Debug.Log("1번 쿨타임 종료");*/
            UpdateCooltimer -= coolTimerSys1;
        }
    }
    void coolTimerSys2()
    {
        /*Debug.Log("2번 쿨타임 가동중");*/
        if (cooltimer2 <= timecounter)
        {
            coolActive2 = false;
            cooltimer1 = 0;
            /*Debug.Log("2번 쿨타임 종료");*/
            UpdateCooltimer -= coolTimerSys2;
        }
    }
    void coolTimerSys3()
    {
        /*Debug.Log("3번 쿨타임 가동중");*/
        if (cooltimer3 <= timecounter)
        {
            coolActive3 = false;
            cooltimer1 = 0;
            /*Debug.Log("3번 쿨타임 종료");*/
            UpdateCooltimer -= coolTimerSys3;
        }
    }
    void damageTimerSys()
    {
        damagetimer -= Time.deltaTime;
        if (damagetimer <= 0)
        {
            damageActive = false;
            UpdateCooltimer -= damageTimerSys;
        }
    }
    void soloTimerSys()
    {
        solotimer -= Time.deltaTime;
        if (solotimer <= 0)
        {
            solorActive = false;
            UpdateCooltimer -= soloTimerSys;
        }
    }
    /// <summary>
    /// 몬스터 쿨타임 시작 시스템(시작하는 순간 타이머가 돌고 입력한 시간만큼 지나면 bool체크)
    /// </summary>
    /// <param name="cas">1번에서 3번까지 쿨타임 카운터가 있습니다.</param>
    /// <param name="time">쿨타임 시간을 지정할수 있습니다.</param>
    protected void cooltimeStart(int cas, float time)
    {
        if (!coolActive1 && !coolActive2 && !coolActive3)
        {
            UpdateCooltimer += timecouting;
        }

        switch (cas)
        {
            case 1:
                coolActive1 = true;
                UpdateCooltimer += coolTimerSys1;
                cooltimer1 = time;
                Debug.Log("1번 쿨타임 시작");
                break;
            case 2:
                coolActive2 = true;
                UpdateCooltimer += coolTimerSys2;
                cooltimer2 = time;
                Debug.Log("2번 쿨타임 시작");
                break;
            case 3:
                coolActive3 = true;
                UpdateCooltimer += coolTimerSys3;
                cooltimer3 = time;
                Debug.Log("3번 쿨타임 시작");
                break;
            case 4:
                damageActive = true;
                UpdateCooltimer += damageTimerSys;
                damagetimer = time;
                /*Debug.Log("데미지 쿨타임 시작");*/
                break;
            case 5:
                solorActive = true;
                UpdateCooltimer += soloTimerSys;
                solotimer = time;
                /*Debug.Log("데미지 쿨타임 시작");*/
                break;
            default:
                Debug.LogWarning("쿨타임 시작 실패");
                break;
        }
    }

    /// <summary>
    /// 쿨타임을 선택해서 종료합니다.
    /// </summary>
    /// <param name="cas"></param>
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
        if (!coolActive1 && !coolActive2 && !coolActive3)
        {
            UpdateCooltimer += timecouting;
        }
    }
    /// <summary>
    /// 모든 쿨타임을 종료합니다.
    /// </summary>
    protected void allcoolStop()
    {
        coolActive1 = false;
        cooltimer1 = 0f;
        coolActive2 = false;
        cooltimer2 = 0f;
        coolActive3 = false;
        cooltimer3 = 0f;
        UpdateCooltimer -= timecouting;
        timecounter = 0;
    }

    void EnemyInithialize()
    {
        Manager = GameManager.Inst;
        factory = Factory.Inst;
        if (player == null)
        {
            player = Manager.Player; // 플레이어를 찾아서 할당
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("플레이어를 찾을수가 없습니다.");
        }
    }
    protected void HeadToCal()
    {
        HeadTo = (target.transform.position - this.gameObject.transform.position).normalized;
    }

}



