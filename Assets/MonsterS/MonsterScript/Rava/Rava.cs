using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rava : EnemyBase
{
    Animator animator;
    Vector2 targetPosition;
    GameObject Ravanian;
    SpriteRenderer sprite;
    public float jumpingTerm = 1.25f;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;


    protected override void Movement()
    {
        transform.Translate(Time.deltaTime * speed * targetPosition);
    }

    protected override void Awake()
    {
        rig = GetComponentInParent<Rigidbody2D>();
        spawneffect = transform.GetChild(2).gameObject;
        animator = GetComponentInChildren<Animator>();
        Ravanian = transform.GetChild(0).gameObject;
        sprite = Ravanian.GetComponent<SpriteRenderer>();
        transform.position = transform.position;
        jumpingTerm = Random.Range(1f, 2f);
    }
    void Start()
    {
        animator.SetFloat("speed", jumpingTerm);
        StopAllCoroutines();
        StartCoroutine(moveingRava());
        targetPosition = Vector2.zero;
    }

    IEnumerator moveingRava()
    {
        while(true)
        {
            yield return new WaitForSeconds(jumpingTerm);
            SetNextTargetPosition();
        }
    }
    protected override void Update()
    {
        Movement();
        orderInGame(sprite);
    }
    private void SetNextTargetPosition()
    {
        float x;
        float y;
        x= Random.Range(MinX,MaxX);
        y = Random.Range(MinY, MaxY);
        targetPosition.x = x;
        targetPosition.y = y;
        if (x>0)
        {
            sprite.flipX = false;
        }
        else 
        {
            sprite.flipX = true;
        }
        targetPosition.Normalize();
    }
    protected override void Hitten()
    {
        base.Hitten();
        StartCoroutine(damaged(sprite));
    }


}
