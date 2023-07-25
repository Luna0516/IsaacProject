using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoopDaWhoop : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("Shoop Da Whoop", 49,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_2,
            2);
    }
}

/*
 사용 시 캐릭터가 아이템의 이미지처럼 벙찐 표정을 지으며, 공격 시 해당 방향으로 레이저를 발사한다.
    공격하지 않고 재사용하면 캐릭터의 얼굴이 원래대로 돌아오며, 이 때는 게이지가 소모되지 않는다.
    레이저는 틱당 캐릭터의 현재 공격력의 2배의 피해를 입히며, 총 12번 타격한다.

 learned line : BLLLARRRRGGG!
 */