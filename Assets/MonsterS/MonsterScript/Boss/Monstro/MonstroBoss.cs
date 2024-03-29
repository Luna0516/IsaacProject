using UnityEngine;

public class MonstroBoss : EnemyBase
{
    public enum Monstate
    {
        Idel = 0, jump, superjump, attack
    }

    Monstate state = Monstate.jump;

    System.Action stateWatcher;
    bool attackgo = false;
    public int jumpcount = 0;
    Vector3 turret = Vector3.zero;

    /// <summary>
    /// 보스 역수계산용 변수
    /// </summary>
    float MaxHPcal;

    protected Monstate Statecom
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            allcoolStop();
            stateWatcher = wewantnoNull;
            switch (state)
            {
                case Monstate.Idel:
                    if (jumpcount > 0)
                    {
                        Statecom = Monstate.jump;
                    }
                    else
                    {
                        StateDone = false;
                        speed = 0;
                        cooltimeStart(1, 1.167f);
                        stateWatcher = Idelchecker;
                    }

                    break;
                case Monstate.jump:

                    StateDone = false;
                    speed = 0;

                    cooltimeStart(1, 0.5f);
                    cooltimeStart(2, 2.25f);
                    cooltimeStart(3, 2.4f);
                    stateWatcher = jumpcheck;
                    if (jumpcount < 3)
                    {
                        jumpcount++;
                        animator.SetTrigger(anicode_Jump);
                    }
                    else
                    {
                        jumpcount = 0;
                        StateDone = true;
                    }
                    break;
                case Monstate.superjump:
                    StateDone = false;
                    speed = 0;
                    animator.SetTrigger(anicode_SuperJump);
                    cooltimeStart(1, 0.5f);
                    cooltimeStart(2, 1.5f);
                    cooltimeStart(3, 2.25f);
                    stateWatcher = superjumpcheck;
                    break;
                case Monstate.attack:
                    attackgo = true;
                    StateDone = false;
                    animator.SetTrigger(anicode_Attack);
                    speed = 0;
                    cooltimeStart(1, 0.7f);
                    cooltimeStart(2, 0.9f);
                    cooltimeStart(3, 2.3f);
                    stateWatcher = attackchecker;
                    break;
            }

        }
    }

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
    int randomPatt = 0;

    /// <summary>
    /// 스프라이트 렌더러
    /// </summary>
    SpriteRenderer spriteRenderer;
    int anicode_Attack = Animator.StringToHash("Attack");
    int anicode_Jump = Animator.StringToHash("Jump");
    int anicode_SuperJump = Animator.StringToHash("SuperJump");


    bool stateDone = false;

    bool StateDone
    {
        get => stateDone;
        set
        {
            if (stateDone != value)
            {
                stateDone = value;
                if (stateDone)
                {
                    //랜덤패턴 int 변수에 랜덤값 대입
                    randomPatt = Random.Range(0, 101);

                    if (randomPatt < 10)
                    {
                        Statecom = Monstate.Idel;
                    }
                    else if (randomPatt < 30)
                    {
                        Statecom = Monstate.jump;
                    }
                    else if (randomPatt < 70)
                    {
                        Statecom = Monstate.superjump;
                    }
                    else
                    {
                        Statecom = Monstate.attack;
                    }
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //애니메이터 불러오기
        animator = transform.GetComponentInChildren<Animator>();
        MaxHPcal = 1.0f / MaxHP;
        //스프라이트 렌더러 불러오기
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        sppeed = speed;
        speed = 0;
        Statecom = Monstate.Idel;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Inst.onBossHealthChange?.Invoke(1);
        state = Monstate.Idel;
    }
    protected override void Update()
    {
        base.Update();
        HeadToCal();
        stateWatcher();
        orderInGame(spriteRenderer);
        //이동 함수 실행
        damageoff(spriteRenderer);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    //공격 받을경우 체력 깎임(EnemyBase 오버라이드)
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    void Idelchecker()
    {
        if (!coolActive1)
        {
            StateDone = true;
        }
    }

    void jumpcheck()
    {
        if (!coolActive1)
        {
            speed = sppeed;
            if (!coolActive2)
            {
                speed = 0;
                if (!coolActive3)
                {
                    Statecom = Monstate.Idel;
                }
            }
        }
    }

    void superjumpcheck()
    {
        if (!coolActive1)
        {
            speed = sppeed * 5;
            if (!coolActive2)
            {
                speed = 0;
                if (!coolActive3)
                {
                    Splashbullet();
                    Statecom = Monstate.Idel;
                }
            }
        }
    }

    void attackchecker()
    {
        if (!coolActive1)
        {
            ShatteredBullet(attackgo);
            if (!coolActive2)
            {
                if (!coolActive3)
                {
                    speed = sppeed;
                    StateDone = true;
                }
            }
        }
    }

    /// <summary>
    /// 몬스트로 이동 함수(오버라이드(베이스 없음))
    /// </summary>
    protected override void Movement()
    {
        //플레이어를 향해 이동하는 식
        transform.Translate(Time.deltaTime * speed * HeadToNormal);

        //방향에 따라 스프라이트 렌더러의 Flip값을 수정하는 조건문
        if (HeadToNormal.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void Splashbullet()
    {
        for (int i = 0; i < 10; i++)
        {
            float angle = i * 360f / 10;  // 각도 계산
            Vector3 spawnPosition = transform.position;  // 생성 위치 계산
            factory.GetObject(PoolObjectType.EnemyBullet, spawnPosition, Vector3.one * 1.5f, angle);  // 총알 생성
        }
    }
    void ShatteredBullet(bool attackclear)
    {
        if (attackclear)
        {
            if (HeadToNormal.x < 0)
            { turret = Quaternion.Euler(0, 0, 90).eulerAngles; }
            else
            { turret = Quaternion.Euler(0, 0, -90).eulerAngles; }
            int randomshot = Random.Range(7, 15);
            for (int i = 0; i < randomshot; i++)
            {
                float Shattering = Random.Range(-45, 46);
                Vector3 shotgack = Quaternion.Euler(0, 0, Shattering).eulerAngles;
                float randx = Random.Range(-0.5f, 0.6f);
                float randy = Random.Range(-0.5f, 0.6f);
                factory.GetObject(PoolObjectType.EnemyBullet, new Vector3(transform.position.x + randx, transform.position.y + randy, 0), Vector3.one * 1.5f, turret.z + shotgack.z);
            }
            attackgo = false;
        }
    }
    public override void Hitten()
    {
        base.Hitten();
        GameManager.Inst.onBossHealthChange?.Invoke(HPCalculater());
        damaged(spriteRenderer);
    }
    float HPCalculater()
    {
        return hp * MaxHPcal;
    }
}
