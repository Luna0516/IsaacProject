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

    public Vector2 dir = Vector2.right;



    public float damage;
    public float Damage
    {
        get => damage;
        set
        {
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
    }
    private void Start()
    {
        //tearExplosion.SetActive(false);
        initialPosition = this.transform.position;

    }
    void Update()
    {
        AddGravity();

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


    private void OnEnable()
    {
        elapsedTime = 0.0f;
        isDropping = false;
        StartCoroutine(LifeOver(lifeTime));
        //Init() 함수로 초기화 적용 시키기.. 
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
        transform.Translate(Time.deltaTime * speed * dir);
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
}

