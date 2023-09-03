using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    bool hasItem = true;

    public int itemPrice;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if(hasItem)
            {
                hasItem = false;
                player.Coin -= itemPrice;
            }
        }
    }
}
