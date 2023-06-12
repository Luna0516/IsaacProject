using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Head : MonoBehaviour
{
    Animator ani;

    Transform Head;

    PlayerAction playerAction;

    Vector2 dir = Vector2.zero;

    //SpriteRenderer HeadSR;

    readonly int isMove = Animator.StringToHash("isMove");
    readonly int dirX = Animator.StringToHash("Dir_X");
    readonly int dirY = Animator.StringToHash("Dir_Y");

    /*private void Awake()
    {
        playerAction = new PlayerAction();
        ani = GetComponent<Animator>();
        Head = transform.Find("HeadIdle");
    }*/

    private void Start()
    {
        ani = GetComponent<Animator>();

    }

    private void Update()
    {
        UpdateState();
    }
    private void FixedUpdate()
    {
        MoveHead();
    }
    private void MoveHead()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        dir.Normalize();
    }

    private void UpdateState()
    {
        if(Mathf.Approximately(dir.x, 0) && Mathf.Approximately(dir.y, 0))
        {
            ani.SetBool(isMove, false);
        }
        else
        {
            ani.SetBool(isMove, true);
        }
        ani.SetFloat(dirX, dir.x);
        ani.SetFloat(dirY, dir.y);
    }
    /*private void OnEnable()
    {
        playerAction.Shot.Enable();
        playerAction.Shot.Cross.performed += OnFire;
    }


    private void OnDisable()
    {
        playerAction.Shot.Cross.performed -= OnFire;
        playerAction.Shot.Cross.canceled -= OnFire;
        playerAction.Shot.Disable();
    }
    private void OnFire(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir = value;
        Debug.Log(value);
        if(dir.x != 0 || dir.y != 0)
        {
            ani.SetBool(isMove, true);
            
        }
        else
        {
            ani.SetBool(isMove, false);
        }
        ani.SetFloat(dirX, dir.x);
        ani.SetFloat(dirY, dir.y);
    }*/
}
