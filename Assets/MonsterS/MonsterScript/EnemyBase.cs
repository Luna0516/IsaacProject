using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float MonsterDamage=1;
    GameManager Manager;
    public Transform target;
    public float speed = 5f;
    public float MaxHP = 5;
    protected float damage;

    GameObject spawneffect;



    Bloodshit bloodpack;

    GameObject blood;

    GameObject[] bloodcollect;
    Sprite[] sprites;
    SpriteRenderer[] renderers;

    

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
        Manager = FindObjectOfType<GameManager>(); // 게임 매니저를 찾아서 할당
        Player player = FindObjectOfType<Player>(); // 플레이어를 찾아서 할당
        if (player != null)
        {
            target = player.transform;
        }
        bloodpack = FindObjectOfType<Bloodshit>();
        spawneffect = transform.GetChild(2).gameObject;
    }
    protected virtual void Start()
    {
        this.sprites = new Sprite[bloodpack.sprites.Length];//bloodpack.sprites 배열의 길이만큼 enemybase sprites 배열 초기화
            for (int i = 0; i < bloodpack.sprites.Length; i++)
            {
                this.sprites[i] = bloodpack.sprites[i];//bloodpack에 입력해둔 스프라이트들을 Enemy Base 스프라이트 배열에 복사
            }
        blood = bloodpack.bloodObject;//bloodpack에 비어있는 gameobject를 할당해 놓았다. 그걸 blood에 넣어 초기화시킨다.

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
        Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }
    void bloodshatter()//피를 흩뿌리는 함수
    {
        int bloodCount = UnityEngine.Random.Range(1, 4);//피의 갯수 1~3 사이 정수를 만든다.
        bloodcollect = new GameObject[bloodCount];//bloodclollect 라는 게임 오브젝트 배열에 피의 갯수만큼 게임 오브젝트 배열을 초기화한다.
        renderers = new SpriteRenderer[bloodCount];//renderers라는 SpriteRenderer 배열에 피의 갯수만큼 SpriteRenderer배열을 초기화한다.
        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            int randomIndex = UnityEngine.Random.Range(0, sprites.Length);//피의 종류 sprite 배열에서 숫자 하나를 고른다.
            float X = UnityEngine.Random.Range(transform.position.x - 1, transform.position.x + 2);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 1, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Instantiate(blood, bloodpos, Quaternion.identity);//bloodshit이라는 게임 오브젝트 생성 종류는 빈 게임 오브젝트, 위치는 bloodpos, 각도는 기존 각도
            bloodcollect[i] = bloodshit;//bloodcollect 배열에 i째 bloodshit 오브젝트를 저장
            renderers[i] = bloodshit.GetComponent<SpriteRenderer>();//bloodshit의 spriterenderer컴포넌트를 renderer배열의 i번째에 저장
            renderers[i].sprite = sprites[randomIndex];//i번째 spriterenderer의 sprite에 sprites 배열에서 랜덤 모양을 찾아서 넣어준다.
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
