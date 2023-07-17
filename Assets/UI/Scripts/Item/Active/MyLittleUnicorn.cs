using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLittleUnicorn : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("My Little Unicorn", 77,
            0, 1.0f, 0.28f, 0, 0, 0,
            sprite, ItemGrade.Grade_1,
            4);
    }
}

/*
 6sec / speed +0.28
 20damage * 2

 learned line : Temporary badass
 */