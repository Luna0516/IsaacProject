using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum ItemGrade {
    Grade_0,
    Grade_1,
    Grade_2,
    Grade_3,
    Grade_4
}

public class Item {
    //
    public string Name { get; private set; }
    public int ItemNum { get; private set; }
    public float Attack { get; private set; }
    public float MultiDmg { get; private set; }
    public float Speed { get; private set; }
    public float AttackSpeed { get; private set; }
    public Sprite Icon { get; private set; }
    public ItemGrade Grade { get; private set; }

    //
    public Item(string name, int itemNum, float attack, float multiDmg, float speed, float atteckSpeed, Sprite icon, ItemGrade grade) {
        Name = name;
        ItemNum = itemNum;
        Attack = attack;
        MultiDmg = multiDmg;
        Speed = speed;
        AttackSpeed = atteckSpeed;
        Icon = icon;
        Grade = grade;
    }
}
