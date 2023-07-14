using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steven : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Steven", 50,
            1.0f, 1, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(passiveItem);

        Destroy(this.gameObject);
    }
}

/*
 attack : +1

 learned line : DMG up
 */