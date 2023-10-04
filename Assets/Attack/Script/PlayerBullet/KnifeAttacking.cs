using System;
using System.Collections;
using UnityEngine;

public class KnifeAttacking : AttackBase
{
    private Vector2 startPos;
    private Vector2 targetPos;
    private bool isReturning = false;

    GameObject child;

    Action updater;

    float dist;

    void noNull() { }

    bool cantChangeMyDestination = false;

    Test_KnifeShooter playerTest;
    float copychager;
    float chargeGage = 0f;

    float ChargeGage
    {
        get
        {
            return chargeGage;
        }
        set
        {
            chargeGage=value;
            if(chargeGage>maxGage)
            {
                chargeGage = maxGage;
            }
        }
    }

    float maxGage = 10f;

    protected override void Awake()
    {
        base.Awake();
        child = transform.GetChild(0).gameObject;
        updater = noNull;
    }
    protected override void OnEnable()
    {
        player = GameManager.Inst.Player;

        if (player != null)
        {
            Init(); // 눈물 세부사항 초기화
        }
    }
    private void Update()
    {
        updater();
    }
    protected override void Init()
    {
        /*base.Init();  플레이어 생기면 추가*/
        speed = playerTest.TearSpeed;
        this.Damage = playerTest.Damage;
        rangeToLife = playerTest.Range;
        moveDir = playerTest.MoveDir;
        rigidBody.gravityScale = 0.0f;
        dir = moveDir;
        Damage *= 2;
    }
    void pressButton()
    {
        updater += chargeing;
    }
    void cancleButton()
    {
        updater -= chargeing;
        copychager = ChargeGage;
        updater += MovingKnife;
        ChargeGage = 0f;
    }
    void chargeing()
    {
        moveDir = playerTest.MoveDir;
        dir = moveDir;
        ChargeGage += Time.deltaTime*speed;
    }
    void MovingKnife()
    {
        child.transform.Translate(dir * speed);
        dist = (playerTest.transform.position - child.transform.position).sqrMagnitude;
        if(dist* copychager / rangeToLife == rangeToLife)
        {
            updater += ReturningKnife;
            updater -= MovingKnife;
        }
    }
    void ReturningKnife()
    {
        child.transform.Translate(-dir * speed);
        if(child.transform.position == Vector3.zero)
        {
            updater -= ReturningKnife;
        }
    }
}
