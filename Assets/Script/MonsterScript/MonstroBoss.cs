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
    IEnumerator JumpgageCharge()
    {
        while (true)
        {
            //transform.position += new Vector3(0, superJumper, 0) * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, superJumper, transform.position.z), 0.7f);
            yield return null;
        }
    }
    IEnumerator JumpgageDisCharge()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -superJumper, transform.position.z), 0.7f);
            yield return null;
        }
    }
    IEnumerator jumping()
    {
        for (int i = 0; i < 03; i++)
        {
            speed = 0; 
            animator.SetInteger("Jump", 1);
            yield return new WaitForSeconds(0.5f);
            speed = sppeed;
            StartCoroutine(JumpgageCharge());
            yield return new WaitForSeconds(0.8f);
            StopCoroutine(JumpgageCharge());
            StartCoroutine(JumpgageDisCharge());
            yield return new WaitForSeconds(0.950f);
            speed = 0;
            StopCoroutine(JumpgageDisCharge());
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
}
