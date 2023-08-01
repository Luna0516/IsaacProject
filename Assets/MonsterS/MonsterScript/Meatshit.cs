using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meatshit : PooledObject
{

   //고깃덩이가 이동하는 코드를 짜야한다.
    Vector2 moveDir;
    public float movespeed=0f;
    public float MeatLifeTime=10f;
    int random;
    SpriteRenderer rend;
    GameManager manager;

    private void Awake()
    {
        manager = GameManager.Inst;
        rend = GetComponentInChildren<SpriteRenderer>();
        random = Random.Range(0,manager.MeatSprite.Length);
    }
    private void OnEnable()
    {
        rend.sprite = manager.MeatSprite[random];
        float x = Random.Range(-1f, 1.1f);
        float y = Random.Range(-1f, 1.1f);
        MeatLifeTime = Random.Range(5f, 11f);
        moveDir = new Vector2(x, y);
        StartCoroutine(MoveingMeatSpeed());
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
