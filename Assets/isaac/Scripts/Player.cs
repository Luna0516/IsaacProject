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

    Vector3 dir1 = Vector2.zero;

    Vector3 dir2 = Vector2.zero;
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

    Transform body;

    Transform head;

    Animator headAni;

    Animator bodyAni;

    SpriteRenderer bodySR;

    SpriteRenderer headSR;

    readonly int boolY_string = Animator.StringToHash("BoolY");

    readonly int boolX_string = Animator.StringToHash("BoolX");

    readonly int headX_string = Animator.StringToHash("HeadX");

    readonly int headY_string = Animator.StringToHash("HeadY");

    /*readonly int shoot_Back = Animator.StringToHash("Up");

    readonly int shoot_Front = Animator.StringToHash("Down");

    readonly int shoot_Right = Animator.StringToHash("Right");

    readonly int shoot_Left = Animator.StringToHash("Left");*/

    readonly int shootX_string = Animator.StringToHash("ShootX");

    readonly int shootY_Back = Animator.StringToHash("ShootY");

    readonly int shootY_Front = Animator.StringToHash("ShootY_Front");
    private void Awake()
    {
        playerAction = new PlayerAction();

        body = transform.Find("bodyIdle");
        bodyAni = body.GetComponent<Animator>();
        bodySR = body.GetComponent<SpriteRenderer>();

        head = transform.Find("HeadIdle");
        headAni = head.GetComponent<Animator>();
        headSR = head.GetComponent<SpriteRenderer>();

        
        GameObject tears = Instantiate(Tears);
        Transform tearspawn = transform.GetChild(0);
        tears.transform.position = tearspawn.position;

    }



    private void Update()
    {
        Vector2 delta = transform.position += Time.deltaTime * speed * dir1;
    }
    private void OnEnable()
    {
        playerAction.Move.Enable();
        playerAction.Move.WASD.performed += OnMove;
        playerAction.Move.WASD.canceled += OnMove;
        playerAction.Shot.Enable();
        playerAction.Shot.Cross.performed += OnFire;
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
        dir1 = value.normalized;
        Debug.Log(value);
        

        if (dir1.y != 0)
        {
            bodyAni.SetBool(boolY_string, true);
            
            if (dir1.y > 0)
            {
                headAni.SetBool(headY_string, true);
                
            }
            else if (dir1.y < 0)
            {
                headAni.SetBool(headY_string, false);
                
            }
        }
        else
        {
            bodyAni.SetBool(boolY_string, false);
            headAni.SetBool(headY_string, false);
            
        }



        headAni.SetBool(headX_string, true);
        bodyAni.SetBool(boolX_string, true);
        if (dir1.x < 0)
        {
            bodySR.flipX = true;
            headSR.flipX = true;
        }
        else if (dir1.x > 0)
        {
            bodySR.flipX = false;
            headSR.flipX = false;
        }
        else
        {
            headAni.SetBool(headX_string, false);
            bodyAni.SetBool(boolX_string, false);
        }

        if (dir1.y != 0)
        {
            headAni.SetBool(headX_string, false);
        }
        else if (dir1.y < 0)
        {
            headAni.SetBool(headX_string, false);
        }


    }
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir2 = value.normalized;
        Debug.Log(value);
        
        
        if(dir2.y < 0)
        {
            headAni.SetBool(shootY_Back, true);
            dir2.y = 0;
        }
        else if (dir2.y > 0)
        {
            headAni.SetBool(shootY_Front, true);
            dir2.y = 0;
        }
        else
        {
            headAni.SetBool(shootY_Back, false);
            headAni.SetBool(shootY_Front, false);
        }

        if(dir2.x <= 0)
        {
            headAni.SetBool(shootX_string, true);
            headSR.flipX = false;
            dir2.x = 0;
        }
        else if (dir2.x >= 0)
        {
            headAni.SetBool(shootX_string, true);
            headSR.flipX = true;
            dir2.x = 0;
        }
        else
        {
            headAni.SetBool(shootX_string, false);
        }





        /*Vector2 value = context.ReadValue<Vector2>().normalized;
        dir = value;
        Debug.Log(value);

        if (dir.y < 0)
        {
            headAni.SetBool(shootY_string, true);
        }
        else if (dir.y < 0)
        {
            headAni.SetBool(shootY_string, false);
            headAni.SetFloat(shootY_float, 1);
        }
        else
        {
            headAni.SetBool(shootY_string, false);
            headAni.SetFloat(shootY_float, 0);
        }
*/


    }
}