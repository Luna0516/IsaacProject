using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;

    public float dropDuration = 2.0f; // 밑으로 떨어지는 시간
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
    GameObject tearExplosion;
    SpriteRenderer tear;



    protected virtual void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        tear = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        tearExplosion.SetActive(false);
        initialPosition = transform.position;

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
        StartCoroutine(LifeOver(lifeTime));
    }

    protected virtual void TearDie(Collision2D collision)
    {
        if (lifeTime < 0)
        {
            tearExplosion.transform.SetParent(null);
            tear.sprite = null;
            tearExplosion.SetActive(true);
            Destroy(gameObject);

        }
        else
        {
            tearExplosion.transform.SetParent(null);
            tearExplosion.transform.position = collision.contacts[0].point;
            tear.sprite = null;
            tearExplosion.SetActive(true);
            Destroy(gameObject);
        }
    }

    protected virtual void AddGravity()
    {
        

        if (!isDropping)
        {
            transform.Translate(Time.deltaTime * speed * dir);
        }
        else
        {
            
            elapsedTime += Time.deltaTime;
            float dropHeight = Mathf.Lerp(0, -dropDistance, elapsedTime / dropDuration);

            //elapsedTime은 5초를 넘을 수 없음, dropDuration은 2

            transform.position = initialPosition + Vector2.down * dropHeight;
        }

    }

    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        StartDrop();
        tearExplosion.transform.SetParent(null);
        tear.sprite = null;
        tearExplosion.SetActive(true);
        Destroy(gameObject);
    }

    private void StartDrop()
    {
        isDropping = true;
        elapsedTime = 0.0f;
    }
}

