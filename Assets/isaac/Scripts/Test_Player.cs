using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Player : MonoBehaviour
{
    /// <summary>
    /// 눈물 오브젝트
    /// </summary>
    public GameObject Tear;

    /// <summary>
    /// 폭탄 오브젝트
    /// </summary>
    public GameObject BombObj;
    /// <summary>
    /// 화면 공속
    /// </summary>
    public float tearSpeed = 2.73f;
    /// <summary>
    /// 눈물 딜레이
    /// </summary>
    float currentTearDelay = 0.0f;
    /// <summary>
    /// 눈물 딜레이 체크
    /// </summary>
    bool IsAttackReady => currentTearDelay < 0;
    /// <summary>
    /// 계산 공속
    /// </summary>
    float attackSpeed;
    /// <summary>
    /// 연사맥스
    /// </summary>
    float maxAttackSpeed = 1.0f;
    /// <summary>
    /// 최대연사수치 (연사맥스에 따라 수치 변함)
    /// </summary>
    float maximumTearSpeed = 5.0f;
    /// <summary>
    /// 이동속도
    /// </summary>
    public float speed = 2.5f;
    /// <summary>
    /// 최대이동속도
    /// </summary>
    const float maximumSpeed = 6.0f;
    /// <summary>
    /// 사거리
    /// </summary>
    public float range;
    /// <summary>
    /// 눈물이 날아가는 속도
    /// </summary>
    public float shotSpeed = 1.0f;
    /// <summary>
    /// 최대체력
    /// </summary>
    public float maxHealth = 6.0f;
    /// <summary>
    /// 현재 체력
    /// </summary>
    public float health = 1;
    /// <summary>
    /// 눈물에 넣어줄 데미지
    /// </summary>
    public float damage;
    /// <summary>
    /// 데미지 배수
    /// </summary>
    float multiDmg = 1.0f;
    /// <summary>
    /// 아이템을 먹은 후 데미지 배수
    /// </summary>
    float currentMultiDmg;
    /// <summary>
    /// 눈물 공격키를 눌렀는지 확인하는 변수
    /// </summary>
    bool isShoot = false;
    /// <summary>
    /// 무적시간
    /// </summary>
    float invisibleTime = 3.0f;
    /// <summary>
    /// 무적시간 초기화용
    /// </summary>
    float currentInvisible = 0.0f;
    // 머리
    Transform head;
    // 머리 애니
    Animator headAni;
    // 몸
    Transform body;
    // 몸 애니
    Animator bodyAni;
    // 이동시 좌우 변경
    SpriteRenderer bodySR;
    // 플레이어 액션
    Test_InputAction inputAction;
    // 머리 움직일 때 쓸 벡터값
    Vector2 headDir = Vector2.zero;
    // 몸 움직일때 쓸 벡터값
    Vector2 bodyDir = Vector2.zero;
    public Action onUseActive;
    public int Coin { get; set; }
    public int Bomb { get; set; }
    public int Key { get; set; }
    /// <summary>
    /// 화면에 띄울 damage 프로퍼티
    /// </summary>
    public float Damage
    {
        get => damage;
        private set => damage = value;
    }
    /// <summary>
    /// 화면에 띄울 speed 프로퍼티
    /// </summary>
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }
    /// <summary>
    /// 화면에 띄울 tearSpeed 프로퍼티
    /// </summary>
    public float TearSpeed
    {
        get => tearSpeed;
        private set => tearSpeed = value;
    }
    /// <summary>
    /// 화면에 띄울 shotSpeed 프로퍼티
    /// </summary>
    public float ShotSpeed
    {
        get => shotSpeed;
        private set => shotSpeed = value;
    }
    /// <summary>
    /// 화면에 띄울 range 프로퍼티
    /// </summary>
    public float Range
    {
        get => range;
        private set => range = value;
    }
    /// <summary>
    /// 화면에 띄울 health 프로퍼티
    /// </summary>
    public float Health
    {
        get => health;
        private set => health = value;
    }
    private void Awake()
    {
        inputAction = new Test_InputAction();
        // 몸통 관련 항목
        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();
        // 머리 관련 항목
        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        health = maxHealth;
    }
    private void Update()
    {
        Vector3 dir = new Vector3(bodyDir.x * speed * Time.deltaTime, bodyDir.y * speed * Time.deltaTime, 0f);
        transform.position += dir;
        currentTearDelay -= Time.deltaTime;
        currentInvisible -= Time.deltaTime;
        
    }
    private void FixedUpdate()
    {
        ShootingTear();
        attackSpeed = maxAttackSpeed / tearSpeed;
        if (tearSpeed > maximumTearSpeed)
        {
            tearSpeed = maximumTearSpeed;
        }
    }
    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMove;
        inputAction.Player.Move.canceled += OnMove;
        inputAction.Player.Shot.performed += OnFire;
        inputAction.Player.Shot.canceled += OnFire;
    }

    private void OnDisable()
    {
        inputAction.Player.Move.performed -= OnMove;
        inputAction.Player.Move.canceled -= OnMove;
        inputAction.Player.Shot.performed -= OnFire;
        inputAction.Player.Shot.canceled -= OnFire;
        inputAction.Player.Disable();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(InvisibleTime());
            Debug.Log("적과 충돌/ 남은 체력 : " + health);
            if (health <= 0)
            {
                Die();
            }
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            Item passive = collision.gameObject.GetComponent<ItemBase>().passiveItem;
            Damage += passive.Attack;
            if(passive.MultiDmg != 0)
            {
                multiDmg *= passive.MultiDmg;
            }
            Speed += passive.Speed;
            Range += passive.Range;
            ShotSpeed += passive.ShotSpeed;
            TearSpeed += passive.TearSpeed;
            Debug.Log("multi : " + multiDmg);
            Damage = Damage * currentMultiDmg;
            multiDmg = 1.0f;
            if(speed > maximumSpeed)
            {
                speed = maximumSpeed;
            }
        }
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        bodyDir = value;

        bodyAni.SetBool("isMove", true);
        bodyAni.SetFloat("MoveDir_X", bodyDir.x);
        bodyAni.SetFloat("MoveDir_Y", bodyDir.y);
        headAni.SetFloat("MoveDir_X", bodyDir.x);
        headAni.SetFloat("MoveDir_Y", bodyDir.y);
    }
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        headDir = value;

        headAni.SetFloat("ShootDir_X", headDir.x);
        headAni.SetFloat("ShootDir_Y", headDir.y);
        if (context.performed)
        {
            headAni.SetBool("isShoot", true);
            isShoot = true;
        }
        else if (context.canceled)
        {
            headAni.SetBool("isShoot", false);
            isShoot = false;
        }
        if (headDir.x > 0 && headDir.x < 0 || headDir.y != 0)
        {
            headDir.x = 0;
            headDir.Normalize();
        }
    }
    private void Damaged()
    {
        health--;
        head.gameObject.SetActive(false);
        bodyAni.SetTrigger("Damage");
    }
    IEnumerator InvisibleTime()
    {
        Damaged();

        currentInvisible = invisibleTime;

        yield return new WaitForSeconds(currentInvisible);

        head.gameObject.SetActive(true);
    }
    private void Die()
    {
        inputAction.Player.Disable();
        bodyAni.SetTrigger("Die");
    }
    void ShootingTear()
    {
        if (isShoot == true)
        {
            if (IsAttackReady)
            {
                StartCoroutine(TearDelay());
            }
        }
    }
    
    IEnumerator TearDelay()
    {
        GameObject tear = Instantiate(Tear);

        Transform tearSpawn = transform.GetChild(0);

        tear.transform.position = tearSpawn.position;

        AttackBase tearComp = tear.GetComponent<AttackBase>();

        tearComp.damage = Damage;

        tearComp.dir = headDir;

        currentTearDelay = attackSpeed; // 딜레이 시간 초기화

        yield return new WaitForSeconds(attackSpeed);
    }
    // 내가 눈물 관련 할것
    // 사거리, 공격속도, 방향, 샷스피드

    // 리펙토링중 기본형태 유지
    // 구조유지, 변수
}


