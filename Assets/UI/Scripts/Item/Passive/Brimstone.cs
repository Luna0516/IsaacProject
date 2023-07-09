using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : ItemBase1
{
    float attack = 0;
    float speed = 0;
    float attackSpeed = 0.33f;
    ItemType item = ItemType.Passive;
    const string itemName = "Brimstone";
    Sprite icon;
    const int itemNum = 118;
    GradeType grade = GradeType.ItemGrade_4;
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
