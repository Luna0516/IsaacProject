using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSadOnion : ItemBase
{
    float attack = 0;
    float speed = 0;
    float attackSpeed = 0.7f;
    ItemType item = ItemType.Passive;
    const string itemName = "The Sad Onion";
    Sprite icon;
    const int itemNum = 1;
    GradeType grade = GradeType.ItemGrade_3;
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
