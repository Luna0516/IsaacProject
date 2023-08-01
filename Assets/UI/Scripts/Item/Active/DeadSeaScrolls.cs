using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSeaScrolls : ItemBase {

    protected override void Awake() {
        base.Awake();
        activeItem = new("Dead Sea Scrolls", 124,
            0, 1.0f, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_1,
            2);
    }
}

/*
 ��� �� �ϴ� ����� ��Ƽ�� ������ �� �ϳ��� �ߵ��ȴ�. �� �� ��Ƽ�� �������� �̸��� ǥ�õȴ�.

 learned line : 
//Anarchist Cookbook
//Best Friend
//Bob's Rotten Head
//The Book of Belial
//Book of Revelations
//Book of Shadows
//The Book of Sin
//Crack the Sky
//Crystal Ball
//Deck of Cards
//Doctor's Remote
//The Gamekid
//The Hourglass
//Lemon Mishap
//Mom's Bottle of Pills
//Mom's Bra
//Mom's Pad
//Monster Manual
//Monstro's Tooth
//Mr. Boom
//My Little Unicorn
//The Nail
//The Necronomicon
//The Pinking Shears
//Prayer Card
//Shoop Da Whoop!
//Spider Butt
//Tammy's Head
//Telepathy for Dummies
//Teleport!
//We Need to Go Deeper!
//Yum Heart
 */