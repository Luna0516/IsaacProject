using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meatshit : MonoBehaviour
{

   //��굢�̰� �̵��ϴ� �ڵ带 ¥���Ѵ�.
    Vector2 moveDir;
    public float movespeed=0.5f;
    public float MeatLifeTime=10f;

    private void Start()
    {
        float x = Random.Range(-1f, 1.1f);
        float y= Random.Range(-1f, 1.1f);
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
        Destroy(this.gameObject);
    }
}
