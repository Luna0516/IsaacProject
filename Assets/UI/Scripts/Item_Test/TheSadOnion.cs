using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSadOnion : ItemBase {
    public PassiveItem theSadOnion;

    private void Start() {
        theSadOnion = new("The Sad Onion", 1, 0.0f, 0.0f, 0f, 0.7f, sprite, ItemGrade.Grade_3);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(theSadOnion);

        Destroy(this.gameObject);
    }
}