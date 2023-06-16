using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// InputAction ����
    /// </summary>
    PlayerAction playerAction;
    /// <summary>
    /// body ����
    /// </summary>
    Vector2 dir1 = Vector2.zero;
    /// <summary>
    /// head ����
    /// </summary>
    Vector2 dir2 = Vector2.zero;
    /// <summary>
    /// ����
    /// </summary>
    public GameObject Tears;
    /// <summary>
    /// ��ź
    /// </summary>
    public GameObject Bomb;
    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float speed = 1.0f;
    /// <summary>
    /// ���� ���� �ӵ�
    /// </summary>
    public float tearsSpeed = 2.73f;
    /// <summary>
    /// ��Ÿ�
    /// </summary>
    public float range = 6.5f;
    /// <summary>
    /// �ִ� ü��
    /// </summary>
    public float maxHealth = 6.0f;

    public float Health;

    private bool tearDelay;

    private bool isAutoClick;

    const float bombSpawn = 2.0f;

    private bool bombDelay;

    Transform body;

    Transform head;

    Animator headAni;

    Animator bodyAni;

    SpriteRenderer bodySR;

    Coroutine Setbomb;

    private void Awake()
    {
        playerAction = new PlayerAction();

        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();

        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        IEnumerator Setbomb = BombSpawnDelay();


        tearDelay = true;
    }

    
    
    private void Update()
    {
        Vector3 dir = new Vector3(dir1.x * speed * Time.deltaTime, dir1.y * speed * Time.deltaTime, 0f);
        transform.position += dir;
    }

    private void FixedUpdate()
    {
        if(isAutoClick == true)
        {
            if (tearDelay) //true �϶�
            {
                StartCoroutine(TearShootCoroutine());  
            }
        }
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
        playerAction.Bomb.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
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
            isAutoClick = true;
        }
        else if(context.canceled)
        {
            isAutoClick = false;
        }
    }

    private void SetBomb(InputAction.CallbackContext context)
    {
        Debug.Log("��ź");

        GameObject bomb = Instantiate(Bomb);


        if (context.performed)
        {
            while (bombDelay)
            {
                bomb.transform.position = body.transform.position;
                bombDelay = false;
                StartCoroutine(BombSpawnDelay());
            }
        }
    }

    IEnumerator BombSpawnDelay()
    {
        yield return new WaitForSeconds(bombSpawn);
        bombDelay = true;
    }

    IEnumerator TearShootCoroutine()
    {
        GameObject tears = Instantiate(Tears);

        Transform tearspawn = transform.GetChild(0);

        tears.transform.position = tearspawn.position;

        Bullet tearComponent = tears.GetComponent<Bullet>();

        tearComponent.dir = dir2;

        tearDelay = false;

        yield return new WaitForSeconds(tearsSpeed);

        tearDelay = true;
    }
    // ���� ������ = �÷��̾� ������ => �������� �Ծ��� �� �÷��̾� ���������� ������ ����
    // ��ź ������ => �ϳ� �������� 2�� ������ ������ (��ź �����̴� ����. const ���ô�.)
}

