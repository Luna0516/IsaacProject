using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarOfFlies : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("Jar Of Flies", 434,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_2,
            -1);
    }
}

/*
 적을 처치할 때마다 파리가 최대 20마리까지 쌓이며 사용 씨 쌓인 아군 파리들을 전부 소환한다.
    일부 패밀리어의 공격으로 적을 죽이면 파리가 쌓이지 않는데, 그 목록은 아이작의 번제: 리버스 위키의 해당 항목을 볼 것.

 learned line : Bug catcher
 */