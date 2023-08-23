using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_chiled : MonoBehaviour
{
	public int prises = 0;
	Player player;

	private void Start()
	{
		player = GameManager.Inst.Player;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")&&player.Coin>prises)
		{
			player.Coin -= prises;
		}
	}


}
