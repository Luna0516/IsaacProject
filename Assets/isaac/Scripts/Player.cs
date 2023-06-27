using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionObject 
{
    public GameObject[] aaaaa;
}
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
    public GameObject Bomb;
    /// <summary>
    /// 액티브 아이템
    /// </summary>
    public GameObject ActiveItem;
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed;
    /// <summary>
    /// 눈물 연사 속도
    /// </summary>
    public float attackSpeed;
    /// <summary>
    /// 사거리
    /// </summary>
    public float range = 6.5f;
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

    // 체력 프로퍼티 Health로 설정
    float Health
    {
        get => health;
        set { }
    }
    private void Awake()
    {
        // 스텟 초기화
        speed = 1.0f;
        damage = 1.0f;
        attackSpeed = 1.0f;
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
        playerAction.Bomb.Bomb.performed += SetBomb;
        playerAction.Bomb.Bomb.canceled += SetBomb;
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
        playerAction.Bomb.Bomb.performed -= SetBomb;
        playerAction.Bomb.Bomb.canceled -= SetBomb;
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
            switch(collision.gameObject.GetComponent<ItemBase>().ItemNum)
            {
                case 0:
                    break;
                case 1:
                    ItemBase theSadOnion = collision.gameObject.GetComponent<TheSadOnion>();
                    damage = theSadOnion.Attack + damage;
                    speed = theSadOnion.Speed + speed;
                    attackSpeed = theSadOnion.AttackSpeed - attackSpeed;
                    break;
            }
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
    private void SetBomb(InputAction.CallbackContext context) // 폭탄 딜레이
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
        GameObject bomb = Instantiate(Bomb);

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

        Bullet tearComponent = tears.GetComponent<Bullet>();

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

