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
    /// ��Ƽ�� ������
    /// </summary>
    public GameObject ActiveItem;
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
    /// <summary>
    /// ü��
    /// </summary>
    public float Health;
    /// <summary>
    /// ������ �־��� ������
    /// </summary>
    public float damage;
    /// <summary>
    /// ���� ������ 1��Ȯ��
    /// </summary>
    private bool isAutoTear;
    /// <summary>
    /// ���� ������ 2��Ȯ��
    /// </summary>
    private bool tearDelay;
    /// <summary>
    /// ��ź ������ 1��Ȯ��
    /// </summary>
    private bool bombDelay;
    /// <summary>
    /// ��ź ������ 2��Ȯ��
    /// </summary>
    private bool isAutoBomb;
    /// <summary>
    /// ��ź ���� �ð� (����)
    /// </summary>
    const float bombSpawn = 2.0f;
    float waitTime;
    /// <summary>
    /// �Ӹ� Transform
    /// </summary>
    Transform head;
    /// <summary>
    /// �Ӹ� Animator
    /// </summary>
    Animator headAni;
    /// <summary>
    /// ���׾Ƹ� Transform
    /// </summary>
    Transform body;
    /// <summary>
    /// ���� Animator
    /// </summary>
    Animator bodyAni;
    /// <summary>
    /// ����(�¿캯��) SpriteRenderer
    /// </summary>
    SpriteRenderer bodySR;

    private void Awake()
    {
        // ��ǲ�ý���
        playerAction = new PlayerAction();
        // ���� ���� �׸�
        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();
        // �Ӹ� ���� �׸�
        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        // ��ź, ���� ������ true ����
        bombDelay = true;
        tearDelay = true;
    }

    private void Update()
    {
        // ���� ������ �� ���Ͱ�
        Vector3 dir = new Vector3(dir1.x * speed * Time.deltaTime, dir1.y * speed * Time.deltaTime, 0f);
        transform.position += dir;
        waitTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // ���� ������ 
        if(isAutoTear == true) // 1�� Ȯ���� true �϶� 
        {
            if (tearDelay) // 2�� Ȯ�α��� true�� �Ǹ�
            {
                StartCoroutine(TearShootCoroutine()); // �ڷ�ƾ ����
            }
        }

        // ��ź ������
        if(isAutoBomb == true) // 1��Ȯ���� true�϶�
        {
            if(bombDelay) // 2�� Ȯ�α��� true�� �Ǹ�
            {
                StartCoroutine(BombSpawnDelay()); // �ڷ�ƾ ����
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
            Health--;
            Debug.Log("���� �浹/ ���� ü�� : " + Health);
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            bodyAni.SetBool("isGetItem", true);
            head.gameObject.SetActive(false);
            // �����ð� �ڿ� ����Ʈ ������ �����
            /*if (waitTime * Time.deltaTime >= 2)
            {
                bodyAni.SetBool("isGetItem", false);
                head.gameObject.SetActive(true);
            }*/
        }
    }
    
    private void OnMove(InputAction.CallbackContext context) // ���� ������
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

    private void OnFire(InputAction.CallbackContext context) // ���� �߻�
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

    private void SetBomb(InputAction.CallbackContext context) // ��ź ������
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

    private void OnActiveItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
        else if (context.canceled)
        {

        }
    }

    IEnumerator BombSpawnDelay() // ��ź ������ �ڷ�ƾ
    {
        GameObject bomb = Instantiate(Bomb);

        bomb.transform.position = body.transform.position;

        bombDelay = false;
        yield return new WaitForSeconds(bombSpawn);
        bombDelay = true;
    }

    IEnumerator TearShootCoroutine() // ���� �߻� �ڷ�ƾ
    {
        GameObject tears = Instantiate(Tears);

        Transform tearspawn = transform.GetChild(0);

        tears.transform.position = tearspawn.position;

        Bullet tearComponent = tears.GetComponent<Bullet>();

        tearComponent.damage = damage;

        tearComponent.dir = dir2;

        tearDelay = false;

        yield return new WaitForSeconds(tearsSpeed);

        tearDelay = true;
    }
}

