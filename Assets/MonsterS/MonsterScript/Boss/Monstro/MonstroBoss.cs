using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static MonstroBoss;

public class MonstroBoss : EnemyBase
{
    /// <summary>
    /// �ִϸ�����
    /// </summary>
    Animator animator;

    /// <summary>
    /// �ӵ� ����� ����
    /// </summary>
    float sppeed;

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    int randomPatt=0;

    //���� ���� ����
    Vector2 HeadTo;

    /// <summary>
    /// ��������Ʈ ������
    /// </summary>
    SpriteRenderer spriteRenderer;


    protected override void Awake()
    {
        //�ִϸ����� �ҷ�����
        animator = transform.GetComponentInChildren<Animator>();

        //��������Ʈ ������ �ҷ�����
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        //�ӵ� ����� ������ ����
        sppeed = speed;

        //���� ���� �Լ� ����
        selectpattern();
    }
    private void Update()
    { 
        //�̵� �Լ� ����
        Movement();
    }

    //���� ������� ü�� ����(EnemyBase �������̵�)
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    /// <summary>
    /// ������ ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator IDel()
    {       
        //�������� �ʱ� ������ speed�� 0
        speed = 0;

        //�ִϸ��̼� �÷��� Ÿ�Ӹ�ŭ ���(1.167��)
        yield return new WaitForSeconds(1.167f);

        //speed�� sppeed���� ����
        speed = sppeed;

        //���� ���� �Լ� ����
        selectpattern();
    }

    /// <summary>
    /// �������� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator jumping()
    {
        //3�� �ݺ� �ݺ���
        for (int i = 0; i < 03; i++)
        {
            //���� �غ� �ڼ��� ���� speed�� 0
            speed = 0; 

            //�ִϸ����� jump ����
            animator.SetInteger("Jump", 1);

            //���� ��� �ð�(0.5��)�� ���� ����
            yield return new WaitForSeconds(0.5f);
            //���� ���� ���� �ϴ��� ���� ����

            //�÷��̾������� �̵��ϸ鼭 ���ƾ� �ϴ� speed���� �����صξ��� sppeed�� ����
            speed = sppeed;

            //���� Ȱ�� �ð� ���
            yield return new WaitForSeconds(1.750f);
            //�ٴڿ� ������ ������

            //�ٴڿ� ������������ speed���� 0
            speed = 0;

            //���� ����
            animator.SetInteger("Jump", 0);

            //Idel �������� ���� ��Ÿ��
            yield return new WaitForSeconds(0.5f);

            //���� ���¸� ���� speed���� �ٽ� sppeed�� �־ speed ���� �������� �ǵ�����.
            speed = sppeed;
        }
        //�ݺ� ���� ���� ������ ���� ���� ���� �Լ� ����
        selectpattern();
    }
    //���� ���� �ڷ�ƾ ����

/// <summary>
/// �������� ���� ���� �ڷ�ƾ
/// </summary>
/// <returns></returns>
    IEnumerator superJump()
    {
        //�⺻���� ��Ŀ������ ���� ���ϰ� �����ϳ� �ݺ��� ���� �ʴ´�.
            speed = 0;
            animator.SetInteger("SuperJump", 1);
            //���� ���
            yield return new WaitForSeconds(0.5f);
            speed = sppeed;
            //����
            yield return new WaitForSeconds(0.8f);
        //Ȱ�� ���߿� �÷��̾� �Ӹ� ���� �������� �ֵ��� ��ġ���� Ÿ�갪���� ����
            transform.position = target.position;
            yield return new WaitForSeconds(0.950f);
            speed = 0;
            //���� ����
            animator.SetInteger("SuperJump", 0);
            //Idel �������� ���� ��Ÿ��
            yield return new WaitForSeconds(1.167f);
            speed = sppeed;       
        selectpattern();
    }

    /// <summary>
    /// �������� ���� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        //���� �ִϸ��̼� ����
        animator.SetInteger("Attack", 1);

        //������ ���� �̵��� �����.
        speed = 0;

        //���� �ϴµ��� ���
        yield return new WaitForSeconds(2.333f);

        //speed�� ����
        speed = sppeed;

        //�ִϸ��̼� ����
        animator.SetInteger("Attack", 0);

        //���� ���� ���� �Լ� ����
        selectpattern();
    }

    /// <summary>
    /// ��Ʈ�� �̵� �Լ�(�������̵�(���̽� ����))
    /// </summary>
    protected override void Movement()
    {
        //���Ⱚ ���ϱ�
        HeadTo = target.position - transform.position;

        //����ȭ
        HeadTo = HeadTo.normalized;

        //�÷��̾ ���� �̵��ϴ� ��
        transform.position += Time.deltaTime * speed * new Vector3(HeadTo.x,HeadTo.y,0);

        //���⿡ ���� ��������Ʈ �������� Flip���� �����ϴ� ���ǹ�
        if (HeadTo.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }


    /// <summary>
    /// ���� ���� ���� �Լ�
    /// </summary>
    void selectpattern()
    { 
        //���� ������ ���� ��� �ڷ�ƾ ����
        StopAllCoroutines();

        //�������� int ������ ������ ����
        randomPatt = Random.Range(0, 4);

        //����ġ�� ������ �ְ� �� ������ �ڷ�ƾ ����
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
       
    }
    protected override void Hitten()
    {
        base.Hitten();
        StartCoroutine(damaged(spriteRenderer));
    }
}