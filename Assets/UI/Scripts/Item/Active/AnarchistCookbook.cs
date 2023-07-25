using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnarchistCookbook : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("Anarchist Cookbook", 65,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_1,
            3);
    }
}

/*
 사용 시 방 안의 무작위 위치에 트롤 폭탄 6개를 소환

 learned line : Summon bombs
 */