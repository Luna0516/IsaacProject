using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBase : MonoBehaviour
{
    TestPlayer player;
    GameObject playerP;
    float healHP;

    private void OnEnable() {
        healHP = 1.0f;
        player = GameManager.Inst.Player;
        Debug.Log(player);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if(player.Health == player.maxHealth) {
                return;
            }
            player.Health += healHP;

            Destroy(gameObject);
        }
    }
}
