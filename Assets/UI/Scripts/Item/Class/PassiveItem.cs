using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : Item
{
    //
    public PassiveItem(string name, int itemNum, float attack, float multiDmg, float speed, float tearSpeed, float shotSpeed, float range, Sprite icon, ItemGrade grade)
        : base(name, itemNum, attack, multiDmg, speed, tearSpeed, shotSpeed, range, icon, grade) { }
}
