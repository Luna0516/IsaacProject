using System.Collections;
using UnityEngine;

public class Meatshit : PooledObject
{

   //고깃덩이가 이동하는 코드를 짜야한다.
    Vector2 moveDir;
    public float movespeed=0f;
    public float MeatLifeTime=10f;
    int random;
    SpriteRenderer rend;
    Factory factory;

    private void Awake()
    {
        factory = Factory.Inst;
        rend = GetComponentInChildren<SpriteRenderer>();
        random = Random.Range(0,factory.MeatSprite.Length);
    }
    private void OnEnable()
    {
        rend.sprite = factory.MeatSprite[random];
        float x = Random.Range(-1f, 1.1f);
        float y = Random.Range(-1f, 1.1f);
        MeatLifeTime = Random.Range(5f, 11f);
        moveDir = new Vector2(x, y);
        StartCoroutine(MoveingMeatSpeed());
        RoomManager.Inst.onChangeRoom += (_) => 
        { 
            StopAllCoroutines();
            this.gameObject.SetActive(false); 
        };
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (RoomManager.Inst != null)
        {
            RoomManager.Inst.onChangeRoom -= (_) =>
            {
                this.gameObject.SetActive(false);
            };
        }
        StopAllCoroutines();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * movespeed * moveDir);
    }

    IEnumerator MoveingMeatSpeed()
    {
        movespeed = 1f;
        yield return new WaitForSeconds(1f);
        movespeed = 0f;
        yield return new WaitForSeconds(MeatLifeTime);
        this.gameObject.SetActive(false);
    }
}
