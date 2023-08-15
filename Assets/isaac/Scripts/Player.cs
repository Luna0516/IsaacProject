using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    #region 눈물관련
    /// <summary>
    /// 화면 공속
    /// </summary>
    [SerializeField]
    float tearSpeed = 0.0f;
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
    float tearDelay;
    /// <summary>
    /// 실제 공격속도
    /// </summary>
    [SerializeField]
    float tearFire;
    /// <summary>
    /// 연사맥스
    /// </summary>
    float fireRate = 5.0f;
    /// <summary>
    /// 아이템으로 인한 공속 증가치
    /// </summary>
    float itemSpeed = 0.0f;
    /// <summary>
    /// 사거리
    /// </summary>
    [SerializeField]
    float range = 6.5f;
    /// <summary>
    /// 눈물이 날아가는 속도
    /// </summary>
    float shotSpeed = 1.0f;
    /// <summary>
    /// 눈물 공격키를 눌렀는지 확인하는 변수
    /// </summary>
    bool isShoot = false;
    #endregion
    #region 이속
    /// <summary>
    /// 이동속도
    /// </summary>
    [SerializeField]
    float speed = 2.5f;
    /// <summary>
    /// 최대이동속도
    /// </summary>
    const float maximumSpeed = 6.0f;
    #endregion
    #region 오브젝트 관련
    /// <summary>
    /// 폭탄 오브젝트
    /// </summary>
    public GameObject bombObj;
    // 머리
    Transform head;
    // 머리 애니
    Animator headAni;
    // 몸
    Transform body;
    Transform getItem;
    SpriteRenderer getItemSR;
    // 몸 애니
    Animator bodyAni;
    // 플레이어 액션
    PlayerAction inputAction;
    // 머리 움직일 때 쓸 벡터값
    Vector2 headDir = Vector2.zero;
    // 몸 움직일때 쓸 벡터값
    Vector2 bodyDir = Vector2.zero;
    float minHeadAni = 1.37f;
    float maxHeadAni = 2.27f;
    Rigidbody2D rigid;
    /// <summary>
    /// 총알 능력치 초기화때 쓸 프로퍼티, 총알 발사 방향
    /// </summary>
    public Vector2 AttackDir 
    {
        get => headDir;
    }

    /// <summary>
    /// 총알 능력치 초기화때 쓸 프로퍼티, 플레이어 이동 방향
    /// </summary>
    public Vector2 MoveDir
    {
        get => bodyDir;
    }

    new CircleCollider2D collider;
    #endregion
    #region 체력
    /// <summary>
    /// 최대체력
    /// </summary>
    public int maxHealth = 6;
    /// <summary>
    /// 현재 체력
    /// </summary>
    public int health = 1;
    #endregion
    #region 데미지
    /// <summary>
    /// 눈물에 넣어줄 데미지
    /// </summary>
    [SerializeField]
    float damage;
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
    bool isGetPolyphemus = false;
    bool isGetScaredHeart = false;
    #endregion
    #region 무적
    /// <summary>
    /// 무적시간
    /// </summary>
    float invisibleTime = 1.16f;
    /// <summary>
    /// 무적시간 초기화용
    /// </summary>
    float currentInvisible = 0.0f;
    #endregion
    #region 프로퍼티들
    public Action onUseActive;
    public int Coin { get; set; }
    public int Bomb { get; set; }

    int key = 0;
    public int Key {
        get => key;
        set {
            key = value;
        }
    }
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
    public int Health {
        get => health;
        set {
            health = value;
            health = Mathf.Clamp(health, 0, maxHealth);
            onHealthChange?.Invoke();
        }
    }
    int soulHealth = 0;
    public int SoulHealth {
        get => soulHealth;
        set {
            if(soulHealth != value) {
                soulHealth = Math.Max(0, value);
                onHealthChange?.Invoke();
            }
        }
    }
    #endregion

    public Action<PassiveItemData> getPassiveItem;
    public Action<ActiveItemData> getActiveItem;

    /// <summary>
    /// 플레이어의 HP가 변경되었음을 알리는 델리게이트
    /// </summary>
    public Action onHealthChange;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        inputAction = new PlayerAction();
        getItem = transform.GetChild(1);
        getItemSR = getItem.GetComponent<SpriteRenderer>();
        // 몸통 관련 항목
        body = transform.GetChild(2);
        bodyAni = body.GetComponent<Animator>();
        // 머리 관련 항목
        head = transform.GetChild(3);
        headAni = head.GetComponent<Animator>();
        
        TearSpeedCaculate();
    }
    private void Start()
    {
        Health = maxHealth;
        Speed = 2.5f;
        Damage = 3.5f;
    }
    private void Update()
    {
        Vector3 dir = new Vector3(bodyDir.x * Speed * Time.deltaTime, bodyDir.y * Speed * Time.deltaTime, 0);
        transform.position += dir;
        currentTearDelay -= Time.deltaTime;
        currentInvisible -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        ShootingTear();
    }
    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMove;
        inputAction.Player.Move.canceled += OnMove;
        inputAction.Player.Shot.performed += OnFire;
        inputAction.Player.Shot.canceled += OnFire;
        inputAction.Player.Bomb.performed += SetBomb;
        inputAction.Player.Active.performed += OnActive;
    }
    private void OnDisable()
    {
        inputAction.Player.Move.performed -= OnMove;
        inputAction.Player.Move.canceled -= OnMove;
        inputAction.Player.Shot.performed -= OnFire;
        inputAction.Player.Shot.canceled -= OnFire;
        inputAction.Player.Bomb.performed -= SetBomb;
        inputAction.Player.Active.performed -= OnActive;
        inputAction.Player.Disable();
    }
    private void SetBomb(InputAction.CallbackContext context)
    {
        // 폭탄의 개수가 0보다 크면 개수를 1개 줄이고 폭탄 생성
        if (Bomb > 0) {
            Bomb--;
            GameObject bomb = Instantiate(bombObj);
            bomb.transform.position = transform.position;
        }
    }
    private void OnActive(InputAction.CallbackContext context)
    {
        onUseActive?.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damaged();
            KnockBack(collision);
            Debug.Log("적과 충돌/ 남은 체력 : " + health);
        }

        // 프롭스 태그를 가진 오브젝트와 충돌 했을 때
        if (collision.gameObject.CompareTag("Props")) 
        {
            // 아이템 데이터 확인
            ItemDataObject props = collision.gameObject.GetComponent<ItemDataObject>();
            if (props != null) 
            {
                // 아이템 데이터 안에 IConsumable 인터페이스가 있는지 확인
                IConsumable consum = props.ItemData as IConsumable;
                if (consum != null) 
                {
                    // 아이템 삭제 여부 확인후 아이템 삭제 ( 코인은 삭제 애니메이션 때문에 삭제 하면 안됨)
                    if (consum.Consume(this.gameObject)) {  
                        Destroy(collision.gameObject);
                    }
                    return;
                }

                // 아이템 데이터 안에 IHealth 인터페이스가 있는지 확인
                IHealth heart = props.ItemData as IHealth;
                if (heart != null) 
                {
                    // 아이템으로 힐이 성공 했을 때 그 아이템을 삭제
                    if (heart.Heal(this.gameObject))
                    {
                        Destroy(collision.gameObject);
                        return;
                    }
                }
            }
        }

        // Item 태그를 가진 오브젝트와 충돌 했을 때
        if (collision.gameObject.CompareTag("Item"))
        {
            ItemDataObject item = collision.gameObject.GetComponent<ItemDataObject>();
            ItemData itemData = item.ItemData;
            if (itemData != null)
            {
                ActiveItemData active = itemData as ActiveItemData;
                if (active != null) 
                {
                    getActiveItem?.Invoke(active);
                    StartCoroutine(GetItem(active.icon));
                }

                PassiveItemData passive = itemData as PassiveItemData;
                if (passive != null)
                {
                    getPassiveItem?.Invoke(passive);
                    StartCoroutine(GetItem(passive.icon));

                    currentDmg += passive.damage;
                    currentMultiDmg *= passive.multiDamage;
                    Speed += passive.speed;
                    Range += passive.range;
                    ShotSpeed += passive.shotSpeed;
                    itemSpeed += passive.tearSpeed;

                    // 데미지 적용시 예외 아이템 switch문
                    switch (passive.itemNum)
                    {
                        case 169:
                            isGetPolyphemus = true;
                            break;
                        case 182:
                            isGetScaredHeart = true;
                            break;
                    }

                    if (passive.itemNum == 169 || passive.itemNum == 182)
                        currentDmg -= passive.damage;

                    Damage = baseDmg * Mathf.Sqrt(currentDmg * 1.2f + 1f);

                    if (isGetPolyphemus)
                        Damage += 4f;

                    Damage *= currentMultiDmg;

                    if (isGetScaredHeart)
                        Damage += 1f;

                    Damage = (float)Math.Round(Damage, 2);
                    if (speed > maximumSpeed) 
                    {
                        speed = maximumSpeed;
                    }
                    TearSpeedCaculate();
                }
            }

            Destroy(collision.gameObject);
        }
    }
    IEnumerator GetItem(Sprite sprite)
    {
        bodyAni.SetTrigger("GetItem");
        head.gameObject.SetActive(false);
        getItemSR.sprite = sprite;
        inputAction.Player.Shot.Disable();

        yield return new WaitForSeconds(0.9f);

        inputAction.Player.Shot.Enable();
        getItemSR.sprite = null;
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
        Transform tearSpawn = transform.GetChild(0);

        GameObject tear = Factory.Inst.GetObject(PoolObjectType.PenetrationTear, tearSpawn.position); // 추적 눈물로 실험 중

        currentTearDelay = tearFire; // 딜레이 시간 초기화

        yield return new WaitForSeconds(tearFire);
    }
    public Action isFire;
    private void Damaged()
    {
        if (SoulHealth <= 0)
        {
            if (Health > 0)
            {
                StartCoroutine(InvisibleTime());
                Health--;
                if (Health == 0)
                {
                    Die();
                }
                else
                {
                    bodyAni.SetTrigger("Damage");
                    head.gameObject.SetActive(false);
                }
            }
            else if (Health <= 0)
            {
                Die();
            }
        }
        else
        {
            StartCoroutine(InvisibleTime());
            SoulHealth--;
            bodyAni.SetTrigger("Damage");
            head.gameObject.SetActive(false);
        }
    }
    IEnumerator InvisibleTime()
    {
        inputAction.Player.Shot.Disable();
        collider.enabled = false;
        currentInvisible = invisibleTime;

        yield return new WaitForSeconds(currentInvisible);

        rigid.velocity = Vector3.zero;
        inputAction.Player.Shot.Enable();
        head.gameObject.SetActive(true);
        collider.enabled = true;
    }
    private void ShootingTear()
    {
        if (isShoot == true)
        {
            if (IsAttackReady)
            {
                new WaitForSeconds(0.258f);
                headAni.speed = Mathf.Lerp(minHeadAni, maxHeadAni, itemSpeed);
                StartCoroutine(TearDelay());
            }
        }
    }
    void TearSpeedCaculate()
    {
        if(itemSpeed >= 3.5f)
        {
            itemSpeed = 3.5f;
        }
        tearDelay = 16.0f - 6.0f * Mathf.Sqrt(itemSpeed * 1.3f + 1.0f);
        tearDelay = (float)Math.Round(tearDelay, 1);
        TearSpeed = 30 / (tearDelay + 1);
        TearSpeed = (float)Math.Round(tearSpeed, 2);
        if (TearSpeed >= fireRate)
        {
            TearSpeed = fireRate;
        }
        tearFire = 1 / TearSpeed;
        tearFire = (float)Math.Round(tearFire, 2);
    }
    private void Die()
    {
        StopAllCoroutines();
        bodyAni.SetTrigger("Die");
        inputAction.Player.Disable();
        collider.enabled = false;
        head.gameObject.SetActive(false);
    }
    private void KnockBack(Collision2D collision)
    {
        rigid.AddForce((transform.position - collision.transform.position).normalized * 10.0f, ForceMode2D.Impulse);
    }
}



