using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GradeTypes {
    ItemGrade_0,
    ItemGrade_1,
    ItemGrade_2,
    ItemGrade_3,
    ItemGrade_4
}

public class PassiveItem
{
    //
    string _name;
    int _itemNum;
    float _attack;
    float _multiDmg;
    float _speed;
    float _attackSpeed;
    Sprite _icon;
    GradeTypes _grade;

    //
    public string Name {
        get => _name;
        private set {
            _name = value;
        }
    }
    public int ItemNum {
        get => _itemNum;
        private set {
            _itemNum = value;
        }
    }
    public float Attack {
        get => _attack;
        private set {
            _attack = value;
        }
    }
    public float MultiDmg {
        get => _multiDmg;
        private set {
            _multiDmg = value;
        }
    }
    public float Speed {
        get => _speed;
        private set {
            _speed = value;
        }
    }
    public float AttackSpeed {
        get => _attackSpeed;
        private set {
            _attackSpeed = value;
        }
    }
    public Sprite Icon {
        get => _icon;
        private set {
            _icon = value;
        }
    }
    public GradeTypes Grade {
        get => _grade;
        private set {
            _grade = value;
        }
    }


    // passive
    public PassiveItem(string name, int itemNum, float attack, float multiDmg, float speed, float atteckSpeed, Sprite icon, GradeTypes grade) {
        Name = name;
        ItemNum = itemNum;
        Attack = attack;
        MultiDmg = multiDmg;
        Speed = speed;
        AttackSpeed = atteckSpeed;
        Icon = icon;
        Grade = grade;
    }
}
