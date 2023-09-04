using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item Data", menuName = "Scriptable Object/Key ItemData", order = 5)]
public class KeyItemData : ItemData, IConsumable
{
    /// <summary>
    /// 열쇠의 개수
    /// </summary>
    public int keyValue = 1;

    /// <summary>
    /// 열쇠를 먹으면 바로 플레이어의 열쇠 개수가 증가하는 함수
    /// </summary>
    /// <param name="target">열쇠를 먹는 오브젝트(플레이어)</param>
    /// <returns>아이템 삭제 여부</returns>
    public bool Consume(GameObject target) {
        bool result = false;

        Player player = target.GetComponent<Player>();
        if (player != null) {
            player.Key += keyValue;
            result = true;
        }

        return result;
    }
}
