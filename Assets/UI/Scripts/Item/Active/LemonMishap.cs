using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonMishap : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("Lemon Mishap", 56,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_0,
            2);
    }
}

/*
 사용 시 캐릭터 주위에 노란 장판을 깐다. 장판을 밟는 적은 틱당 8, 초당 24의 대미지를 입는다.

 learned line : Oops 
 */