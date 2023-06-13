using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBase : MonoBehaviour
{
    Player player;

    float healHP;

    private void OnEnable() {
        healHP = 1.0f;
        //player = GameManager.Inst.Player.GetComponent<Player>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //if(player.Health == player.maxHealth) {
            //    return;
            //}
            //player.Health += healHP;
        }
    }
}
