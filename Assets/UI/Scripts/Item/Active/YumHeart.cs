using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YumHeart : ItemBase {

    public ActiveItem yumHeart;

    protected override void Awake() {
        base.Awake();
        yumHeart = new("Yum Heart", 1,
            0, 0, 0, 0.7f, 0, 0,
            sprite, ItemGrade.Grade_3,
            4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        // getItem?.Invoke(yumHeart);

        Destroy(this.gameObject);
    }
}

/*
 RedHeart +1 heal

 learned line : Reusable regeneration 
 */