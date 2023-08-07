using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Item Data", menuName = "Scriptable Object/Coin ItemData", order = 4)]
public class CoinItemData : ItemData, IConsumable {
    /// <summary>
    /// 코인 값
    /// </summary>
    public int coinValue;

    /// <summary>
    /// 코인을 먹으면 바로 플레이어의 코인이 증가하는 함수
    /// </summary>
    /// <param name="target">코인을 먹는 오브젝트(플레이어)</param>
    public void Consume(GameObject target) {
        Player player = target.GetComponent<Player>();
        if(player != null) {
            player.Coin += coinValue;
        }
    }
}
