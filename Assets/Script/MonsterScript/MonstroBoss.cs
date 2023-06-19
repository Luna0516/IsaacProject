using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MonstroBoss : EnemyBase
{
    Animator animator;
    float sppeed;
    float superJumper=1;
    int randomPatt=0;
    Vector2 HeadTo;
    SpriteRenderer spriteRenderer;
    public enum MonstroPaterns
    {
        Idel = 0, Jump, Attack, superjump
    }

    void selectpattern()
    {
        switch (randomPatt) 
        {
            case 0:
                StartCoroutine(IDel());
                break;
            case 1:
                StartCoroutine(jumping());
                break;
            case 2:
                StartCoroutine(Attack());
                break;
            case 3:
                StartCoroutine(superJump());
                break;
            default:
                StartCoroutine(IDel());
                break;
        }
        randomPatt = Random.Range(0, 3);
    }
    protected override void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        sppeed = speed;
        HeadTo = target.position-transform.position;
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        StopAllCoroutines();
    }
    IEnumerator IDel()
    {       
        speed = 0;
        yield return new WaitForSeconds(1.167f);
        speed = sppeed;
        selectpattern();
        StopCoroutine(IDel());
    }
    IEnumerator jumping()
    {
        for (int i = 0; i < 03; i++)
        {
            animator.SetInteger("Jump", 1);
            superJumper = 2;
            yield return new WaitForSeconds(2.250f);
            speed = 0;
            animator.SetInteger("Jump", 0);
            yield return new WaitForSeconds(1.167f);
            speed = sppeed;
        }
        selectpattern();
        StopCoroutine(jumping());
    }
    IEnumerator superJump()
    {
        yield return new WaitForSeconds(2);
    }

    IEnumerator Attack()
    {
        speed = 0;
        yield return new WaitForSeconds(2.333f);
        speed = sppeed;
        selectpattern();
        StopCoroutine(Attack());
    }
    private void Update()
    {
        Movement();
    }
    protected override void Movement()
    {
        transform.position += Time.deltaTime * speed * new Vector3(HeadTo.x,HeadTo.y* superJumper, 0);
        if (HeadTo.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
