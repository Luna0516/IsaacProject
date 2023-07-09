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
    public string Name { get; protected set; }
    public int ItemNum { get; protected set; }
    public float Attack { get; protected set; }
    public float MultiDmg { get; protected set; }
    public float Speed { get; protected set; }
    public float TearSpeed { get; protected set; }
    public float ShotSpeed { get; protected set; }
    public float Range { get; protected set; }    
    public Sprite Icon { get; protected set; }
    public ItemGrade Grade { get; protected set; }

    //
    public Item(string name, int itemNum, float attack, float multiDmg, float speed, float tearSpeed, float shotSpeed, float range, Sprite icon, ItemGrade grade) {
        Name = name;
        ItemNum = itemNum;
        Attack = attack;
        MultiDmg = multiDmg;
        Speed = speed;
        TearSpeed = tearSpeed;
        ShotSpeed = shotSpeed;
        Range = range;
        Icon = icon;
        Grade = grade;
    }
}
