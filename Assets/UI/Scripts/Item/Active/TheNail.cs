using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("The Nail", 83,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_2,
            6);
    }
}

/*
 사용 시 캐릭터가 악마로 변하며 다음의 효과를 얻는다.
    hud black heart 블랙 하트 + 0.5개
    isaac speed 이동 속도 - 0.18
    isaac damage 공격력 + 2
    캐릭터의 눈물이 빨갛게 변하며, 피격 시 굵은 악마 소리를 낸다.
    접촉하는 적에게 초당 20의 피해를 2번 입힌다.
    돌, 똥류 오브젝트를 밟아서 부술 수 있게 된다.

 learned line : Temporary demon form
 */