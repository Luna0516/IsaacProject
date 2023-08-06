using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 등급
/// </summary>
public enum ItemGrade {
    Grade_0,
    Grade_1,
    Grade_2,
    Grade_3,
    Grade_4
}

/// <summary>
/// 아이템 종류
/// </summary>
public enum ItemType {
    Active,
    Passive,
    Accessories,
    Props,
}

public class ItemData : ScriptableObject {
    /// <summary>
    /// 아이템 종류
    /// </summary>
    public ItemType itemType;
    
    /// <summary>
    /// 아이템 이름
    /// </summary>
    public string itemName;

    /// <summary>
    /// UI에서 사용할 아이템 아이콘
    /// </summary>
    public Sprite icon;

    /// <summary>
    /// 씬에서 사용할 아이템 프리펩
    /// </summary>
    public GameObject itemPrefab;
}