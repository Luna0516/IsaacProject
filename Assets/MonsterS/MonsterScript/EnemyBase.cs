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
        Manager = GetComponent<GameManager>();
        target = Manager.Player.transform;
        bloodpack = FindObjectOfType<Bloodshit>();
    }
    private void Start()
    {
        sprites = new Sprite[bloodpack.sprites.Length];
        for (int i = 0; i < bloodpack.sprites.Length; i++)
        {
            sprites[i] = bloodpack.sprites[i];
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
        int bloodCount = UnityEngine.Random.Range(1, 3);
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
