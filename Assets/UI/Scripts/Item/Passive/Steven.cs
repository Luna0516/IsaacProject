using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steven : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Steven", 50,
            1.0f, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }
}

/*
 attack : +1

 learned line : DMG up
 */