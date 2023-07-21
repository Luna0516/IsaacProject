using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float MonsterDamage=1;
    GameManager Manager;
    Player player=null;
    public Transform target;
    public float speed = 5f;
    public float MaxHP = 5;
    protected float damage;
    GameObject spawneffect;
    GameObject meat;
    GameObject blood;  
    /// <summary>
    /// 체력값을 정의하는 프로퍼티
    /// </summary>
    public float HP
    {
       get => MaxHP;
       protected set
        {
            if (MaxHP != value)
            {
                MaxHP = value;

                if (MaxHP <= 0)
                { 
                    MaxHP = 0;
                    Die();
                    //MaxHP가 -가 나와버리면 그냥 0으로 지정하고 해당 개체를 죽이는 함수 실행
                }
            }
        }
    }

    protected virtual void Awake()
    {
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
        meat = Manager.meatObject;
        blood = Manager.bloodObject;//bloodpack에 비어있는 gameobject를 할당해 놓았다. 그걸 blood에 넣어 초기화시킨다.
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
        }
    }

    protected virtual void OnEnable()
    {
        spawneffect.SetActive(true);
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
    void bloodshatter()//피를 흩뿌리는 함수
    {
        int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Instantiate(blood, bloodpos, Quaternion.identity);//bloodshit이라는 게임 오브젝트 생성 종류는 빈 게임 오브젝트, 위치는 bloodpos, 각도는 기존 각도
        }
    }
    void meatshatter()//고기를 흩뿌리는 함수
    {
        int meatCount = UnityEngine.Random.Range(3, 6);
        for (int i = 0; i < meatCount; i++)
        {
            GameObject meatshit = Instantiate(meat, transform.position, Quaternion.identity);
        }

    }
    protected virtual void Hitten()
    {
        HP -= damage;
        Debug.Log($"{gameObject.name}이 {damage}만큼 공격받았다. 남은 체력: {HP}");
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
