using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Brimstone", 153,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }
}

/*
 multiTearSpeed : x0.42

 learned line : Quad shot
 */