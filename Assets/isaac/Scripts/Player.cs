using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    ItemBase item;
    /// <summary>
    /// InputAction 연결
    /// </summary>
    PlayerAction playerAction;
    /// <summary>
    /// body 백터
    /// </summary>
    Vector2 dir1 = Vector2.zero;
    /// <summary>
    /// head 벡터
    /// </summary>
    Vector2 dir2 = Vector2.zero;
    /// <summary>
    /// 눈물
    /// </summary>
    public GameObject Tears;   
    /// <summary>
    /// 폭탄
    /// </summary>
    public GameObject SetBomb;
    /// <summary>
    /// 액티브 아이템
    /// </summary>
    public GameObject ActiveItem;
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed;
    /// <summary>
    /// 화면에 나올 연사속도
    /// </summary>
    public float tearSpeed = 2.73f;
    /// <summary>
    /// 눈물 연사 속도 계산
    /// </summary>
    float attackSpeed;
    /// <summary>
    /// 연사 맥스
    /// </summary>
    float maxAttackSpeed = 1.0f;
    /// 공격속도의 최대값(기본값. 연사 맥스가 올라가면 상한 사라짐)
    /// </summary>
    float maximumTearSpeed = 5.0f;
    /// <summary>
    /// 이동속도의 최대값
    /// </summary>
    const float maximumSpeed = 5.0f;
    /// <summary>
    /// 사거리
    /// </summary>
    public float range;
    /// <summary>
    /// 눈물 날아가는 속도
    /// </summary>
    public float shotSpeed = 1.0f;
    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHealth = 6.0f;
    /// <summary>
    /// 체력
    /// </summary>
    public float health;
    /// <summary>
    /// 눈물에 넣어줄 데미지
    /// </summary>
    public float damage;
    /// <summary>
    /// 데미지 배수
    /// </summary>
    float multiDmg = 1.0f;
    /// <summary>
    /// 눈물 딜레이 1차확인
    /// </summary>
    private bool isAutoTear;
    /// <summary>
    /// 눈물 딜레이 2차확인
    /// </summary>
    private bool tearDelay;
    /// <summary>
    /// 폭탄 딜레이 1차확인
    /// </summary>
    private bool bombDelay;
    /// <summary>
    /// 폭탄 딜레이 2차확인
    /// </summary>
    private bool isAutoBomb;
    /// <summary>
    /// 폭탄 스폰 시간 (고정)
    /// </summary>
    const float bombSpawn = 2.0f;
    /// <summary>
    /// 아이템 먹는 애니메이션 끝나는 시간
    /// </summary>
    private float itemDelay = 2.0f;
    /// <summary>
    /// 머리 Transform
    /// </summary>
    Transform head;
    /// <summary>
    /// 머리 Animator
    /// </summary>
    Animator headAni;
    /// <summary>
    /// 몸뚱아리 Transform
    /// </summary>
    Transform body;
    /// <summary>
    /// 몸통 Animator
    /// </summary>
    Animator bodyAni;
    /// <summary>
    /// 몸통(좌우변경) SpriteRenderer
    /// </summary>
    SpriteRenderer bodySR;

    public int Coin { get; set; }
    public int Bomb { get; set; }
    public int Key { get; set; }
    public float Damage

    {
        get => damage;
        private set => damage = value;
    }
    public float Speed
    {
        get => speed;
        private set => speed = value;
    }
    public float TearSpeed 
    {
        get => tearSpeed;
        private set => tearSpeed = value;
    }
    public float ShotSpeed
    {
        get => shotSpeed;
        private set => shotSpeed = value;
    }
    public float Range
    {
        get => range;
        private set => range = value;
    }
    public float Health
    {
        get => health; 
        set 
        { 
            health = value; 
        }
    }

    private void Awake()
    {
        // 스텟 초기화
        //speed = 1.0f;

        damage = 3.5f;
        // 인풋시스템
        playerAction = new PlayerAction();
        // 몸통 관련 항목
        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();
        // 머리 관련 항목
        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        // 폭탄, 눈물 딜레이 true 변경
        bombDelay = true;
        tearDelay = true;
    }

    private void Update()
    {
        // 몸통 움직일 때 벡터값
        Vector3 dir = new Vector3(dir1.x * speed * Time.deltaTime, dir1.y * speed * Time.deltaTime, 0f);
        transform.position += dir;
    }

    private void FixedUpdate()
    {
        // 폭탄 딜레이
        BombDelay();
        // 눈물 딜레이
        TearDelay();

        attackSpeed = maxAttackSpeed / tearSpeed;
        if (tearSpeed > maximumTearSpeed)
        {
            tearSpeed = maximumTearSpeed;
        }

        /*
        Debug.Log($"현재 공격속도 : {attackSpeed}");
        Debug.Log($"최대 공격속도 : {maxAttackSpeed}");
        Debug.Log($"공격속도 저장 : {tearSpeed}");
        */
    }

    private void OnEnable()
    {
        playerAction.Move.Enable();
        playerAction.Move.WASD.performed += OnMove;
        playerAction.Move.WASD.canceled += OnMove;
        playerAction.Shot.Enable();
        playerAction.Shot.Cross.performed += OnFire;
        playerAction.Shot.Cross.canceled += OnFire;
        playerAction.Bomb.Enable();
        playerAction.Bomb.Bomb.performed += SetBombDelay;
        playerAction.Bomb.Bomb.canceled += SetBombDelay;
        playerAction.Active.Enable();
        playerAction.Active.Active.performed += OnActiveItem;
    }

    private void OnDisable()
    {
        playerAction.Move.WASD.performed -= OnMove;
        playerAction.Move.WASD.canceled -= OnMove;
        playerAction.Move.Disable();
        playerAction.Shot.Cross.performed -= OnFire;
        playerAction.Shot.Cross.canceled -= OnFire;
        playerAction.Shot.Disable();
        playerAction.Bomb.Bomb.performed -= SetBombDelay;
        playerAction.Bomb.Bomb.canceled -= SetBombDelay;
        playerAction.Bomb.Disable();
        playerAction.Active.Active.performed -= OnActiveItem;
        playerAction.Active.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            Debug.Log("적과 충돌/ 남은 체력 : " + health);
            if(health <= 0)
            {
                Die();
            }
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            StartCoroutine(GetItemDelay());
            Destroy(collision.gameObject);
            switch(collision.gameObject.GetComponent<Item>().ItemNum)
            {
                case 0:
                    break;
                case 1:
                    Item theSadOnion = collision.gameObject.GetComponent<TheSadOnion>().theSadOnion;
                    damage = theSadOnion.Attack + damage;
                    speed = theSadOnion.Speed + speed;
                    tearSpeed = theSadOnion.TearSpeed + tearSpeed;
                    break;
                case 169:
                    Item polyphemus = collision.gameObject.GetComponent<Polyphemus>().polyphemus;
                    damage = polyphemus.Attack + damage;
                    speed = polyphemus.Speed + speed;
                    tearSpeed = polyphemus.TearSpeed + tearSpeed;
                    multiDmg = polyphemus.MultiDmg * multiDmg;
                    break;
                case 182:
                    Item sacredHeart = collision.gameObject.GetComponent<SacredHeart>().sacredHeart;
                    multiDmg = sacredHeart.MultiDmg * multiDmg;
                    damage = sacredHeart.Attack + damage;
                    speed = sacredHeart.Speed + speed;
                    tearSpeed = sacredHeart.TearSpeed + tearSpeed;
                    break;
            }
            damage = damage * multiDmg;
            multiDmg = 1.0f;
        }

    }

    private void Die()
    {
        head.gameObject.SetActive(false);
        bodyAni.SetTrigger("Die");
        playerAction.Move.Disable();
        playerAction.Shot.Disable();
    }

    private void OnMove(InputAction.CallbackContext context) // 몸통 움직임
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir1 = value;
        //Debug.Log(value);

        if (dir1.x == 0 && dir1.y == 0)
        {
            bodyAni.SetBool("isMove", false);
            headAni.SetBool("isMove", false);
        }
        else
        {
            bodyAni.SetBool("isMove", true);
            headAni.SetBool("isMove", true);
            
            bodyAni.SetFloat("MoveDir_Y", dir1.y);
            headAni.SetFloat("Dir_Y1", dir1.y);
            
            if (dir1.x < 0)
            {
                bodySR.flipX = true;
            }
            else
            {
                bodySR.flipX = false;
            }
            bodyAni.SetFloat("MoveDir_X", dir1.x);
            headAni.SetFloat("Dir_X1", dir1.x);
        }
    }
    /// <summary>
    /// 눈물 발사
    /// </summary>
    /// <param name="context"></param>
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir2 = value.normalized;
        //Debug.Log(value);

        if (dir2.x == 0 && dir2.y == 0)
        {
            headAni.SetBool("isShoot", false);
        }
        else
        {
            headAni.SetBool("isShoot", true);
            if(dir2.x != 0 && dir2.y != 0)
            {
                dir2.x = 0;
            }
            headAni.SetFloat("Dir_X2", dir2.x);
            headAni.SetFloat("Dir_Y2", dir2.y);
        }

        if (context.performed)
        {
            isAutoTear = true;
        }
        else if(context.canceled)
        {
            isAutoTear = false;
        }
    }
    /// <summary>
    /// 폭탄 딜레이
    /// </summary>
    /// <param name="context"></param>
    private void SetBombDelay(InputAction.CallbackContext context) // 폭탄 딜레이
    {
        if (context.performed)
        {
            isAutoBomb = true;
        }
        else if(context.canceled)
        {
            isAutoBomb = false;
        }
    }
    /// <summary>
    /// 액티브 사용
    /// </summary>
    /// <param name="context"></param>
    private void OnActiveItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }
    /// <summary>
    /// 폭탄 딜레이 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator BombSpawnDelay() // 폭탄 딜레이 코루틴
    {
        GameObject bomb = Instantiate(SetBomb);

        bomb.transform.position = body.transform.position;

        bombDelay = false;
        yield return new WaitForSeconds(bombSpawn);
        bombDelay = true;
    }
    /// <summary>
    /// 폭탄 딜레이
    /// </summary>
    void BombDelay()
    {
        // 폭탄 딜레이
        if (isAutoBomb == true) // 1차확인이 true일때
        {
            if (bombDelay) // 2차 확인까지 true가 되면
            {
                StartCoroutine(BombSpawnDelay()); // 코루틴 실행
            }
        }
    }
    /// <summary>
    /// 눈물 발사 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator TearShootCoroutine() // 눈물 발사 코루틴
    {
        GameObject tears = Instantiate(Tears);

        Transform tearspawn = transform.GetChild(0);

        tears.transform.position = tearspawn.position;

        AttackBase tearComponent = tears.GetComponent<AttackBase>();

        tearComponent.damage = damage;

        tearComponent.dir = dir2;

        tearDelay = false;

        yield return new WaitForSeconds(attackSpeed);

        tearDelay = true;
    }
    /// <summary>
    /// 눈물 딜레이
    /// </summary>
    void TearDelay()
    {
        // 눈물 딜레이 
        if (isAutoTear == true) // 1차 확인이 true 일때 
        {
            if (tearDelay) // 2차 확인까지 true가 되면
            {
                StartCoroutine(TearShootCoroutine()); // 코루틴 실행
            }
        }
    }

    IEnumerator GetItemDelay()
    {
        bodyAni.SetBool("isGetItem", true);
        head.gameObject.SetActive(false);
        yield return new WaitForSeconds(itemDelay);
        bodyAni.SetBool("isGetItem", false);
        head.gameObject.SetActive(true);
    }
}
// 아이템에는 스텟이 담겨있으니
// 아이템을 한번만 먹으면 적용되는 식으로

