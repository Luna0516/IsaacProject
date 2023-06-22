using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ Ÿ��
/// </summary>
public enum ItemType
{
    Active,
    Passive,

}

/// <summary>
/// ������ ���
/// </summary>
public enum GradeType
{
    ItemGrade_0,
    ItemGrade_1,
    ItemGrade_2,
    ItemGrade_3,
    ItemGrade_4
}

public class ItemBase : MonoBehaviour
{
    /// <summary>
    /// ���ݷ� ��ȭ��
    /// </summary>
    public float Attack { get; protected set; }

    /// <summary>
    /// �̵��ӵ� ��ȭ��
    /// </summary>
    public float Speed { get; protected set; }

    /// <summary>
    /// ���ݼӵ� ��ȭ��
    /// </summary>
    public float AttackSpeed { get; protected set; }

    /// <summary>
    /// ������ Ÿ��
    /// </summary>
    public ItemType Item { get; protected set; }

    /// <summary>
    /// ������ �̸�
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// ������ ������
    /// </summary>
    public Sprite Icon { get; protected set; }

    /// <summary>
    /// ������ ��ȣ
    /// </summary>
    public int ItemNum { get; protected set; }

    /// <summary>
    /// ������ ���
    /// </summary>
    public GradeType Grade { get; protected set; }

    /// <summary>
    /// ������ �����
    /// </summary>    
    public bool Usable { get; protected set; }

    /// <summary>
    /// ������ ������
    /// </summary>
    public bool Stackable { get; protected set; }

    /// <summary>
    /// �������϶� ���� ũ��
    /// </summary>
    public int MaxStackSize { get; protected set; }

    protected virtual void Awake()
    {
        Init();
    }

    /// <summary>
    /// �ʱ�ȭ �Լ�
    /// </summary>
    protected virtual void Init()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.CompareTag("Player")))
            return;

        // �κ��丮 â���� �̵��ϴ� �ڵ�
    }
}