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
 ��� �� ĳ���Ͱ� �������� �̹���ó�� ���� ǥ���� ������, ���� �� �ش� �������� �������� �߻��Ѵ�.
    �������� �ʰ� �����ϸ� ĳ������ ���� ������� ���ƿ���, �� ���� �������� �Ҹ���� �ʴ´�.
    �������� ƽ�� ĳ������ ���� ���ݷ��� 2���� ���ظ� ������, �� 12�� Ÿ���Ѵ�.

 learned line : BLLLARRRRGGG!
 */