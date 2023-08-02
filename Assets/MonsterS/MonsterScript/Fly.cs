using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : EnemyBase
{
	Vector3 headto;
	Vector2 Rnad;
	float invincivalTime = 1f;
	bool invincival = true;
	Animator animator;
	SpriteRenderer rneder;
	Collider2D coll;

	float InvincivalTime
	{
		get
		{
			return invincivalTime;
		}
		set
		{
			if (invincivalTime != value)
			{
				invincivalTime = value;
				if (invincivalTime < 0)
				{
					invincival = false;
				}
			}
		}
	}

	private void Start()
	{
		coll = GetComponent<Collider2D>();
		rneder = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		speed = Random.Range(0.5f, 2f);
		StartCoroutine(IvincivalFly());
	}
	void noisyMove()
	{
		if (coll.isTrigger==false)
		{
			float X = Random.Range(-10, 10.1f);
			float Y = Random.Range(-10, 10.1f);
			Rnad = new Vector2(X, Y);
			this.gameObject.transform.Translate(Time.deltaTime * speed * Rnad);
		}
	}
	private void Update()
	{
		headto = (target.position - transform.position).normalized;
		if (headto.x > 0)
		{
			rneder.flipX = true;
		}
		else
		{
			rneder.flipX = false;
		}
		this.gameObject.transform.Translate(Time.deltaTime * speed * headto);
		noisyMove();
	}
	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		if (!invincival)
		{
			if (collision.gameObject.CompareTag("PlayerBullet"))
			{
				damage = collision.gameObject.GetComponent<AttackBase>().Damage;
				Hitten();
			}
		}
	}
	protected override void Die()
	{
		coll.isTrigger = true;
		animator.SetInteger("Dead", 1);
		Destroy(this.gameObject, 0.917f);
	}
	IEnumerator IvincivalFly()
	{
		while (invincival)
		{
			InvincivalTime -= Time.deltaTime;
			yield return null;
		}
	}
}
