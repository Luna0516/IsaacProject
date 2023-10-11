using System;
using UnityEngine;

public class EnemyBase : PooledObject
{
    /// <summary>
    /// 몬스터 데미지
    /// </summary>
    [Header("몬스터 데미지")]
    public float MonsterDamage = 1;

    /// <summary>
    /// 에너미에서 적을 추가로 스폰하는 경우, 그 적의 숫자
    /// </summary>
    public int AddableSpawnEnemy = 0;

    /// <summary>
    /// 추가되는 적을 저장하는 배열
    /// </summary>
    public EnemyBase[] addenemy;

    /// <summary>
    /// 
    /// </summary>
    public System.Action<bool> count;

    /// <summary>
    /// 게임 매니저
    /// </summary>
    GameManager Manager;

    /// <summary>
    /// 펙토리
    /// </summary>
    protected Factory factory;

    /// <summary>
    /// 플레이어
    /// </summary>
    protected Player player = null;

    /// <summary>
    /// 이동 목표(플레이어)
    /// </summary>
    protected Transform target;

    /// <summary>
    /// 방향벡터
    /// </summary>
    protected Vector2 calcHeadTo;

    /// <summary>
    /// 방향 벡터(노멀)
    /// </summary>
    protected Vector2 HeadToNormal;

    [Header("플레이어 거리 감지 범위")]
    public float distance = 1.0f;

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
    public float hp;

    /// <summary>
    /// 몬스터에게 들어오는 데미지
    /// </summary>
    public float damage;

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

        set
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
    public float timecounter = 0f;

    /// <summary>
    /// 쿨타임 설정 시간
    /// </summary>
    public float cooltimer1 = 0.0f;
    public float cooltimer2 = 0.0f;
    public float cooltimer3 = 0.0f;

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

    /// <summary>
    /// 
    /// </summary>
    protected bool tooclosetoFight = false;

    /// <summary>
    /// 처음 초기화때 찾으면 안되는 녀석들을 찾지 않게 하기 위해 넣어둔 int값(첫번째 초기화는 무시하고, 두번째 초기화부터 초기화가 정상적으로 이루어진다.)
    /// </summary>
    int countEnable = 0;

    /// <summary>
    /// 몬스터들의 마찰력을 담을 float
    /// </summary>
    protected float draglinear = 15f;


    /// <summary>
    /// 쿨타임용 델리게이트에 null방지 함수 추가, rigidbody 를 추가하고 마찰력값을 저장
    /// </summary>
    protected virtual void Awake()
    {
        UpdateCooltimer += wewantnoNull;
        rig = GetComponent<Rigidbody2D>();
        draglinear = rig.drag;
    }

    /// <summary>
    /// 체력 초기화, 적이 필요한 정보 초기화, 활성화시 초기화 방지용 숫자를 1 더한다. ISDead Null방지
    /// </summary>
    protected virtual void OnEnable()
    {
        HPInitial();
        if (countEnable > 0)
        {
            EnemyInithialize();
            player = Manager.Player; // 플레이어를 찾아서 할당
            target = player.transform;

        }
        if (player != null)
        {
            HeadToCal();
        }
        countEnable++;
        IsDead = (bool obj) => { wewantnoNull(); };
    }

    /// <summary>
    /// 플레이어와 그 위치를 정하고 방향도 한번 계산해준다.
    /// </summary>
    protected virtual void Start()
    {
        if (player == null)
        {
            player = Manager.Player; // 플레이어를 찾아서 할당
            target = player.transform;
        }
        HeadToCal();
    }

    /// <summary>
    /// 비활성화시 추가 생성 적의 수를 0으로 만들고 죽었다고 알린다.
    /// </summary>
    protected override void OnDisable()
    {
        base.OnDisable();
        AddableSpawnEnemy = 0;
        IsDead?.Invoke(true);
    }

    /// <summary>
    /// Update에서 실행되는 쿨타임 계산용 델리게이트
    /// </summary>
    protected virtual void Update()
    {
        UpdateCooltimer();
    }


