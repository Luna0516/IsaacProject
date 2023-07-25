using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePinkingShears : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("The Pinking Shears", 107,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_2,
            6);
    }
}

/*
 사용 시 그 방에서 몸과 머리가 분리된다. 플레이어는 머리를 조종하며, 머리는 기존의 캐릭터에 비행 능력만 추가된 형태이다.
    몸은 적에게 빠르게 달라붙으며 틱당 5.5, 초당 23.6의 대미지를 입힌다.
    몸은 적이 깐 장판을 피하려는 성질이 있다.
    만약 캐릭터가 몸이 없는 외형이라면 몸 대신 영혼이 생성된다.
    만약 캐릭터가 비행 능력이 있었다면 몸 또한 비행 능력을 가진다.

 learned line : Cut and run
 */