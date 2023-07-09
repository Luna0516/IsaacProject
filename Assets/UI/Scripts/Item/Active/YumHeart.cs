using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumHeart : ItemBase1
{
    float attack = 0;
    float speed = 0;
    float attackSpeed = 0;
    ItemType item = ItemType.Active;
    const string itemName = "Yum Heart";
    Sprite icon;
    const int itemNum = 45;
    GradeType grade = GradeType.ItemGrade_1;
    bool usable = true;
    bool stackable = true;
    int maxStackSize = 4;

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

    protected override void Active() {
        if(StackSize == MaxStackSize) {
            StackSize -= MaxStackSize;
            Effect();
        }
    }

    void Effect() {
        Player player = GameManager.Inst.Player;
        player.health += 2;
    }
}
