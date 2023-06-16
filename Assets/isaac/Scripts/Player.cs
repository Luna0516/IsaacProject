using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// InputAction ¿¬°á
    /// </summary>
    PlayerAction playerAction;
    /// <summary>
    /// body ¹éÅÍ
    /// </summary>
    Vector2 dir1 = Vector2.zero;
    /// <summary>
    /// head º¤ÅÍ
    /// </summary>
    Vector2 dir2 = Vector2.zero;
    /// <summary>
    /// ´«¹°
    /// </summary>
    public GameObject Tears;
    /// <summary>
    /// ÆøÅº
    /// </summary>
    public GameObject Bomb;
    /// <summary>
    /// ÀÌµ¿ ¼Óµµ
    /// </summary>
    public float speed = 1.0f;
    /// <summary>
    /// ´«¹° °ø°Ý ¼Óµµ
    /// </summary>
    public float tearsSpeed = 2.73f;
    /// <summary>
    /// »ç°Å¸®
    /// </summary>
    public float range = 6.5f;
    /// <summary>
    /// ÃÖ´ë Ã¼·Â
    /// </summary>
    public float maxHealth = 6.0f;

    public float Health;

    private bool tearDelay;

    private bool isAutoClick;

    Transform body;

    Transform head;

    Animator headAni;

    Animator bodyAni;

    SpriteRenderer bodySR;

    private void Awake()
    {
        playerAction = new PlayerAction();

        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();

        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();

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
            if (tearDelay) //true ÀÏ¶§
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
        Debug.Log(value);

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
        Debug.Log("ÆøÅº");
        GameObject bomb = Instantiate(Bomb);
        bomb.transform.position = body.transform.position;
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
}

