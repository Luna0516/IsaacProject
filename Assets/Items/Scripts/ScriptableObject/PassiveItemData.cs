using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Item Data", menuName = "Scriptable Object/Passive ItemData", order = 2)]
public class PassiveItemData : ItemData
{
    /// <summary>
    /// 아이템 번호
    /// </summary>
    public int itemNum;

    /// <summary>
    /// UI에서 사용할 아이템 아이콘
    /// </summary>
    public Sprite icon;

    /// <summary>
    /// 아이템 획득 대사
    /// </summary>
    public string explain;

    /// <summary>
    /// 아이템 등급
    /// </summary>
    public ItemGrade grade;

    /// <summary>
    /// 아이템 획득시 증가할 공격력
    /// </summary>
    public float damage;
    /// <summary>
    /// 아이템 획득시 증가할 이동속도
    /// </summary>
    public float speed;
    /// <summary>
    /// 아이템 획득시 증가할 공격 속도
    /// </summary>
    public float tearSpeed;
    /// <summary>
    /// 아이템 획득시 증가할 눈물 이동속도
    /// </summary>
    public float shotSpeed;
    /// <summary>
    /// 아이템 획득시 증가할 눈물 공격 범위
    /// </summary>
    public float range;

    /// <summary>
    /// 아이템 획득시 증가할 눈물 공격 배수
    /// </summary>
    public float multiDamage = 1.0f;
    /// <summary>
    /// 아이템 획득시 증가할 공격 속도 배수
    /// </summary>
    public float multiTearSpeed = 1.0f;

    /// <summary>
    /// 아이템 획득후 회복 량
    /// </summary>
    public int heal;

    /// <summary>
    /// 아이템 획득 후 최대 체력 증가량
    /// </summary>
    public int maxHP = 0;

    /// <summary>
    /// 아이템 획득후 빨간하트 체력 최대치 회복 여부
    /// </summary>
    public bool redFullHeal = false;
}
