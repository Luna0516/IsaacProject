using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static PlayerAction;

public class shit : EnemyBase
{
	Rigidbody2D rig;
	Animator animator;
	SpriteRenderer spriteRenderer;
	Vector3 Headto;
	public Color bloodColor = Color.white;
	public float power = 1f;
	public float coolTime = 5;
	float coolTimeCopy;
	public GameObject flyer;
	bool Attacking = false;
	WaitForSeconds waiting;
	float NextStateTime = 1;
	public bool alive = true;

	protected override void Awake()
	{
		base.Awake();
		rig = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		waiting = new WaitForSeconds(NextStateTime);
	}
	private void Start()
	{

		StartCoroutine(attackcool());
	}
	private void Update()
	{
		Headto = (target.position - transform.position).normalized;
		if (Headto.x < 0)
		{
			spriteRenderer.flipX = true;
		}
		else
		{ spriteRenderer.flipX = false; }
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		coolTimeCopy = coolTime;
	}


	void Attack()
	{
		Attacking = true;
		rig.AddForce(Headto * power, ForceMode2D.Impulse);
	}
	private void Stopping()
	{
		Attacking = false;
		rig.velocity = Vector2.zero;
	}
	IEnumerator attackcool()
	{
		while (true)
		{
			if (coolTime > 0)
			{
				coolTime -= Time.deltaTime;
				yield return null;
			}
			else
			{
				animator.SetTrigger("Attack");
				yield return null;
				Attack();
				StartCoroutine(runningShit());
				coolTime = coolTimeCopy;
				NextStateTime = 0.917f;
				yield return waiting;
				Stopping();
				NextStateTime = coolTime;
				yield return waiting;
				coolTime = UnityEngine.Random.Range(3, 5);
			}
		}
	}
	IEnumerator runningShit()
	{
		while (Attacking)
		{
			GameObject shitiything = Factory.Inst.GetObject(PoolObjectType.EnemyShit, transform.position);
			yield return new WaitForSeconds(0.02f);

		}
	}

	protected override void Die()
	{
		alive = false;
		bloodshatter();
		int rand = UnityEngine.Random.Range(2, 5);
		float ranX = UnityEngine.Random.Range(-1, 1.1f);
		float ranY = UnityEngine.Random.Range(-1, 1.1f);

		Vector2 pos = new Vector2(ranX, ranY);
		for (int i = 0; i < rand; i++)
		{
			GameObject fly = Instantiate(flyer, (Vector2)this.transform.position + pos, this.transform.rotation);
		}
		Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
	}

	protected override void bloodshatter()
	{
		int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

		for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
		{
			float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
			float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
			Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
			GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EnemyShit, bloodpos);
		}
	}
	protected override void Hitten()
	{
		base.Hitten();
		StartCoroutine(damaged(spriteRenderer));
	}

}
