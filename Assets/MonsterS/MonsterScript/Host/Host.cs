using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : EnemyBase
{
    /// <summary>
    /// ���� ����� �Ѿ� ������
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// �ͷ�
    /// </summary>
    GameObject turret;

    /// <summary>
    /// �ִϸ�����
    /// </summary>
    Animator animator;

    /// <summary>
    /// ��������Ʈ ������
    /// </summary>
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// "����"�̶�� ���ڸ� ����ִ� int��
    /// </summary>
    int animestate;

    /// <summary>
    /// ���� ���� ���� (���� : false�϶� �����Դϴ�.)
    /// </summary>
    bool invincivle=false;

    /// <summary>
    /// Awake �� ������ �� �־��ִ� �۾�
    /// </summary>
    protected override void Awake()
    {
		turret=transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animestate = Animator.StringToHash("Attack");
    }
    /// <summary>
    /// �÷��̾ ���ݹ��� ���� ���Դٰ� ������ Ʈ����
    /// </summary>
    /// <param name="collision">�΋H���� ���</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        ///���ǹ� : collision�� �±װ� Player�� ���
        if (collision.CompareTag("Player"))
        {
            ///���� �۵� �Լ�
            AttackMove();
        }
    }

    /// <summary>
    /// ���� ��ü�� ���ݹ޴� �ݸ���
    /// </summary>
    /// <param name="collision">�΋H�� ��ü</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ///���ǹ� : �΋H�� ��ü�� �±װ� "PlayerBullet"�� ���, ��.��.��. ���� ������ True�� ��� �۵��մϴ�.
        if (collision.gameObject.CompareTag("PlayerBullet")&&invincivle)
        {
            ///collision�� Damage������Ƽ�� �ҷ��ͼ� damage ������ �ְ� Enemy Base Ŭ���� ������ �����մϴ�.
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            ///���� �޴� �Լ� ȣ��
            Hitten();
        }
    }

    /// <summary>
    /// ���� �׼� �Լ�
    /// </summary>
    void AttackMove()
    {
        ///���� �ڷ�ƾ ����
        StartCoroutine(attackCoroutine(invincivle));
    }


/// <summary>
/// ���ݾ׼ǿ� �ڷ�ƾ
/// </summary>
/// <param name="attackmode">�̰��� ���� ���� �������� Ȱ���մϴ�.</param>
/// <returns></returns>
    IEnumerator attackCoroutine(bool attackmode)
    {
        //���ǹ� : attackmode �� invincivle���� ���� �ƴҶ�(�����϶�) ����˴ϴ�.
        if (attackmode != true)
        {
        //���� ���¸� Ǯ�� 
        invincivle = true;
        //�ִϸ����͸� ���� ���·� �����
        animator.SetInteger(animestate, 1);
        //0.8�� ����մϴ�.
        yield return new WaitForSeconds(0.8f);
        //��� �Ŀ� 3���� źȯ�� �߻��ϴ� �ݺ����Դϴ�.
        for (int i = 0; i < 3; i++)
        {
            bulletshotting(invincivle);
			yield return new WaitForSeconds(0.2f);
		}
        //�߻� �Ŀ� ���� ���¸� �����ϰ�
		animator.SetInteger(animestate, 0);
        //�������·� �����մϴ�.
        invincivle = false;
        }
    }
    /// <summary>
    /// �Ѿ� �߻� �Լ�
    /// </summary>
    /// <param name="shotcool">�̰��� invincivle �������� Ȱ���մϴ�.</param>
    void bulletshotting(bool shotcool)
    {
        //������ �ƴҰ�� �Ѿ��� �߻��մϴ�.
        if(shotcool)
        { 
            //�ͷ��� ���� ���� : �ͷ��� �÷��̾ �ܳ��մϴ�.
        turret.transform.rotation = Quaternion.LookRotation(Vector3.forward,target.position-transform.position);
            //�Ѿ� ���������κ��� ���� ������Ʈ�� �����Ͽ� �ͷ��� ��ġ�� �������� �Ѿ��� �������ϴ�.
        GameObject bullet = Instantiate(bulletPrefab, turret.transform.position, turret.transform.rotation);
        }
    }

    /// <summary>
    /// �´� ó�� �Լ�
    /// </summary>
    protected override void Hitten()
    {
        base.Hitten();
        //�¾����� ��������Ʈ �������� ���������� ���մϴ�.
        StartCoroutine(damaged(spriteRenderer));
    }
}
