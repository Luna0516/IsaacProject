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

    Bloodshit bloodpack;

    GameObject[] blood;
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
        if (Manager == null)
        {
            Debug.LogError("GameManager not found."); // 게임 매니저가 없을 경우 오류 메시지 출력
        }

        Player player = FindObjectOfType<Player>(); // 플레이어를 찾아서 할당
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Player not found."); // 플레이어가 없을 경우 오류 메시지 출력
        }

        bloodpack = FindObjectOfType<Bloodshit>(); 
        if (bloodpack == null)
        {
            Debug.LogError("Bloodshit not found.1"); // Bloodshit 오브젝트가 없을 경우 오류 메시지 출력
        }
    }
    private void Start()
    {
        if (bloodpack != null)
        {
            this.sprites = new Sprite[bloodpack.sprites.Length];
            for (int i = 0; i < bloodpack.sprites.Length; i++)
            {
                this.sprites[i] = bloodpack.sprites[i];
            }
        }
        else
        {
            Debug.LogError("Bloodshit not found.2"); // Bloodshit 오브젝트가 없을 경우 오류 메시지 출력
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            damage = collision.gameObject.GetComponent<AttackBase>().Damage;
            Hitten();
        }
    }

    protected virtual void Movement()
    {

    }
    protected virtual void Die()
    {
        int bloodCount = UnityEngine.Random.Range(1, 4);
        for(int i = 0; i < bloodCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, sprites.Length);
        float X = UnityEngine.Random.Range(transform.position.x - 1, transform.position.x + 2);
        float Y = UnityEngine.Random.Range(transform.position.y - 1, transform.position.y);
        Vector3 bloodpos = new Vector3(X, Y, 0);
        GameObject bloodshit = Instantiate(blood[i], bloodpos,Quaternion.identity);
            bloodshit.AddComponent<SpriteRenderer>();
            renderers[i] = blood[i].GetComponent<SpriteRenderer>();
            renderers[i].sprite = sprites[randomIndex];
        }
        Destroy(this.gameObject);
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
