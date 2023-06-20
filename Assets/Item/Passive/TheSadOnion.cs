using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TheSadOnion : ItemBase {
    float attack = 0;
    float speed = 0.7f;
    float attackSpeed = 0;
    ItemType item = ItemType.Passive;
    const string itemName = "The Sad Onion";
    Sprite icon;
    const int itemNum = 1;
    const string description = "Tears up";
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
        Description = description;
        Grade = grade;
        Usable = usable;
        Stackable = stackable;
        MaxStackSize = maxStackSize;
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
    }
}
