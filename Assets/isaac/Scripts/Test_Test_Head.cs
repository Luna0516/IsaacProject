using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test_Test_Head : MonoBehaviour
{
    Animator ani;

    SpriteRenderer HeadSR;

    Vector2 dir = Vector2.zero;

    readonly int isMove = Animator.StringToHash("isMove");

    readonly int dirX = Animator.StringToHash("Dir_X");

    readonly int dirY = Animator.StringToHash("Dir_Y");

    private void Start()
    {
        ani = GetComponent<Animator>();
        HeadSR = GetComponent<SpriteRenderer>();
        
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
        if (Mathf.Approximately(dir.x, 0) && Mathf.Approximately(dir.y, 0))
        {
            ani.SetBool(isMove, false);
        }
        else
        {
            ani.SetBool(isMove, true);
        }

        ani.SetFloat(dirY, dir.y);
        if (dir.x > 0)
        {
            HeadSR.flipX = true;
        }
        else
        {
            HeadSR.flipX = false;
        }
        ani.SetFloat(dirX, dir.x);

    }
}
