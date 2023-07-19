using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    private float dropStartHeight = 0.0f;


    protected virtual void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
        tear = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        tearExplosion.SetActive(false);
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
            elapsedTime += Time.deltaTime;


        if (!isDropping)
        {
            transform.Translate(Time.deltaTime * speed * dir);
            if(elapsedTime >= dropDuration)
            {
                
                StartDrop();
            }
        }
        else if(isDropping && elapsedTime > 0 ) 
        {
           
            //float dropHeight = Mathf.Lerp(0, -dropDistance, (elapsedTime / dropDuration) * 0.05f);
            if( dir.x == 1 || dir.x== -1)
            {
                float normalizedTime = elapsedTime / dropDuration;
                //float dropHeight = Mathf.Lerp(dropStartHeight, -dropDistance, normalizedTime * normalizedTime);

                float dropHeight = Mathf.SmoothStep(dropStartHeight, -dropDistance, normalizedTime);

                transform.Translate(Vector2.down * -dropHeight);
            }
            else
            {
                transform.Translate(Time.deltaTime * speed * dir);
            }
            



            // transform.Translate(Vector2.down * -dropHeight);
            // this.transform.position = Vector2.down * dropHeight;
        }

    }

    protected virtual IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(dropDuration);
        //StartDrop();
        yield return new WaitForSeconds(delay - dropDuration);
        tearExplosion.transform.SetParent(null);
        tear.sprite = null;
        tearExplosion.SetActive(true);
        Destroy(gameObject);
    }

    private void StartDrop()
    {

        elapsedTime = 0.0f;
        isDropping = true;
        
    }
}

