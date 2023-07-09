using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Brimstone", 153,
            0, 1, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(passiveItem);

        Destroy(this.gameObject);
    }
}

/*
 multiTearSpeed : x0.42

 learned line : Quad shot
 */