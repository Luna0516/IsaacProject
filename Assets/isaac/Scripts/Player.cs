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
    /// InputAction ����
    /// </summary>
    PlayerAction playerAction;

    Vector2 dir1 = Vector2.zero;

    Vector2 dir2 = Vector2.zero
;
    /// <summary>
    /// ����
    /// </summary>
    public GameObject Tears;
    /// <summary>
    /// �̵� �ӵ�
    /// </summary>
    public float speed = 1.0f;
    /// <summary>
    /// ���� ���� �ӵ�
    /// </summary>
    public float tears = 2.73f;
    /// <summary>
    /// ��Ÿ�
    /// </summary>
    public float range = 6.5f;
    /// <summary>
    /// �ִ� ü��
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
        else // Y ���� 0�϶� 
        {
            if (dir1.x < 0) // x�� 0���� ������
            {
                bodySR.flipX = true; // �ø�
                headSR.flipX = true;
                
            }
            else
            {
                bodySR.flipX = false; // �ø��� Ǯ��
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
        
        if (dir2.x < 0) // �������� ��
        {
            headSR.flipX = true; // X�ø�
        }
        else if(dir1.x > 0) // ���� ���������� �����̸�
        {
            headSR.flipX = false; //�Ӱ��� X�ø� ����
        }

        else // ���������� ��
        {
            if(dir2.x > 0) // ���������� ���
            {
                headSR.flipX = false; // x�ø� ���
            }
            else if (dir1.x < 0) // �������� �����̸�
            {
                headSR.flipX = true; // x�ø� Ȱ��ȭ
            }
        }
        headAni.SetFloat(shootDirX, dir2.x);



        //���� ����

        if (context.performed)
        {
            //���� Resource�� �ִ� ���ҽ��� �ε带 �����ؾ���. ( 1���� �ص��� Awake or start)
            //�� �ε� �� �ָ� Instantiate �� �ؾ� ��

            GameObject tears = Instantiate(Tears);
            Transform tearspawn = transform.GetChild(0);
            tears.transform.position = tearspawn.position;

            //Instantiate�� ������� GameObject �� ���� ������ ������ �ؾߴ�.
            Tears tearComponent = tears.GetComponent<Tears>();
            tearComponent.SetTearDirection(dir2);
        }
    }

    //OnFire �� ������ ��
    //���⼭ dir2�� ���� (��������)

    //�� �� �������� ������ ����

    //�� �� �������� ������ �̵�
}