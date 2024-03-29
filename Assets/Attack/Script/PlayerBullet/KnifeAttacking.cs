using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class KnifeAttacking : AttackBase
{
    /// <summary>
    /// 자식 컴포넌트
    /// </summary>
    GameObject child;

    /// <summary>
    /// 반복 함수 호출용 델리게이트
    /// </summary>
    Action updater;

    /// <summary>
    /// 발사 상태 확인
    /// </summary>
    public bool isFiring = false;

    /// <summary>
    /// 아이작이 칼을 가지고 있는지 확인 (발사 안함)
    /// </summary>
    public bool inMyHand = true;

    Player playerTest;
    float copychager;


    [ReadOnly(true)]
    public float chargeGage = 0f;

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
    }
    protected override void OnEnable()
    {
        playerTest = GameManager.Inst.Player;

        if (playerTest != null)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = collision.transform.GetComponentInChildren<EnemyBase>();
            if (enemy == null)
            {
                enemy = collision.GetComponentInParent<EnemyBase>();
            }
            enemy.damage = Damage;
            enemy.Hitten();
            Vector2 nuckBackDir = dir;
            enemy.NuckBack(nuckBackDir.normalized);
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

    }
    protected override void Init()
    {
        speed = playerTest.TearSpeed;
        this.Damage = playerTest.Damage;
        lifeTime = playerTest.Range;
        MoveDir = playerTest.AttackDir;
        rigidBody.gravityScale = 0.0f;
        dir = MoveDir;
        Damage *= 2;
    }
    void changeDir()
    {
        MoveDir = playerTest.AttackDir;
        Debug.Log(moveDir);
    }
    void rotateTurret(Vector2 dir)
    {
        if (inMyHand)
        {
            Debug.Log("값 변함");
            if (dir.x > 0)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 270);
            }
            else if (dir.x < 0)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir.y < 0)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (dir.y > 0)
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    public void pressButton()
    {
        if (!isFiring && inMyHand)
        {
            isFiring = true;
            Debug.Log("샷");
            updater += chargeing;
        }
    }
    public void cancleButton()
    {
        if (inMyHand)
        {
            Debug.Log("취소");
            updater -= chargeing;
            copychager = ChargeGage;
            updater += MovingKnife;
            ChargeGage = 0f;
        }
    }
    void chargeing()
    {
        ChargeGage += Time.deltaTime * speed;
    }
    void MovingKnife()
    {
        inMyHand = false;
        child.transform.Translate(Vector2.up * Time.deltaTime * copychager, Space.Self);
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
            isFiring = false;
            inMyHand = true;
        }
    }
}
