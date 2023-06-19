using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 타입
/// </summary>
public enum ItemType {
    Active,
    Passive,

}

/// <summary>
/// 아이템 등급
/// </summary>
public enum GradeType {
    ItemGrade_0,
    ItemGrade_1,
    ItemGrade_2,
    ItemGrade_3,
    ItemGrade_4
}

public class ItemBase : MonoBehaviour
{
    /// <summary>
    /// 공격력 변화량
    /// </summary>
    public float Attack { get; protected set; }

    /// <summary>
    /// 이동속도 변화량
    /// </summary>
    public float Speed { get; protected set; }

    /// <summary>
    /// 공격속도 변화량
    /// </summary>
    public float AttackSpeed { get; protected set; }

    /// <summary>
    /// 아이템 타입
    /// </summary>
    public ItemType Item { get; protected set; }

    /// <summary>
    /// 아이템 이름
    /// </summary>
    public string Name { get; protected set; }
    
    /// <summary>
    /// 아이템 아이콘
    /// </summary>
    public Sprite Icon { get; protected set; }
    
    /// <summary>
    /// 아이템 번호
    /// </summary>
    public int ItemNum { get; protected set; }
    
    /// <summary>
    /// 아이템 등급
    /// </summary>
    public GradeType Grade { get; protected set; }
    
    /// <summary>
    /// 아이템 사용형
    /// </summary>    
    public bool Usable { get; protected set; }

    /// <summary>
    /// 아이템 스택형
    /// </summary>
    public bool Stackable { get; protected set; }

    /// <summary>
    /// 스택형일때 스택 크기
    /// </summary>
    public int MaxStackSize { get; protected set; }

    protected virtual void Awake() {
        Init();
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    protected virtual void Init() {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!(collision.gameObject.CompareTag("Player")))
            return;

        // 인벤토리 창으로 이동하는 코드
    }
}
