using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Active,
    Passive,

}

public enum GradeType
{
    ItemGrade_0,
    ItemGrade_1,
    ItemGrade_2,
    ItemGrade_3,
    ItemGrade_4
}

public class ItemBase1 : MonoBehaviour
{
    public float Attack { get; protected set; }

    public float MultiDmg { get; protected set; }

    public float Speed { get; protected set; }

    public float AttackSpeed { get; protected set; }

    public ItemType Item { get; protected set; }

    public string Name { get; protected set; }

    public Sprite Icon { get; protected set; }

    public int ItemNum { get; protected set; }

    public GradeType Grade { get; protected set; }
  
    public bool Usable { get; protected set; }

    public bool Stackable { get; protected set; }

    public int StackSize { get; protected set; }

    public int MaxStackSize { get; protected set; }

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Active() {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.CompareTag("Player")))
            return;

        // �κ��丮 â���� �̵��ϴ� �ڵ�
    }
}