using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSadOnion : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("The Sad Onion", 1,
            0, 1.0f, 0, 0.7f, 0, 0,
            sprite, ItemGrade.Grade_3);
    }
}

/*
 tearsSpeed : +0.7

 learned line : Tears up
 */