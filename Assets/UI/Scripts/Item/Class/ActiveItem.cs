using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item
{
    //
    public int CoolTime { get; protected set; }
    
    //
    public ActiveItem(string name, int itemNum, float attack, float multiDmg, float speed, float tearSpeed, float shotSpeed,  float range, Sprite icon, ItemGrade grade, int coolTime)
    : base(name, itemNum, attack, multiDmg, speed, tearSpeed, shotSpeed, range, icon, grade) {    
        CoolTime = coolTime;   
    }
}
