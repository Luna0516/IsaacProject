using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : ItemBase
{
    float attack = 4.0f;
    float speed = 0;
    float attackSpeed = 0.42f;
    float multiDmg = 2.0f;
    ItemType item = ItemType.Passive;
    const string itemName = "Polyphemus";
    Sprite icon;
    const int itemNum = 169;
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
        MultiDmg = multiDmg;
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
