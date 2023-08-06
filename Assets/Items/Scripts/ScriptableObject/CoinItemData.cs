using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Item Data", menuName = "Scriptable Object/Coin ItemData", order = 4)]
public class CoinItemData : ItemData, IConsumable {
    /// <summary>
    /// 코인 값
    /// </summary>
    public int coinValue;

    public void Consume(GameObject target) {
        Player player = target.GetComponent<Player>();
        if(player != null) {
            player.Coin += coinValue;
        }
    }
}
