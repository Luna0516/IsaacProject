using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Active Item Data", menuName = "Scriptable Object/Active ItemData", order = 1)]
public class ActiveItemData : ItemData {
    /// <summary>
    /// 아이템 번호
    /// </summary>
    public int itemNum;

    /// <summary>
    /// 아이템 획득 대사
    /// </summary>
    public string explain;

    /// <summary>
    /// 아이템 등급
    /// </summary>
    public ItemGrade grade;

    /// <summary>
    /// 아이템 쿨타임
    /// </summary>
    public float coolTime;
}
