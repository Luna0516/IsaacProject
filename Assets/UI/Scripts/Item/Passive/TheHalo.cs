using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : ItemBase {

    public PassiveItem theHalo;

    protected override void Awake() {
        base.Awake();
        theHalo = new("The Halo", 101, 
            1.0f, 0, 0.3f, 0.2f, 0, 1.5f,
            sprite, ItemGrade.Grade_2);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(theHalo);

        Destroy(this.gameObject);
    }
}

/*
 maxHeart + 1 & RedHeart +1 heal

 attack : +1
 speed : +0.3
 tearsSpeed : +0.2
 range : +1.5

 learned line : All stats up
 */