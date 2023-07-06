using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;

    public GameObject[] items;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.Key <= 0)
                return;

            player.Key -= 1;
            anim.SetTrigger("Open");
            GameObject item = items[Random.Range(0, items.Length)];
            Instantiate(item);
            item.transform.position = transform.position;

        }
    }
}
