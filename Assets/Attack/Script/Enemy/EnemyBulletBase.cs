using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBase : PooledObject
{
    public float speed = 10.0f;
    public float lifeTime = 5.0f;

    private void OnEnable()
    {

        StartCoroutine(LifeOver(lifeTime));
    }
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            this.gameObject.SetActive(false);
        }
    }

    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}
