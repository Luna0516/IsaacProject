using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steven : ItemBase {

    public PassiveItem steven;

    protected override void Awake() {
        base.Awake();
        steven = new("Steven", 50,
            1.0f, 0, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(steven);

        Destroy(this.gameObject);
    }
}

/*
 attack : +1

 learned line : DMG up
 */