using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemBase {

    public PassiveItem mutantSpider;

    protected override void Awake() {
        base.Awake();
        mutantSpider = new("Brimstone", 153,
            0, 1, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(mutantSpider);

        Destroy(this.gameObject);
    }
}

/*
 multiTearSpeed : x0.42

 learned line : Quad shot
 */