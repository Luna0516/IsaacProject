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


    public float superJumper=1f;

    GameObject originalMonstro;
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
        originalMonstro = transform.GetChild(0).gameObject;
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
            //점프 대기
            yield return new WaitForSeconds(0.5f);
            speed = sppeed;
            //점프
            yield return new WaitForSeconds(1.750f);
            speed = 0;
            //점프 종료
            animator.SetInteger("Jump", 0);
            //Idel 실행으로 점프 쿨타임
            yield return new WaitForSeconds(0.5f);
            speed = sppeed;
        }
        selectpattern();
        StopCoroutine(jumping());
    }
    IEnumerator superJump()
    {
        for (int i = 0; i < 03; i++)
        {
            speed = 0;
            animator.SetInteger("SuperJump", 1);
            //점프 대기
            yield return new WaitForSeconds(0.5f);
            speed = sppeed;
            //점프
            yield return new WaitForSeconds(1.750f);
            speed = 0;
            //점프 종료
            animator.SetInteger("SuperJump", 0);
            //Idel 실행으로 점프 쿨타임
            yield return new WaitForSeconds(1.167f);
            speed = sppeed;
        }
        selectpattern();
        StopCoroutine(superJump());
    }

    IEnumerator Attack()
    {
        animator.SetInteger("Attack", 1);
        speed = 0;
        yield return new WaitForSeconds(2.333f);
        speed = sppeed;
        animator.SetInteger("Attack", 0);
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
        transform.position += Time.deltaTime * speed * new Vector3(HeadTo.x,HeadTo.y,0);

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
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }



}
