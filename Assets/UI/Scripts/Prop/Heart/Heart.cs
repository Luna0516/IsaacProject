using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int Hp;

    Player player;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player = collision.gameObject.GetComponent<Player>();
            if(player == null) {
                return;
            }

            if(player.health < player.maxHealth) {
                player.health += Hp;
                Destroy(this.gameObject);
            }
        }
    }
}
