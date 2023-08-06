using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heart Item Data", menuName = "Scriptable Object/Heart ItemData", order = 3)]
public class HeartItemData : ItemData {
    /// <summary>
    /// 기본 적용 체력
    /// </summary>
    public int redHeart;
    /// <summary>
    /// 소울 하트 체력
    /// </summary>
    public int soulHeart;
    /// <summary>
    /// 블랙 하트 체력
    /// </summary>
    public int blackHeart;
}
