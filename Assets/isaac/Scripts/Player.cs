using System;
using System.Collections;
using System.Collections.Generic;
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
    SpriteRenderer headSR;
    // 몸
    Transform body;
    // 몸 애니
    Animator bodyAni;
    SpriteRenderer bodySR;
    // 플레이어 액션
    PlayerAction inputAction;
    Transform getItem;
    SpriteRenderer getItemSR;
    Transform sadOnionSprite;
    Animator sadOnionAni;
    SpriteRenderer sadOnionSR;
    Transform martyrSprite;
    Transform allGetItem;
    Animator brimstoneAni;
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

    /// <summary>
    /// 스프라이트 변경 아이템을 먹지 않았을때
    /// </summary>
    bool isEmpty = true;

    bool isGetitem;

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
    bool isGetSacredHeart = false;
    bool isGetSadOnion = false;
    bool isGetBrimstone = false;
    //bool isGetMartyr = false;
    #endregion
    #region 무적
    /// <summary>
    /// 무적시간 초기화용
    /// </summary>
    float invisibleTime = 0.9f;
    /// <summary>
    /// 무적시간
    /// </summary>
    float currentInvisible = 0.0f;

    bool isDamaged = false;
    #endregion
    #region 프로퍼티들
    public Action onUseActive;
    int coin = 0;
    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
            onCoinChange?.Invoke(coin);
        }
    }
    /// <summary>
    /// 코인 개수 변경을 알리는 델리게이트
    /// </summary>
    public Action<int> onCoinChange;
    int bomb = 0;
    public int Bomb
    {
        get => bomb;
        set
        {
            bomb = value;
            onBombChange?.Invoke(bomb);
        }
    }
    /// <summary>
    /// 폭탄 개수 변경을 알리는 델리게이트
    /// </summary>
    public Action<int> onBombChange;
    int key = 0;
    public int Key {
        get => key;
        set {
            key = value;
            onKeyChange?.Invoke(key);
        }
    }
    /// <summary>
    /// 열쇠 개수 변경을 알리는 델리게이트
    /// </summary>
    public Action<int> onKeyChange;
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
    #region 스프라이트관련
    public enum PassiveSpriteState
    {
        None = 0,
        CricketHead,
        Halo,
        SadOnion,
        SacredHeart,
        Polyphemus,
        MutantSpider,
        Brimstone,
        BloodOfMartyr
    }
    PassiveSpriteState state = PassiveSpriteState.None;
    public PassiveSpriteState State
    {
        get => state;
        set
        {
            state = value;
            switch (state)
            {
                case PassiveSpriteState.None:
                    isEmpty = true;
                    break;
                case PassiveSpriteState.CricketHead:
                    var headResourceName = "HeadAC/Head_Cricket_AC";
                    headAni.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(headResourceName);
                    isEmpty = false;
                    break;
                case PassiveSpriteState.Halo:
                    isEmpty = false;
                    break;
                case PassiveSpriteState.SadOnion:
                    isEmpty = false;
                    break;
                case PassiveSpriteState.SacredHeart:
                    isEmpty = false;
                    isGetSacredHeart = true;
                    break;
                case PassiveSpriteState.Polyphemus:
                    isEmpty = false;
                    isGetPolyphemus = true;
                    break;
                case PassiveSpriteState.MutantSpider:
                    isEmpty = false;
                    headResourceName = "HeadAC/Head_Mutant_AC";
                    headAni.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(headResourceName);
                    break;
                case PassiveSpriteState.Brimstone:
                    isEmpty = false;
                    headResourceName = "HeadAC/Head_Brimstone_AC";
                    headAni.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(headResourceName);
                    var bodyResourceName = "BodyAC/Body_Brimstone_AC";
                    bodyAni.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load(bodyResourceName);
                    isGetBrimstone = true;
                    break;
                case PassiveSpriteState.BloodOfMartyr:
                    isEmpty = false;
                    break;
                default:
                    break;
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
    bool shotbrim => brimDelay < 0;
    float brimDelay;
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
        bodySR = body.GetComponent<SpriteRenderer>();
        // 머리 관련 항목
        head = transform.GetChild(3);
        headAni = head.GetComponent<Animator>();
        headSR = head.GetComponent<SpriteRenderer>();
        
        TearSpeedCaculate();
    }
    private void Start()
    {
        // 아이템관련
        allGetItem = transform.GetChild(4);
        sadOnionSprite = allGetItem.transform.GetChild(0);
        sadOnionAni = sadOnionSprite.GetComponent<Animator>();
        sadOnionSR = sadOnionSprite.GetComponent<SpriteRenderer>();
        martyrSprite = allGetItem.transform.GetChild(1);
        brimstoneAni = FindObjectOfType<Animator>();

        isGetitem = true;
        Health = maxHealth;
        Speed = 2.5f;
        Damage = 3.5f;
    }
    private void Update()
    {
        brimDelay -= Time.deltaTime;
        //Debug.Log(brimDelay);
        currentTearDelay -= Time.deltaTime;
        currentInvisible -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(bodyDir.x * Speed * Time.fixedDeltaTime, bodyDir.y * Speed * Time.fixedDeltaTime, 0);
        transform.position += dir;
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
                PropsItemData propsItem = props.ItemData as PropsItemData;
                if (propsItem != null) 
                {
                    switch (propsItem.propsType)
                    {
                        case PropsItem.Penny:
                        case PropsItem.Nickel:
                        case PropsItem.Dime:
                            Coin += propsItem.itemValues;
                            break;
                        case PropsItem.Bomb:
                        case PropsItem.DoubleBomb:
                            Bomb += propsItem.itemValues;
                            break;
                        case PropsItem.Key:
                        case PropsItem.KeyRing:
                            Key += propsItem.itemValues;
                            break;
                        default:
                            break;
                    }

                    if (propsItem.Consume(gameObject))
                    {
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

        if (collision.gameObject.CompareTag("Item"))
        {
            if (isGetitem)
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

                        switch (passive.itemNum)
                        {
                            case 1:
                                State = PassiveSpriteState.SadOnion;
                                break;
                            // 크리켓
                            case 4:
                                State = PassiveSpriteState.CricketHead;
                                break;
                            // 가시관
                            case 7:
                                State = PassiveSpriteState.BloodOfMartyr;
                                break;
                            // 혈사포
                            case 118:
                                State = PassiveSpriteState.Brimstone;
                                break;
                            // 왕눈이눈물
                            case 169:
                                State = PassiveSpriteState.Polyphemus;
                                break;
                            // 유도눈물
                            case 182:
                                State = PassiveSpriteState.SacredHeart;
                                break;
                            // 칼
                            case 114:
                                break;
                            // 거미눈물 4발
                            case 153:
                                State = PassiveSpriteState.MutantSpider;
                                break;
                        }

                        if (passive.itemNum == 169 || passive.itemNum == 182)
                            currentDmg -= passive.damage;

                        Damage = baseDmg * Mathf.Sqrt(currentDmg * 1.2f + 1f);

                        if (isGetPolyphemus)
                            Damage += 4f;

                        Damage *= currentMultiDmg;

                        if (isGetSacredHeart)
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
    }
    IEnumerator GetItem(Sprite sprite)
    {
        bodyAni.SetTrigger("GetItem");
        head.gameObject.SetActive(false);
        getItemSR.sprite = sprite;
        inputAction.Player.Shot.Disable();
        isGetitem = false;
        yield return new WaitForSeconds(0.9f);

        inputAction.Player.Shot.Enable();
        getItemSR.sprite = null;
        head.gameObject.SetActive(true);
        isGetitem = true;
        if(State == PassiveSpriteState.SadOnion)
        {
            sadOnionSprite.gameObject.SetActive(true);
            isGetSadOnion = true;
        }
        if (State == PassiveSpriteState.BloodOfMartyr)
        {
            martyrSprite.gameObject.SetActive(true);
            //isGetMartyr = true;
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
        if (isGetSadOnion)
        {
            sadOnionAni.SetFloat("MoveDir_X", bodyDir.x);
            sadOnionAni.SetFloat("MoveDir_Y", bodyDir.y);
            if (bodyDir.y > 0.001f)
            {
                sadOnionSR.sortingOrder = 0;
            }
            else
            {
                sadOnionSR.sortingOrder = 2;
            }
        }
    }
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        headDir = value;
        headAni.SetFloat("ShootDir_X", headDir.x);
        headAni.SetFloat("ShootDir_Y", headDir.y);
        if (isGetSadOnion)
        {
            sadOnionAni.SetFloat("ShotDir_Y", headDir.y);
            sadOnionAni.SetFloat("ShotDir_X", headDir.x);
            if (headDir.y > 0.001f)
            {
                sadOnionSR.sortingOrder = 0;
            }
            else
            {
                sadOnionSR.sortingOrder = 2;
            }
        }
        if (context.performed)
        {
            if (isEmpty || !isGetBrimstone)
            {
                headAni.SetBool("isNormalShoot", true);
            }
            if (isGetBrimstone)
            {
                brimstoneDir = headDir;
                headAni.SetBool("BrimstoneCharge", true);
                headAni.SetBool("CanShot", false);
                brimDelay = 0.8f;
                Time.timeScale = 0.5f;
            }
            if (isGetSadOnion)
            {
                sadOnionAni.SetBool("isShot", true);
            }
            isShoot = true;
        }
        else if (context.canceled)
        {
            if (isEmpty || !isGetBrimstone)
            {
                headAni.SetBool("isNormalShoot", false);
            }
            if (isGetBrimstone)
            {
                StartCoroutine(ShootBrimstone());
                if (shotbrim)
                {
                    headAni.SetBool("CanShot", true);
                }
                Time.timeScale = 1f;
            }
            if (isGetSadOnion)
            {
                sadOnionAni.SetBool("isShot", false);
            }
            isShoot = false;
        }
        if (headDir.x > 0 && headDir.x < 0 || headDir.y != 0)
        {
            headDir.x = 0;
            headDir.Normalize();
        }
    }
    Vector2 brimstoneDir = Vector2.zero;
    IEnumerator ChargeBrimstone()
    {
        yield return null;
    }
    IEnumerator ShootBrimstone()
    {
        headAni.SetFloat("ShootDir_X", brimstoneDir.x);
        headAni.SetFloat("ShootDir_Y", brimstoneDir.y);
        headAni.SetBool("BrimstoneCharge", false);
        yield return new WaitForSeconds(0.5f);
        headAni.SetFloat("ShootDir_X", 0);
        headAni.SetFloat("ShootDir_Y", 0);
        brimstoneDir = Vector2.zero;
        headDir = Vector2.zero;
    }
    IEnumerator TearDelay()
    {
        Transform tearSpawn = transform.GetChild(0);
        GameObject tear;
        if (isEmpty)
        {
            tear = Factory.Inst.GetObject(PoolObjectType.Tear, tearSpawn.position);
        }
        if (isGetPolyphemus)
        {
            tear = Factory.Inst.GetObject(PoolObjectType.BigTear, tearSpawn.position);
            isEmpty = false;
        }
        else if (isGetSacredHeart)
        {
            tear = Factory.Inst.GetObject(PoolObjectType.GuidedTear, tearSpawn.position);
            isEmpty = false;
        }

        currentTearDelay = tearFire; // 딜레이 시간 초기화

        yield return new WaitForSeconds(tearFire);
    }
    public Action isFire;
    public void Damaged()
    {
        if (SoulHealth <= 0)
        {
            if (Health > 0)
            {
                isDamaged = true;
                health--;
                if (Health == 0)
                {
                    Die();
                }
                else
                {
                    currentInvisible = invisibleTime;
                    StartCoroutine(InvisibleTime());
                }
            }
            else if (Health <= 0)
            {
                Die();
            }
        }
        else
        {
            // 여긴 소울 하트 있을때 발생하는 부분인데 뭔가 이상한 부분 있으면 고쳐주세요 ㅜㅜ <====> 신우철
            StartCoroutine(InvisibleTime());
            SoulHealth--;
            bodyAni.SetTrigger("Damage");
            head.gameObject.SetActive(false);
        }
    }
    IEnumerator InvisibleTime()
    {
        StartCoroutine(Invisible());
        head.gameObject.SetActive(false);
        bodyAni.SetTrigger("Damage");
        collider.enabled = false;
        inputAction.Player.Shot.Disable();
        isDamaged = true;

        yield return new WaitForSeconds(0.3f);
        inputAction.Player.Shot.Enable();
        head.gameObject.SetActive(true);

        yield return new WaitForSeconds(currentInvisible);
        collider.enabled = true;
        isDamaged = false;
    }
    IEnumerator Invisible()
    {
        while(isDamaged)
        {
            headSR.color = new(1, 1, 1, 0);
            bodySR.color = new(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            headSR.color = new(1, 1, 1, 1);
            bodySR.color = new(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
        }
        
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



