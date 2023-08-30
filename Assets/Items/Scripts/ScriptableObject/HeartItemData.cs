using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heart Item Data", menuName = "Scriptable Object/Heart ItemData", order = 3)]
public class HeartItemData : ItemData, IHealth {
    /// <summary>
    /// 기본 적용 체력
    /// </summary>
    public int redHeart;
    /// <summary>
    /// 소울 하트 체력
    /// </summary>
    public int soulHeart;
    /// <summary>
    /// 블랙 하트 체력
    /// </summary>
    public int blackHeart;

    public bool Heal(GameObject target) {
        bool result = false;
        
        Player player = target.GetComponent<Player>();
        
        if (player != null) {
            if(redHeart >0) {
                if (player.Health < player.maxHealth) {
                    player.Health += redHeart;
                    result = true;
                }
            }

            if(soulHeart > 0) {
                player.SoulHealth += soulHeart;
                result = true;
            }
        }

        return result;
    }
}
