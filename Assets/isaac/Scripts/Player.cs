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
    public float tears = 2.73f;
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

    SpriteRenderer headSR;

    readonly int moveDirY = Animator.StringToHash("MoveDir_Y");

    readonly int moveDirX = Animator.StringToHash("MoveDir_X");

    readonly int isMove = Animator.StringToHash("isMove");

    readonly int headDirX = Animator.StringToHash("Dir_X1");

    readonly int headDirY = Animator.StringToHash("Dir_Y1");

    readonly int isShoot = Animator.StringToHash("isShoot");

    readonly int shootDirX = Animator.StringToHash("Dir_X2");

    readonly int shootDirY = Animator.StringToHash("Dir_Y2");
    private void Awake()
    {
        playerAction = new PlayerAction();

        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();

        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        headSR = head.GetComponent<SpriteRenderer>();


        


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
            bodyAni.SetBool(isMove, false);
            headAni.SetBool(isMove, false);
        }
        else
        {
            bodyAni.SetBool(isMove, true);
            headAni.SetBool(isMove, true);
        }
        
        bodyAni.SetFloat(moveDirY, dir1.y);
        headAni.SetFloat(headDirY, dir1.y);

        if(dir1.y != 0)
        {
            headSR.flipX = false;
            if(dir1.x < 0)
            {
                bodySR.flipX = true;
            }
            else
            {
                bodySR.flipX = false;
            }
        }
        else // Y 값이 0일때 
        {
            if (dir1.x < 0) // x가 0보다 작을시
            {
                bodySR.flipX = true; // 플립
                headSR.flipX = true;
                
            }
            else
            {
                bodySR.flipX = false; // 플립을 풀어
                headSR.flipX = false;
                
            }
        }
        bodyAni.SetFloat(moveDirX, dir1.x);
        headAni.SetFloat(headDirX, dir1.x);
    }
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir2 = value;
        Debug.Log(value);

        if (dir2.x == 0 && dir2.y == 0)
        {
            headAni.SetBool(isShoot, false);
        }
        else
        {
            headAni.SetBool(isShoot, true);
        }
        headAni.SetFloat(shootDirY, dir2.y);
        
        if (dir2.x < 0) // 왼쪽으로 쏠때
        {
            headSR.flipX = true; // X플립
        }
        else if(dir1.x > 0) // 몸이 오른쪽으로 움직이면
        {
            headSR.flipX = false; //머가리 X플립 해제
        }

        else // 오른쪽으로 쏠떄
        {
            if(dir2.x > 0) // 오른쪽으로 쏘면
            {
                headSR.flipX = false; // x플립 취소
            }
            else if (dir1.x < 0) // 왼쪽으로 움직이면
            {
                headSR.flipX = true; // x플립 활성화
            }
        }
        headAni.SetFloat(shootDirX, dir2.x);



        //눈물 생성

        if (context.performed)
        {
            //먼저 Resource에 있는 리소스를 로드를 먼저해야함. ( 1번만 해도댐 Awake or start)
            //그 로드 된 애를 Instantiate 를 해야 함

            GameObject tears = Instantiate(Tears);
            Transform tearspawn = transform.GetChild(0);
            tears.transform.position = tearspawn.position;

            //Instantiate로 만들어진 GameObject 에 방향 정보를 전달을 해야대.
            Tears tearComponent = tears.GetComponent<Tears>();
            tearComponent.SetTearDirection(dir2);
        }
    }

    //OnFire 가 실행이 되
    //여기서 dir2를 받음 (공격정보)

    //그 쪽 방향으로 눈물을 생성

    //그 쪽 방향으로 눈물이 이동
}