using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacredHeart : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Sacred Heart", 182,
            1.0f, 2.3f, 0, -0.4f, -0.25f, 0,
            sprite, ItemGrade.Grade_4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(passiveItem);

        Destroy(this.gameObject);
    }
}

/*
 maxHeart + 1 & RedHeart full heal

 attack : +1
 multiDamage : x2.3
 tearsSpeed : -0.4
 shotspeed : -0.25

 learned line : Homing shot + DMG up
 */