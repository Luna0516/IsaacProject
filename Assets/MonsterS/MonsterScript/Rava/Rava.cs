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
    public bool movecheck = true;

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
        targetPosition = Vector2.zero;
    }

    void moveingRava()
    {
        if(movecheck)
        {
            movecheck = false;
            cooltimeStart(2, jumpingTerm);
        }
        else if(!coolActive2)
        {
            SetNextTargetPosition();
            movecheck = true;
        }
    }
    protected override void Update()
    {
        coolTimeSystem(coolActive1, coolActive2, coolActive3, damageActive);
        Movement();
        orderInGame(sprite);
        damageoff(sprite);
        moveingRava();
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
        damaged(sprite);
    }
}
