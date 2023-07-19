using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using static MonstroBoss;

public class MonstroBoss : EnemyBase
{
    public GameObject bulletPrefab;
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

    public Transform turret;

    protected override void Awake()
    {
        base.Awake();
        //�ִϸ����� �ҷ�����
        animator = transform.GetComponentInChildren<Animator>();

        //��������Ʈ ������ �ҷ�����
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();

        turret = transform.GetChild(1);
    }
    protected override void Start()
    {
        base.Start();
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
            Splashbullet();
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
        yield return new WaitForSeconds(0.5f);
        ShatteredBullet();
        yield return new WaitForSeconds(1.5f);

        //�ִϸ��̼� ����
        animator.SetInteger("Attack", 0);
        //speed�� ����
        speed = sppeed;

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
        randomPatt = Random.Range(0, 101);
        //Ȯ�� ������ ����
        int pattern = 0;

        if (randomPatt <10)
        {
            pattern = 0;
        }
        else if (randomPatt <30) 
        {
            pattern = 1;
        }
        else if(randomPatt<70)
        {
            pattern = 2;
        }
        else
        {
            pattern = 3;
        }
        //����ġ�� ������ �ְ� �� ������ �ڷ�ƾ ����
        switch (pattern) 
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
	private void Splashbullet()
	{
		for (int i = 0; i < 10; i++)
		{
			float angle = i * 360f / 10;  // ���� ���
			Quaternion rotation = Quaternion.Euler(0f, 0f, angle);  // ȸ���� ���

			Vector3 spawnPosition = transform.position;  // ���� ��ġ ���
			GameObject bullet = Instantiate(bulletPrefab, spawnPosition, rotation);  // �Ѿ� ����
		}
	}
    void ShatteredBullet()
    {
        if (HeadTo.x < 0)
        { turret.rotation = Quaternion.Euler(0, 0, 90); }
        else
        { turret.rotation = Quaternion.Euler(0, 0, -90); }
        int randomshot = Random.Range(7, 15);
        for (int i = 0;  i < randomshot; i++)
        {
            float Shattering = Random.Range(-45, 46);
            Quaternion shotgack = Quaternion.Euler(0,0, Shattering);
            float randx = Random.Range(-0.5f, 0.6f);
            float randy = Random.Range(-0.5f, 0.6f);
            GameObject bullet = Instantiate(bulletPrefab,new Vector3(turret.transform.position.x+ randx, turret.transform.position.y+ randy,0), turret.rotation*shotgack);
        }
    }
	protected override void Hitten()
    {
        base.Hitten();
        StartCoroutine(damaged(spriteRenderer));
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y - 0.5f, 0), new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y - 0.5f, 0));
        Gizmos.DrawLine(new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y - 0.5f, 0), new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y + 0.5f, 0));
        Gizmos.DrawLine(new Vector3(turret.transform.position.x + 0.5f, turret.transform.position.y + 0.5f, 0), new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y + 0.5f, 0));
        Gizmos.DrawLine(new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y + 0.5f, 0), new Vector3(turret.transform.position.x - 0.5f, turret.transform.position.y - 0.5f, 0));
    }
}
