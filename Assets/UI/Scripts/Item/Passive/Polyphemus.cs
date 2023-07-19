using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : ItemBase {
    protected override void Awake() {
        base.Awake();

        passiveItem = new("Polyphemus", 169,
            4, 2.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(passiveItem);

        Destroy(this.gameObject);
    }
}

/*
 attack : (current attack + 4) x 2
 multiTearSpeed : x0.42
 
 learned line : Mega tears
 */