using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    #region 눈물관련
    /// <summary>
    /// 눈물 오브젝트
    /// </summary>
    public GameObject Tear;
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
    /// 사거리
    /// </summary>
    public float range;
    /// <summary>
    /// 눈물이 날아가는 속도
    /// </summary>
    public float shotSpeed = 1.0f;
    /// <summary>
    /// 눈물 공격키를 눌렀는지 확인하는 변수
    /// </summary>
    bool isShoot = false;
    #endregion
    #region 이속
    /// <summary>
    /// 이동속도
    /// </summary>
    public float speed = 2.5f;
    /// <summary>
    /// 최대이동속도
    /// </summary>
    const float maximumSpeed = 6.0f;
    #endregion
    #region 오브젝트 관련
    /// <summary>
    /// 폭탄 오브젝트
    /// </summary>
    public GameObject BombObj;
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

    CircleCollider2D collider;
    #endregion
    #region 체력
    /// <summary>
    /// 최대체력
    /// </summary>
    public float maxHealth = 6.0f;
    /// <summary>
    /// 현재 체력
    /// </summary>
    public float health = 1;
    #endregion
    #region 데미지
    /// <summary>
    /// 눈물에 넣어줄 데미지
    /// </summary>
    public float damage;
    /// <summary>
    /// 기본 데미지
    /// </summary>
    float baseDmg = 3.5f;
    /// <summary>
    /// 먹은 아이템 데미지 총합
    /// </summary>
    float currentDmg = 0.0f;
    /// <summary>
    /// 먹은 아이템 데미지배수 총합
    /// </summary>
    float currentMultiDmg = 1.0f;
    // 데미지 적용시 예외 아이템
    bool isGetItem169 = false;
    bool isGetItem182 = false;
    #endregion
    #region 무적
    /// <summary>
    /// 무적시간
    /// </summary>
    float invisibleTime = 0.9f;
    /// <summary>
    /// 무적시간 초기화용
    /// </summary>
    float currentInvisible = 0.0f;
    #endregion
    #region 프로퍼티들
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
    #endregion
    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        inputAction = new Test_InputAction();
        // 몸통 관련 항목
        body = transform.GetChild(1);
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();
        // 머리 관련 항목
        head = transform.GetChild(2);
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
            Damaged();
            Debug.Log("적과 충돌/ 남은 체력 : " + health);
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            StartCoroutine(GetItem());
            Item passive = collision.gameObject.GetComponent<ItemBase>().passiveItem;
            currentDmg += passive.Attack;
            currentMultiDmg *= passive.MultiDmg;
            // 데미지 적용시 예외 아이템 switch문
            switch (passive.ItemNum)
            {
                case 169:
                    isGetItem169 = true;
                    break;
                case 182:
                    isGetItem182 = true;
                    break;
            }
            if (passive.ItemNum == 169 || passive.ItemNum == 182)
                currentDmg -= passive.Attack;

            Damage = baseDmg * Mathf.Sqrt(currentDmg * 1.2f + 1f);

            if (isGetItem169)
                Damage += 4f;

            Damage *= currentMultiDmg;

            if (isGetItem182)
                Damage += 1f;

            Damage = (float)Math.Round(Damage, 2);

            Speed += passive.Speed;
            Range += passive.Range;
            ShotSpeed += passive.ShotSpeed;
            TearSpeed += passive.TearSpeed;
            Debug.Log("currentDmg : " + currentDmg);
            Debug.Log("currentmulti : " + currentMultiDmg);
            Debug.Log("total damage : " + damage);
            if (speed > maximumSpeed)
            {
                speed = maximumSpeed;
            }
        }
    }
    IEnumerator GetItem()
    {
        bodyAni.SetTrigger("GetItem");
        head.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.9f);

        head.gameObject.SetActive(true);
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
    private void Damaged()
    {
        if (Health > 0)
        {
            StartCoroutine(InvisibleTime());
            health--;
            head.gameObject.SetActive(false);
            bodyAni.SetTrigger("Damage");
        }
        else if (Health <= 0)
        {
            Die();
        }
    }
    IEnumerator InvisibleTime()
    {
        collider.enabled = false;
        currentInvisible = invisibleTime;

        yield return new WaitForSeconds(currentInvisible);

        head.gameObject.SetActive(true);
        collider.enabled = true;
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
    private void Die()
    {
        bodyAni.SetTrigger("Die");
        StopAllCoroutines();
        inputAction.Player.Disable();
        collider.enabled = false;
        head.gameObject.SetActive(false);
    }
    
    // 내가 눈물 관련 할것
    // 사거리, 공격속도, 방향, 샷스피드

    // 리펙토링중 기본형태 유지
    // 구조유지, 변수
}


