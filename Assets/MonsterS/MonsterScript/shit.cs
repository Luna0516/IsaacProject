using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerAction;

public class shit : EnemyBase
{
	/// <summary>
	/// 애니메이터 변수
	/// </summary>
	Animator animator;

	/// <summary>
	/// 스프라이트 렌더러 변수
	/// </summary>
	SpriteRenderer spriteRenderer;

	/// <summary>
	/// 
	/// </summary>
	public float power = 1f;
	public float coolTime = 5;
	public GameObject flyer;

	WaitForSeconds waiting;
	float NextStateTime = 1;
	bool shitDead = false;
	bool Attacking = false;
	float draglinear = 15f;

	public bool ShitDead
	{
		get => shitDead;
		set
		{
			shitDead = value;
		}
	}
	bool AttacActive
	{
		get => Attacking;
		set
		{
			Attacking = value;
			IsAttack?.Invoke(this);
		}
	}
	System.Action<bool> IsDead;
	System.Action<bool> IsAttack;
	protected override void Awake()
	{
        base.Awake();
        draglinear = rig.drag;
        animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		waiting = new WaitForSeconds(NextStateTime);
	}
	private void Start()
	{
        coolTime = Random.Range(3, 5);
		StartCoroutine(CoolTiming());
	}
	protected override void Update()
	{
		base.Update();
		orderInGame(spriteRenderer);

        if (HeadTo.x < 0)
		{
			spriteRenderer.flipX = true;
		}
		else
		{ spriteRenderer.flipX = false; }
		damageoff(spriteRenderer);

    }

	protected override void OnEnable()
	{
		base.OnEnable();
		IsAttack += Attack;
	}
	private void OnDestroy()
	{
		StopAllCoroutines();
		IsAttack -= Attack;
	}

	void Attack(bool Attackchack)
	{
		rig.drag = 1;
        coolTime = Random.Range(3, 5);
		rig.isKinematic = false;
		rig.AddForce(HeadTo * power, ForceMode2D.Impulse);
		if (Attackchack)
		{
			StartCoroutine(attackAction());
			StartCoroutine(runningShit());
		}
	}
	private void Stopping()
	{
		AttacActive = false;
		rig.drag = draglinear;
        rig.isKinematic = true;
		rig.velocity = Vector2.zero;
		StartCoroutine(CoolTiming());
	}
	IEnumerator CoolTiming()
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
				AttacActive = true;
				yield break;
			}
		}
	}
	IEnumerator attackAction()
	{
		while (AttacActive)
		{
			animator.SetTrigger("Attack");
			yield return null;
			NextStateTime = 0.917f;
			yield return waiting;
			Stopping();
		}
	}
	IEnumerator runningShit()
	{
		while (AttacActive)
		{
			GameObject shitiything = Factory.Inst.GetObject(PoolObjectType.EnemyShit, transform.position);
			Shitblood bloodobj = shitiything.GetComponent<Shitblood>();
			IsDead += bloodobj.EnamvleChoosAction;
			IsDead?.Invoke(false);
			IsDead -= bloodobj.EnamvleChoosAction;
			yield return new WaitForSeconds(0.05f);
		}
	}

	protected override void Die()
	{
		bloodshatter();
		int sellect = 0;
		int rand = UnityEngine.Random.Range(0, 101);
		if (rand < 40)
		{
			sellect = 0;
		}
		else if (rand < 60)
		{
			sellect = 3;
		}
		else if (rand < 80)
		{
			sellect = 4;
		}
		else if (rand < 101)
		{
			sellect = 5;
		}

		float ranX = Random.Range(-1, 1.1f);
		float ranY = Random.Range(-1, 1.1f);

		Vector2 pos = new Vector2(ranX, ranY);
		for (int i = 0; i < sellect; i++)
		{
			GameObject fly = Instantiate(flyer, (Vector2)this.transform.position + pos, this.transform.rotation);
		}
		Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
	}

	protected override void bloodshatter()
	{
		for (int i = 0; i < 2; i++)//피의 갯수만큼 반복작업
		{
			float X = Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
			float Y = Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
			GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EnemyShit, new Vector2(X, Y));
			Shitblood bloodobj = bloodshit.GetComponent<Shitblood>();
			IsDead += bloodobj.EnamvleChoosAction;
			IsDead?.Invoke(true);
		}
	}
	protected override void Hitten()
	{
		base.Hitten();
		damaged(spriteRenderer);
	}

}
