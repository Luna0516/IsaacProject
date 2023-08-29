using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Fly : EnemyBase
{
	Vector2 Rnad;
	public float invincivalTime = 1f;
	bool invincival = true;
	Animator animator;
	SpriteRenderer rneder;
	Collider2D coll;


	/// <summary>
	/// 노이즈 무브 정도
	/// </summary>
    [Header("노이즈 무브")]
    public float noise = 5f;


	float X;
	float Y;

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
		cooltimeStart(1 , invincivalTime);
		coll = GetComponent<Collider2D>();
		rneder = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		speed = UnityEngine.Random.Range(0.5f, 2f);
		Rnad = Vector2.zero;
	}
	void noisyMove()
	{
		if (coll.isTrigger==false)
        {
			 X = UnityEngine.Random.Range(-noise, noise+0.1f);
			 Y = UnityEngine.Random.Range(-noise, noise+0.1f);
			Rnad.x = X;
			Rnad.y = Y;	
			this.gameObject.transform.Translate(Time.deltaTime * speed * Rnad);
		}
	}
	protected override void Update()
	{
		if (invincivalTime > 0)
		{
			InvincivalTime = cooltimer1;
		}
		base.Update();
		orderInGame(rneder);

        if (HeadTo.x > 0)
		{
			rneder.flipX = true;
		}
		else
		{
			rneder.flipX = false;
		}
		this.gameObject.transform.Translate(Time.deltaTime * speed * HeadTo);
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
				NuckBack(-HeadTo);
			}
		}
	}
	protected override void Die()
	{
		coll.isTrigger = true;
		animator.SetInteger("Dead", 1);
		Destroy(this.gameObject, 0.917f);
	}
}
