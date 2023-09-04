using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerAction;

public class shit : EnemyBase
{
    /// <summary>
    /// 애니메이터 변수
    /// </summary>
    Animator animator;

    /// <summary>
    /// 스프라이트 렌더러 변수
    /// </summary>
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// 
    /// </summary>
    public float power = 1f;
    public float coolTime = 5;
    public GameObject flyer;
    bool shitDead = false;
    float draglinear = 15f;
    static int flyCount = 0;

    System.Action watcher;

    public bool attackmode = true;
    public bool att = false;

    bool Attackmode
    {
        get
        {
            return attackmode;
        }
        set
        {
            if (attackmode != value)
            {
                attackmode = value;
                if (attackmode && !att)
                {
                    watcher = rest;
                    Attack();
                }
                else
                {
                    att = false;
                    Stopping();
                    Debug.Log("쉬기");
                    coolTime = Random.Range(3, 5);
                    cooltimeStart(1, coolTime);
                    cooltimeStart(3, coolTime + 1f);
                    watcher = watchAttack;
                }
            }

        }
    }

    public bool ShitDead
    {
        get => shitDead;
        set
        {
            shitDead = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        draglinear = rig.drag;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flyCounter();
    }
    protected override void Start()
    {
        base.Start();
        Attackmode = true;
        Attackmode = false;
        att = false;
    }
    protected override void Update()
    {
        base.Update();
        orderInGame(spriteRenderer);

        watcher();

        if (HeadTo.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        { spriteRenderer.flipX = false; }
        damageoff(spriteRenderer);

    }
    void Attack()
    {
        rig.drag = 1;
        rig.AddForce(HeadTo * power, ForceMode2D.Impulse);
        animator.SetTrigger("Attack");
    }
    private void Stopping()
    {
        allcoolStop();
        rig.drag = draglinear;
        rig.velocity = Vector2.zero;
    }

    /// <summary>
    /// 공격을 시작할때 까지의 1번 쿨타임 감시자
    /// </summary>
    void watchAttack()
    {
        Debug.Log("1");
        if (!coolActive1)
        {
            Attackmode = true;
            att = true;
        }
    }
    void rest()
    {
        runningShit();
        Debug.Log("2");
        if (!coolActive3)
        {
            Attackmode = false;
        }
    }

    void runningShit()
    {
        if (!solorActive)
        {
            GameObject shitiything = Factory.Inst.GetObject(PoolObjectType.EnemyShit, transform.position);
            Shitblood bloodobj = shitiything.GetComponent<Shitblood>();
            IsDead += bloodobj.EnamvleChoosAction;
            IsDead?.Invoke(false);
            IsDead -= bloodobj.EnamvleChoosAction;
            cooltimeStart(5, 0.2f);
        }
    }
    void flyCounter()
    {
        int rand = UnityEngine.Random.Range(0, 101);
        if (rand < 40)
        {
            flyCount = 0;
        }
        else if (rand < 60)
        {
            flyCount = 3;
        }
        else if (rand < 80)
        {
            flyCount = 4;
        }
        else if (rand < 101)
        {
            flyCount = 5;
        }
    }

    protected override void Die()
    {
        bloodshatter();
        float ranX = Random.Range(-1, 1.1f);
        float ranY = Random.Range(-1, 1.1f);
        Vector2 pos = new Vector2(ranX, ranY);
        for (int i = 0; i < flyCount; i++)
        {
            GameObject fly = Instantiate(flyer, (Vector2)this.transform.position + pos, this.transform.rotation);
        }
        Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }

    protected override void bloodshatter()
    {
        for (int i = 0; i < 2; i++)//피의 갯수만큼 반복작업
        {
            float X = Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EnemyShit, new Vector2(X, Y));
            Shitblood bloodobj = bloodshit.GetComponent<Shitblood>();
            IsDead += bloodobj.EnamvleChoosAction;
            IsDead?.Invoke(true);
        }
    }
    protected override void Hitten()
    {
        base.Hitten();
        damaged(spriteRenderer);
    }

}
