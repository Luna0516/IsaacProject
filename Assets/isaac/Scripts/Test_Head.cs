using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Head : MonoBehaviour
{
    Animator ani;

    PlayerAction playerAction;

    Vector2 dir = Vector2.zero;

    SpriteRenderer HeadSR;

    readonly int isShoot = Animator.StringToHash("isShoot");

    readonly int isMove = Animator.StringToHash("isMove");

    readonly int dirX1 = Animator.StringToHash("Dir_X1");

    readonly int dirY1 = Animator.StringToHash("Dir_Y1");

    readonly int dirX2 = Animator.StringToHash("Dir_X2");

    readonly int dirY2 = Animator.StringToHash("Dir_Y2");

    private void Awake()
    {
        playerAction = new PlayerAction();
        ani = GetComponent<Animator>();
        HeadSR = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerAction.Shot.Enable();
        playerAction.Shot.Cross.performed += OnFire;
        playerAction.Shot.Cross.canceled += OnFire;
        playerAction.Move.Enable();
        playerAction.Move.WASD.performed += OnMove;
        playerAction.Move.WASD.canceled += OnMove;
    }


    private void OnDisable()
    {
        playerAction.Shot.Cross.performed -= OnFire;
        playerAction.Shot.Cross.canceled -= OnFire;
        playerAction.Shot.Disable();
        playerAction.Move.WASD.performed -= OnMove;
        playerAction.Move.WASD.canceled -= OnMove;
        playerAction.Move.Enable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir = value;
        Debug.Log(value);

        if(dir.x == 0 && dir.y == 0)
        {
            ani.SetBool(isMove, false);
        }
        else
        {
            ani.SetBool(isMove, true);
        }

        if(dir.y < 0 || dir.y > 0)
        {
            ani.SetFloat(dirY1, dir.y);
        }
        else
        {
            ani.SetFloat(dirY1, 0);
        }
        if(dir.y != 0)
        {
            ani.SetFloat(dirX1, 0);
        }
        ani.SetFloat(dirX1, dir.x);
        if (dir.x < 0)
        {
            HeadSR.flipX = true;
        }
        else if(dir.x > 0)
        {
            HeadSR.flipX = false;
        }
        

    }
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir = value;
        Debug.Log(value);

        if(dir.x == 0 && dir.y == 0)
        {
            ani.SetBool(isShoot, false);
        }
        else
        {
            ani.SetBool(isShoot, true);
        }
        ani.SetFloat(dirY2, dir.y);
        if(dir.x > 0)
        {
            HeadSR.flipX = true;
        }
        else
        {
            HeadSR.flipX = false;
        }
        ani.SetFloat(dirX2, dir.x);

    }

   
}
