using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumHeart : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("Yum Heart", 45,
            0, 0, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_1,
            4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        setItem?.Invoke(activeItem);

        Destroy(this.gameObject);
    }
}

/*
 RedHeart +1 heal

 learned line : Reusable regeneration
 */