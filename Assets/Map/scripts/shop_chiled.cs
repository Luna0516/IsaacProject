using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_chiled : MonoBehaviour
{
	public int prises = 0;
	Player player;
	SpriteRenderer spriteRenderer;
	Color alpha = Color.white;
	shop mainshop;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		mainshop = GetComponentInParent	<shop>();
    }
    private void Start()
	{
		player = GameManager.Inst.Player;
		alpha.a = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")&&player.Coin>=prises)
		{
			player.Coin = player.Coin-prises;
			spriteRenderer.color = alpha;
            mainshop.Purchased = true;
            prises = 99999;
        }
	}


}
