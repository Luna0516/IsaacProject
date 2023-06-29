using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : ItemBase
{
    float attack = 1.0f;
    float speed = 0.3f;
    float attackSpeed = 0.2f;
    // float range = 1.5f;
    // maxHealth + 1; Health +1;
    ItemType item = ItemType.Passive;
    const string itemName = "The Halo";
    Sprite icon;
    const int itemNum = 7;
    GradeType grade = GradeType.ItemGrade_2;
    bool usable = false;
    bool stackable = false;
    int maxStackSize = -1;

    protected override void Awake() {
        icon = GetComponent<SpriteRenderer>().sprite;
        base.Awake();
    }

    protected override void Init() {
        Attack = attack;
        Speed = speed;
        AttackSpeed = attackSpeed;
        Item = item;
        Name = itemName;
        Icon = icon;
        ItemNum = itemNum;
        Grade = grade;
        Usable = usable;
        Stackable = stackable;
        MaxStackSize = maxStackSize;
        StackSize = MaxStackSize;
    }
}
