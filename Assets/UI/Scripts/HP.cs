using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    // UI에 나타낼 프리펩 저장
    public GameObject heartFull;
    public GameObject heartHalf;
    public GameObject heartVoid;
    public GameObject soulHeartFull;
    public GameObject soulHeartHalf;

    Player player = null;

    void Start() {
        player = GameManager.Inst.Player;

        if(player == null)
        {
            Debug.LogWarning("Player Null");
        }

        player.onHealthChange += UpdateHP;

        UpdateHP();
    }

    /// <summary>
    /// 플레이어의 HP의 변경에 따라 UI를 변경하는 함수
    /// </summary>
    public void UpdateHP() {
        int count = transform.childCount;
        
        for (int i = 0; i < count; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < (player.Health / 2); i++) {
            Instantiate(heartFull, transform);
        }
        for (int i = 0; i < (player.Health % 2); i++) {
            Instantiate(heartHalf, transform);
        }
        for (int i = 0; i < ((player.maxHealth - player.Health) / 2); i++) {
            Instantiate(heartVoid, transform);
        }

        if (player.SoulHealth > 0)
        {
            for (int i = 0; i < player.SoulHealth / 2; i++)
            {
                Instantiate(soulHeartFull, transform);
            }
            for (int i = 0; i < player.SoulHealth % 2; i++)
            {
                Instantiate(soulHeartHalf, transform);
            }
        }
    }
}
