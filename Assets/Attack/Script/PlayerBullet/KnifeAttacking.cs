using System;
using System.Collections;
using UnityEngine;

public class KnifeAttacking : AttackBase
{

    GameObject child;

    Action updater;

    bool isfireing = false;

    Test_KnifeShooter playerTest;
    float copychager;
    float chargeGage = 0f;

    Vector2 MoveDir
    {
        get
        {
            return moveDir;
        }
        set
        {
            if (moveDir != value)
            {
                moveDir = value;
                rotateTurret(moveDir);
            }
        }
    }

    float ChargeGage
    {
        get
        {
            return chargeGage;
        }
        set
        {
            chargeGage = value;
            if (chargeGage > maxGage)
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
        updater = changeDir;
        playerTest = FindAnyObjectByType<Test_KnifeShooter>();
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
        this.transform.position = playerTest.transform.position;
    }
    protected override void FixedUpdate()
    {

    }
    void testInst()
    {
        speed = playerTest.TearSpeed;
        this.Damage = playerTest.Damage;
        lifeTime = playerTest.Range;
        MoveDir = playerTest.MoveDir;
        rigidBody.gravityScale = 0.0f;
        dir = MoveDir;
        Damage *= 2;
    }
    protected override void Init()
    {
        /*base.Init();  플레이어 생기면 추가*/
        speed = player.TearSpeed;
        this.Damage = player.Damage;
        lifeTime = player.Range;
        MoveDir = player.MoveDir;
        rigidBody.gravityScale = 0.0f;
        dir = MoveDir;
        Damage *= 2;
    }
    void changeDir()
    {
        MoveDir = player.MoveDir;
    }
    void rotateTurret(Vector2 dir)
    {
        if (!isfireing)
        {
            if (dir.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else if (dir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir.y < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (dir.y > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    public void pressButton()
    {
        if (!isfireing)
        {
            updater += chargeing;
            isfireing = true;
        }
    }
    public void cancleButton()
    {
        if (isfireing)
        {
            updater -= chargeing;
            copychager = ChargeGage;
            updater += MovingKnife;
            ChargeGage = 0f;
        }
    }
    void chargeing()
    {
        ChargeGage += Time.deltaTime * speed;
        Debug.Log(ChargeGage);
    }
    void MovingKnife()
    {
        child.transform.Translate(Vector2.up * Time.deltaTime * copychager, Space.Self);
        Debug.Log(lifeTime * copychager * 0.1f);
        if (child.transform.localPosition.y > lifeTime * copychager * 0.1f)
        {
            updater += ReturningKnife;
            updater -= MovingKnife;
        }
    }
    void ReturningKnife()
    {
        child.transform.Translate(Vector2.down * Time.deltaTime * copychager, Space.Self);
        if (child.transform.localPosition.y < 0.5f)
        {
            updater -= ReturningKnife;
            isfireing = false;
        }
    }
}
