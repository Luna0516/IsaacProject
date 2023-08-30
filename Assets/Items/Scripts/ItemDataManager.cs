using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 액티브 아이템
/// </summary>
public enum ActiveItem
{                       // 아이템 고유 번호
    TheBible,           // 33
    TheNecronomicon,    // 35
    YumHeart,           // 45
    ShoopDaWhoop,       // 49
    LemonMishap,        // 56
    AnarchistCookbook,  // 65
    MyLittleUnicorn,    // 77
    TheNail,            // 83
    ThePinkingShears,   // 107
    DeadSeaScrolls,     // 124
    JarOfFlies,         // 434
}

/// <summary>
/// 패시브 아이템
/// </summary>
public enum PassiveItem
{                       // 아이템 고유 번호
    TheSadOnion,        // 1
    CricketsHead,       // 4
    BloodOfTheMartyr,   // 7
    Steven,             // 50
    TheHalo,            // 101
    MomsKnife,          // 114
    Brimstone,          // 118
    MutantSpider,       // 153
    Polyphemus,         // 169
    SacredHeart,        // 182
}

/// <summary>
/// 체력 회복 아이템
/// </summary>
public enum HeartItem
{
    RedHeart_Half,
    RedHeart_Full,
    SoulHeart_Half,
    SoulHeart_Full,
    // BlackHeart_Half,
    // BlackHeart_Full
}

/// <summary>
/// 폭탄 종류
/// </summary>
public enum BombItem
{
    Bomb,
    DoubleBomb
}

/// <summary>
/// 코인 종류
/// </summary>
public enum CoinItem
{
    Penny,
    Nickel,
    Dime
}

public class ItemDataManager : MonoBehaviour
{
    /// <summary>
    /// 액티브 아이템 데이터 종류
    /// </summary>
    public ActiveItemData[] activeItemDatas;

    /// <summary>
    /// 패시브 아이템 데이터 종류
    /// </summary>
    public PassiveItemData[] passiveItemDatas;

    /// <summary>
    /// 체력 아이템 데이터 종류
    /// </summary>
    public HeartItemData[] heartItemDatas;

    /// <summary>
    /// 폭탄 아이템 데이터 종류
    /// </summary>
    public BombItemData[] bombItemDatas;

    /// <summary>
    /// 코인 아이템 데이터 종류
    /// </summary>
    public CoinItemData[] coinItemDatas;

    /// <summary>
    /// 액티브 아이템 데이터 받아오기
    /// </summary>
    public ActiveItemData GetActiveItemData(ActiveItem code)
    {
        return activeItemDatas[(int)code];
    } 

    /// <summary>
    /// 패시브 아이템 데이터 받아오기
    /// </summary>
    public PassiveItemData GetPassiveItemData(PassiveItem code)
    {
        return passiveItemDatas[(int)code];
    }

    /// <summary>
    /// 아이템 데이터 받아오기
    /// </summary>
    public HeartItemData GetHeartItemData(HeartItem code)
    {
        return heartItemDatas[(int)code];
    }

    /// <summary>
    /// 아이템 데이터 받아오기
    /// </summary>
    public BombItemData this[BombItem code] => bombItemDatas[(int)code];

    /// <summary>
    /// 아이템 데이터 받아오기
    /// </summary>
    public CoinItemData this[CoinItem code] => coinItemDatas[(int)code];
}
