using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBible : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("The Bible", 33,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_1,
            4);
    }
}

/*
 ��� �� �� �濡���� ���� ȿ���� ��´�.

 learned line : Temporary flight
 */