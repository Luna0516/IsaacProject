using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Brimstone", 169,
            4, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }
}

/*
 attack : (current attack + 4) x 2
 multiTearSpeed : x0.42
 
 learned line : Mega tears
 */