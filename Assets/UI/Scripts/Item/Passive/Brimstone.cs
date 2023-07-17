using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Brimstone", 118,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }
}

/*
 multiTearSpeed : x0.33

 learned line : Blood laser barrage
 */