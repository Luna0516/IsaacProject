using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOfTheMartyr : ItemBase {

    public PassiveItem bloodOfTheMartyr;

    protected override void Awake() {
        base.Awake();
        bloodOfTheMartyr = new("Blood of the Martyr", 7,
            1.0f, 0, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_3);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(bloodOfTheMartyr);

        Destroy(this.gameObject);
    }
}

/*
 attack : +1

 learned line : DMG up! 
 */