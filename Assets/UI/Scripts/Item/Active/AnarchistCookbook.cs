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
 ��� �� �� ���� ������ ��ġ�� Ʈ�� ��ź 6���� ��ȯ

 learned line : Summon bombs
 */