    /// <summary>
    /// 모든 적이 플레이어 총알과 부딫히면 데미지를 받고 피격 이펙트를 취한 후, 넉백된다.
    /// </summary>
    /// <param name="collision"></param>
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
/*        if (collision.gameObject.CompareTag("PlayerKnife"))
        {
            KnifeAttacking attackBase = collision.gameObject.GetComponent<KnifeAttacking>();
            damage = attackBase.Damage;
            Hitten();
            Vector2 nuckBackDir = attackBase.dir;
            NuckBack(nuckBackDir.normalized);
        }*/
    }

    /// <summary>
    /// 관통 총알 (PenetrationTear)용 OnTrigger2D. (필요에 따라 유지 혹은 삭제 가능성 있습니다)
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            AttackBase attackBase = collision.GetComponent<AttackBase>();
            damage = collision.GetComponent<AttackBase>().Damage;
            Hitten();
            Vector2 nuckBackDir = attackBase.dir;
            NuckBack(nuckBackDir.normalized);
        }
/*        if (collision.CompareTag("PlayerKnife"))
        {
            KnifeAttacking attackBase = collision.GetComponent<KnifeAttacking>();
            damage = attackBase.Damage;
            Hitten();
            Vector2 nuckBackDir = attackBase.dir;
            NuckBack(nuckBackDir.normalized);
        }*/
    }

    /// <summary>
    /// 체력 초기화
    /// </summary>
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
    /// 죽었을때 실행될 가상함수(피랑 고기를 흩뿌리고 비활성화된다.)
    /// </summary>
    protected virtual void Die()
    {
        bloodshatter();
        meatshatter();
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 델리게이트 null 방지용 함수
    /// </summary>
    protected void wewantnoNull()
    {

    }


    /// <summary>
    /// 피를 흩뿌리는 함수
    /// </summary>
    protected virtual void bloodshatter()//피를 흩뿌리는 함수
    {
        int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EffectBlood, bloodpos);
        }
    }

    /// <summary>
    /// 고기를 흩뿌리는 함수
    /// </summary>
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

    /// <summary>
    /// 피격시 데미지를 받는 함수
    /// </summary>
    public virtual void Hitten()
    {
        HP -= damage;
        /*Debug.Log($"{gameObject.name}이 {damage}만큼 공격받았다. 남은 체력: {HP}");*/
    }

    /// <summary>
    /// 넉백 함수
    /// </summary>
    /// <param name="HittenHeadTo">이곳에 입력된 벡터쪽으로 날아간다.(-벡터를 넣어야 넉백됨)</param>
    public virtual void NuckBack(Vector2 HittenHeadTo)
    {
        rig.isKinematic = false;
        rig.drag = draglinear;
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


    /// <summary>
    /// 오더레이어 함수(앞 뒤에 따라 렌더링이 위에 된다.)
    /// </summary>
    /// <param name="render"></param>
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

    /// <summary>
    /// 적이 필요로 하는 정보들을 불러오는 함수
    /// </summary>
    void EnemyInithialize()
    {
        Manager = GameManager.Inst;
        factory = Factory.Inst;
    }

    /// <summary>
    /// 방향 계산 함수
    /// </summary>
    protected void HeadToCal()
    {
        calcHeadTo = (target.transform.position - gameObject.transform.position);
        HeadToNormal = calcHeadTo.normalized;
    }


    //---------------------------------------------------<여기서 부터는 쿨타임 관련>-----------------------------------------------------------------

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
                break;
            case 2:
                coolActive2 = true;
                UpdateCooltimer += coolTimerSys2;
                cooltimer2 = time;
                break;
            case 3:
                coolActive3 = true;
                UpdateCooltimer += coolTimerSys3;
                cooltimer3 = time;
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
                break;
            case 2:
                coolActive2 = false;
                cooltimer2 = 0f;
                break;
            case 3:
                coolActive3 = false;
                cooltimer3 = 0f;
                break;
            case 4:
                damageActive = false;
                damagetimer = 0f;
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
        damageActive = false;
        damagetimer = 0f;
        solorActive = false;
        solotimer = 0f;
        UpdateCooltimer -= timecouting;
        timecounter = 0;
    }
}



