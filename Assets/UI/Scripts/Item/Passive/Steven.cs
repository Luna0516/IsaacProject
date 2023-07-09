using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steven : ItemBase1
{
    float attack = 1;
    float speed = 0;
    float attackSpeed = -0.4f;
    //float tearSpeed = -0.25f;
    //float attackMag = 2.3f;
    ItemType item = ItemType.Passive;
    const string itemName = "Steven";
    Sprite icon;
    const int itemNum = 50;
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
