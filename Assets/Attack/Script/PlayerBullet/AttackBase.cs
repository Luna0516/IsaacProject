using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackBase : PooledObject
{
    
    public float speed = 1.0f;
    public float lifeTime = 5.0f;

    public float dropDuration = 10.0f; // 밑으로 떨어지는 시간
    public float dropDistance = 1.0f; // 밑으로 떨어지는 거리

    private float elapsedTime = 0.0f;
    private bool isDropping = false;
    
    private Vector2 initialPosition;

    public Vector2 moveDir = Vector2.zero;
    public Vector2 dir = Vector2.right;

    Player player;


    public float damage;
    public float Damage
    {
        get => damage;
        set
        {
            if(value < 0)
            {
                damage = 0;
            }

            damage = value;
        }
    }

    Animator anim;
    //GameObject tearExplosion;
    SpriteRenderer tear;

    private float dropStartHeight = 0.0f;


    protected virtual void Awake()
    {
        //tearExplosion = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        tear = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }
private void OnEnable()
    {
        player = GameManager.Inst.Player;
        
        Init();
        StartCoroutine(LifeOver(lifeTime));
        //Init() 함수로 초기화 적용 시키기.. 
    }
    private void Start()
    {
        //tearExplosion.SetActive(false);
        initialPosition = this.transform.position;

    }


    void Update()
    {
        //AddGravity();
    }
    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed * dir);
        Rigidbody2D bullertRB = this.GetComponent<Rigidbody2D>();
        bullertRB.MovePosition(bullertRB.position + dir * speed * Time.fixedDeltaTime);
        bullertRB.velocity = new Vector3(dir.x * speed, dir.y * speed);
    }

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

  

    protected virtual void TearDie(Collision2D collision)
    {
        if (lifeTime < 0)
        {
            //tearExplosion.transform.SetParent(null);
            //tear.sprite = null;
            //tearExplosion.SetActive(true);
            Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position);
            gameObject.SetActive(false);

        }
        else
        {
            //tearExplosion.transform.SetParent(null);
            //tearExplosion.transform.position = collision.contacts[0].point;
            //tear.sprite = null;
            //tearExplosion.SetActive(true);
            Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position);
            gameObject.SetActive(false);
        }
    }

    protected virtual void AddGravity()
    {
        elapsedTime += Time.deltaTime;
        
        
        isDropping = true;
        if (!isDropping)
        {

            if (elapsedTime >= dropDuration)
            {
                StartDrop();
            }
        }
        else if (isDropping && elapsedTime > 0)
        {

            //float dropHeight = Mathf.Lerp(0, -dropDistance, (elapsedTime / dropDuration) * 0.05f);
            if (dir.x == 1 || dir.x == -1)
            {
                float normalizedTime = elapsedTime * 0.05f / dropDuration;
                //float dropHeight = Mathf.Lerp(dropStartHeight, -dropDistance, normalizedTime * normalizedTime);

                float dropHeight = Mathf.SmoothStep(dropStartHeight, -dropDistance, normalizedTime);

                transform.Translate(Vector2.down * -dropHeight);
            }




            // transform.Translate(Vector2.down * -dropHeight);
            // this.transform.position = Vector2.down * dropHeight;
        }

    }

    protected override IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(dropDuration);
        //StartDrop();
        yield return new WaitForSeconds(delay - dropDuration);
        Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position);
        //tearExplosion.transform.SetParent(null);
        //tear.sprite = null;
        //tearExplosion.SetActive(true);
        gameObject.SetActive(false);
    }

    private void StartDrop()
    {
        elapsedTime = 0.0f;
        isDropping = true;        
    }

    /// <summary>
    /// 총알 능력치 초기화
    /// </summary>
    private void Init()
    {
        this.Damage = player.Damage;
        lifeTime =  player.range;

        isDropping = false;
        elapsedTime = 0.0f;
    }
}

