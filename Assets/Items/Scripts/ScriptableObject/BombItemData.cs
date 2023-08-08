using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bomb Item Data", menuName = "Scriptable Object/Bomb ItemData", order = 6)]
public class BombItemData : ItemData, IConsumable
{
    /// <summary>
    /// 폭탄의 개수
    /// </summary>
    public int bombValue = 1;

    /// <summary>
    /// 폭탄을 먹으면 바로 플레이어의 폭탄의 개수가 증가하는 함수
    /// </summary>
    /// <param name="target">폭탄을 먹는 오브젝트(플레이어)</param>
    /// <returns>아이템 삭제 여부</returns>
    public bool Consume(GameObject target) {
        bool result = false;

        Player player = target.GetComponent<Player>();
        if (player != null) {
            player.Bomb += bombValue;
            result = true;
        }

        return result;
    }
}
