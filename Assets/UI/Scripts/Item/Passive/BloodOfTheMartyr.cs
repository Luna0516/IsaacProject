using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOfTheMartyr : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Blood of the Martyr", 7,
            1.0f, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }
}

/*
 attack : +1

 learned line : DMG up! 
 */