using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinBase : MonoBehaviour
{
    protected int coinCount;

    protected Rigidbody2D rigid;

    protected virtual void OnEnable() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!(collision.gameObject.CompareTag("Player")))
            return;

        Inventory.instance.Coin = this.coinCount;

        this.rigid.simulated = false;

        Destroy(this.gameObject);
    }
}
