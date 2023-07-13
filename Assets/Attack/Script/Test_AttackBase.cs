using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test_AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;

    public float dropDuration = 0.5f; // ¹ØÀ¸·Î ¶³¾îÁö´Â ½Ã°£
    public float dropDistance = 1.0f; // ¹ØÀ¸·Î ¶³¾îÁö´Â °Å¸®

    private float elapsedTime = 0.0f;
    private bool isDropping = false;
    private Vector2 initialPosition;

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
            transform.Translate(Time.deltaTime * speed * Vector2.right);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            float dropHeight = Mathf.Lerp(0, -dropDistance, elapsedTime / dropDuration);

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
