using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNecronomicon : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("The Necronomicon", 35,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_1,
            4);
    }
}

/*
 사용 시 그 방의 모든 적에게 40의 피해를 준다.

 learned line : Mass room damage
 */