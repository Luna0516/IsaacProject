using System;
using UnityEngine;
public class Fly : EnemyBase
{
    Vector2 Rnad;
    public float invincivalTime = 1f;
    bool invincival = false;
    Animator animator;
    SpriteRenderer rneder;
    Collider2D coll;
    string enemytag = "Enemy";
    string untager = "DeadFly";


    /// <summary>
    /// 노이즈 무브 정도
    /// </summary>
    [Header("노이즈 무브")]
    public float noise = 5f;


    float X;
    float Y;
    Action Invic;

    protected override void Awake()
    {
        base.Awake();
        coll = GetComponent<Collider2D>();
        rneder = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Invic = wewantnoNull;
    }
    protected override void OnEnable()
    {
        transform.tag = enemytag;
        invincival = false;
        base.OnEnable();
        speed = UnityEngine.Random.Range(0.5f, 2f);
        cooltimeStart(1, invincivalTime);
        Invic += invancivalcheck;
        Rnad = Vector2.zero;
    }
    protected override void OnDisable()
    {
        coll.isTrigger = false;
        base.OnDisable();
        Invic -= invancivalcheck;
        Invic -= Dieying;
        allcoolStop();
    }
    void noisyMove()
    {
        if (!coll.isTrigger)
        {
            X = UnityEngine.Random.Range(-noise, noise + 0.1f);
            Y = UnityEngine.Random.Range(-noise, noise + 0.1f);
            Rnad.x = X;
            Rnad.y = Y;
            this.gameObject.transform.Translate(Time.deltaTime * speed * Rnad);
        }
    }
    protected override void Update()
    {
        base.Update();
        HeadToCal();
        Invic();
        orderInGame(rneder);
        if (HeadToNormal.x > 0)
        {
            rneder.flipX = true;
        }
        else
        {
            rneder.flipX = false;
        }
        this.gameObject.transform.Translate(Time.deltaTime * speed * HeadToNormal);
        noisyMove();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
/*        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
        }*/
    }
    protected override void Die()
    {
        animator.SetInteger("Dead", 1);
        cooltimeStart(2, 0.917f);
        transform.tag = untager;
        Invic += Dieying;
        coll.isTrigger = true;
    }
    public override void Hitten()
    {
        if (invincival)
        {
            base.Hitten();
        }
    }
    void Dieying()
    {
        if (!coolActive2)
        {
            this.gameObject.SetActive(false);
        }
    }
    void invancivalcheck()
    {
        if (!coolActive1)
        {
            invincival = !coolActive1;
            allcoolStop();
            Invic -= invancivalcheck;
        }
    }
}
