using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : ItemBase {

    public PassiveItem brimstone;

    protected override void Awake() {
        base.Awake();
        brimstone = new("Brimstone", 118,
            0, 0, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(brimstone);

        Destroy(this.gameObject);
    }
}

/*
 multiTearSpeed : x0.33

 learned line : Blood laser barrage
 */