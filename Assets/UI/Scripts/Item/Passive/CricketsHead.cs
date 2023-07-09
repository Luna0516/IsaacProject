using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : ItemBase {

    public PassiveItem cricketsHead;

    protected override void Awake() {
        base.Awake();
        cricketsHead = new("Brimstone", 4,
            0.5f, 1.5f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(cricketsHead);

        Destroy(this.gameObject);
    }
}

/*
 attack : +0.5
 multiDmg : x1.5

 learned line : DMG up
 */