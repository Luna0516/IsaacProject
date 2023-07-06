using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulHeart : MonoBehaviour
{
    public int SoulHp;

    Player player;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player = collision.gameObject.GetComponent<Player>();
            if (player == null) {
                return;
            }
            /*
            if (player.soulhealth < player.maxSoulhealth) {
                player.soulhealth += SoulHp;
                Destroy(this.gameObject);
            }*/
        }
    }
}
