using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item
{
    //
    public int CoolTime { get; private set; }
    
    //
    public ActiveItem(string name, int itemNum, int coolTime, float attack, float multiDmg, float speed, float attackSpeed, Sprite icon, ItemGrade grade) 
    : base(name, itemNum, attack, multiDmg, speed, attackSpeed, icon, grade) {    
        CoolTime = coolTime;   
    }
}
