using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static MonstroBoss;

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
    public MonstroPaterns monstropatern = MonstroPaterns.Idel;


    void selectpattern()
    {
        randomPatt = Random.Range(0, 3);

        switch (monstropatern) 
        {
            case MonstroPaterns.Idel:
                StartCoroutine(IDel());
                break;
            case MonstroPaterns.Jump:
                StartCoroutine(jumping());
                break;
            case MonstroPaterns.Attack:
                StartCoroutine(Attack());
                break;
            case MonstroPaterns.superjump:
                StartCoroutine(superJump());
                break;
            default:
                StartCoroutine(IDel());
                break;
        }
       
    }
    protected override void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        sppeed = speed;

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
            speed = 0; 
            animator.SetInteger("Jump", 1);
            yield return new WaitForSeconds(0.5f);
            superJumper += Time.deltaTime+10;
            speed = sppeed;
            yield return new WaitForSeconds(0.8f);
            superJumper -= Time.deltaTime+10;
            yield return new WaitForSeconds(0.950f);
            speed = 0;
            animator.SetInteger("Jump", 0);
            yield return new WaitForSeconds(1.167f);
            speed = sppeed;
            superJumper = 1;
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
    private void Start()
    {
        selectpattern();
    }
    private void Update()
    {

        Movement();

    }
    protected override void Movement()
    {
        HeadTo = target.position - transform.position;
        HeadTo = HeadTo.normalized;
        transform.position += Time.deltaTime * speed * new Vector3(HeadTo.x,HeadTo.y* superJumper, 0);
        //transform.Translate(Time.deltaTime * speed * HeadTo);
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
