using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// InputAction 연결
    /// </summary>
    PlayerAction playerAction;

    Vector3 dir = Vector3.zero;
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
    
    Animator ani;

    SpriteRenderer spriteRenderer;

    readonly int boolY_string = Animator.StringToHash("BoolY");
    
    readonly int boolX_string = Animator.StringToHash("BoolX");
    private void Awake()
    {
        playerAction = new PlayerAction();
        ani = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position += Time.deltaTime * speed * dir;
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
        playerAction.Shot.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir = value;
        Debug.Log($"{value}");

        if (dir.y != 0)
        {
            ani.SetBool(boolY_string, true);
        }
        else
        {
            ani.SetBool(boolY_string, false);
        }


        ani.SetBool(boolX_string, true);
        if(dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            ani.SetBool(boolX_string, false);
        }



    }
    private void OnFire(InputAction.CallbackContext context)
    {
        GameObject tears = GameObject.Instantiate(Tears);
        Transform tearspawn = transform.GetChild(0);
        Tears.transform.position = tearspawn.position;
    }
    

}
