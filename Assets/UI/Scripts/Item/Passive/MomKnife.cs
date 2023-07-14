using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomKnife : ItemBase {
    protected override void Awake() {
        base.Awake();
        passiveItem = new("Brimstone", 4,
            0, 1, 0, 0, 0, 0,
            sprite, ItemGrade.Grade_4);
    }

    protected override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);

        getItem?.Invoke(passiveItem);

        Destroy(this.gameObject);
    }
}

/*
 learned line : Stab stab stab


 획득 시 캐릭터가 전방으로 식칼을 항상 소지하게 되며 공격 시 소지한 식칼을 공격 방향으로 발사해 다시 캐릭터에게 돌아오는 부메랑 형식으로 바뀌게 된다.
 
 소지 중인 식칼은 기본적으로 접촉한 적에게 1초에 20틱, 틱당 (현재 공격력 × 2)의 피해를 준다.
 그리고 이 피해량 공식은 충전 공격으로 발사되었다 돌아오는 식칼의 발사 시 피해량을 제외한, 다른 아이템과의 상호 작용으로 사출되는 모든 식칼에 일괄적으로 적용된다.
 
 공격 버튼을 꾹 누르면 공격을 충전하기 시작한 후 공격 버튼을 떼면 발사한다.
 이 때 충전 속도는 공격 속도에 비례하고, 식칼의 사거리는 사거리 스텟과 충전량에 비례하며,

  날아가는 식칼의 공격력은 충전량에 비례해 최소 (현재 공격력 × 2), 충전 게이지의 1/3이 찼을 때 최대 (현재 공격력 × 6)의 피해를 주며 나머지 2/3의 충전량은 사거리에만 관여하게 된다.
 또한, 식칼이 돌아올 때는 일괄적으로 공격력의 2배의 피해를 입힌다.

 한 번 던진 식칼은 다시 돌아올 때까지 방향을 변경할 수 없고 대각선 공격이 가능하지만 조준이 매우 힘들어 다루기 까다롭다.

 아이작의 사거리가 길수록 식칼이 날아갈 수 있는 최대 거리가 늘어나고 아이작의 공격 속도가 높을수록 짧은 시간의 충전으로 더 멀리까지 나간다.
 따라서 DPS가 높으려면 사거리는 짧고 공격속도는 빨라야 한다.

 이러한 공격적이고 극단적인 특성 덕에 다루기는 아주 까다롭지만 "아이템 하나만으로 낼 수 있는 피해량"으론 식칼이 내로라는 사기 아이템의 수 배는 넘기에 숙련하는 보람이 있는 아이템이다.
 */