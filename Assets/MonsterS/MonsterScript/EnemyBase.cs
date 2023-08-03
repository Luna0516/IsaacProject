using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float MonsterDamage=1;
    GameManager Manager;
    Player player=null;
    protected Transform target;
    protected Vector2 HeadTo;
    public float speed = 5f;
    public float NuckBackPower = 1f;
    public float MaxHP = 5;
    float hp;
    protected float damage;
    GameObject spawneffect;

    protected Rigidbody2D rig;
    /// <summary>
    /// 체력값을 정의하는 프로퍼티
    /// </summary>
    public float HP
    {
       get => hp;
       protected set
        {
            if (hp != value)
            {
                hp = value;

                if (hp <= 0)
                { 
                    hp = 0;
                    Die();
                    //MaxHP가 -가 나와버리면 그냥 0으로 지정하고 해당 개체를 죽이는 함수 실행
                }
            }
        }
    }


    protected virtual void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        Manager = GameManager.Inst;
        if (player == null)
        {
        player = Manager.Player; // 플레이어를 찾아서 할당
        target = player.transform;
        }
        else
        {
            Debug.LogWarning("플레이어를 찾을수가 없습니다.");
        }
        spawneffect = transform.GetChild(2).gameObject;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
            NuckBack(collision.contacts[0].normal);
        }
    }

    protected virtual void OnEnable()
    {
        HPInitial();
        spawneffect.SetActive(true);
    }

    private void HPInitial()
    {
        hp = MaxHP;
    }

    protected virtual void Movement()
    {

    }
    protected virtual void Die()
    {
        bloodshatter();
        meatshatter();
        Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }

    protected virtual void bloodshatter()//피를 흩뿌리는 함수
    {
        int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EnemyBlood, bloodpos);
			//GameObject bloodshit = Instantiate(blood, bloodpos, Quaternion.identity);//bloodshit이라는 게임 오브젝트 생성 종류는 빈 게임 오브젝트, 위치는 bloodpos, 각도는 기존 각도
		}
    }
    void meatshatter()//고기를 흩뿌리는 함수
    {
        int meatCount = UnityEngine.Random.Range(3, 6);
        for (int i = 0; i < meatCount; i++)
        {
            GameObject meatshit = Factory.Inst.GetObject(PoolObjectType.EnemyMeat);
            meatshit.transform.position = this.transform.position;
			//GameObject meatshit = Instantiate(meat, transform.position, Quaternion.identity);
		}
    }
    protected virtual void Hitten()
    {
        HP -= damage;
        Debug.Log($"{gameObject.name}이 {damage}만큼 공격받았다. 남은 체력: {HP}");      
    }
    protected virtual void Update()
    {
        HeadTo = (this.gameObject.transform.position - target.transform.position).normalized;
    }
    protected void NuckBack(Vector2 HittenHeadTo)
    {
        rig.AddForce(HittenHeadTo* NuckBackPower, ForceMode2D.Impulse);
    }
    protected IEnumerator damaged(SpriteRenderer sprite, SpriteRenderer sprite1)
    {
        sprite.color = Color.red;
        sprite1.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        sprite1.color = Color.white;
    }

    protected IEnumerator damaged(SpriteRenderer sprite)
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
