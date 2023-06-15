using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// InputAction 연결
    /// </summary>
    PlayerAction playerAction;

    Vector2 dir1 = Vector2.zero;

    Vector2 dir2 = Vector2.zero
;
    /// <summary>
    /// 눈물
    /// </summary>
    public GameObject Tears;
    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed = 1.0f;
    /// <summary>
    /// 눈물 공격 속도
    /// </summary>
    public float tearsSpeed = 2.73f;
    /// <summary>
    /// 사거리
    /// </summary>
    public float range = 6.5f;
    /// <summary>
    /// 최대 체력
    /// </summary>
    public float maxHealth = 6.0f;

    public float Health;

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
    }
    
    private void Update()
    {
        Vector3 dir = new Vector3(dir1.x * speed * Time.deltaTime, dir1.y * speed * Time.deltaTime, 0f);
        transform.position += dir;
    }

    private void OnEnable()
    {
        playerAction.Move.Enable();
        playerAction.Move.WASD.performed += OnMove;
        playerAction.Move.WASD.canceled += OnMove;
        playerAction.Shot.Enable();
        playerAction.Shot.Cross.performed += OnFire;
        playerAction.Shot.Cross.canceled += OnFire;
    }

    private void OnDisable()
    {
        playerAction.Move.WASD.performed -= OnMove;
        playerAction.Move.WASD.canceled -= OnMove;
        playerAction.Move.Disable();
        playerAction.Shot.Cross.performed -= OnFire;
        playerAction.Shot.Cross.canceled -= OnFire;
        playerAction.Shot.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir1 = value;
        Debug.Log(value);

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
        dir2 = value;
        Debug.Log(value);

        if (dir2.x == 0 && dir2.y == 0)
        {
            headAni.SetBool("isShoot", false);
        }
        else
        {
            headAni.SetBool("isShoot", true);

            headAni.SetFloat("Dir_X2", dir2.x);
            headAni.SetFloat("Dir_Y2", dir2.y);
        }
        
        if (context.performed)
        {
            StartCoroutine(TearShootCoroutine());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    // 연사 방지 매커니즘 만들기 ( 방향키 다다닥 눌러도 눈물이 안나가게 만들기. 눈물 발사 절대값)

    IEnumerator TearShootCoroutine()
    {
        while (true)
        {
            //먼저 Resource에 있는 리소스를 로드를 먼저해야함. ( 1번만 해도댐 Awake or start)
            //그 로드 된 애를 Instantiate 를 해야 함

            GameObject tears = Instantiate(Tears);
            Transform tearspawn = transform.GetChild(0);
            tears.transform.position = tearspawn.position;

            Bullet tearComponent = tears.GetComponent<Bullet>();
            tearComponent.dir = dir2;
            /*tearComponent.SetTearDirection(dir2);*/
            yield return new WaitForSeconds(tearsSpeed);
        }
    }
}
//Instantiate로 만들어진 GameObject 에 방향 정보를 전달을 해야대.
/*

*/