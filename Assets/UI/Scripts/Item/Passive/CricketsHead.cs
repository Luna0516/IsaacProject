using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Brimstone", 4,
            0.5f, 1.5f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(passiveItem);

        Destroy(this.gameObject);
    }
}

/*
 attack : +0.5
 multiDmg : x1.5

 learned line : DMG up
 */