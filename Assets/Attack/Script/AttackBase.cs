using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    public float speed = 1.0f;
    public float lifeTime = 5.0f;

    GameObject tearExplosion;
    public void Awake()
    {
        tearExplosion = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tearExplosion.transform.SetParent(null);
        tearExplosion.transform.position = collision.contacts[0].point;
        tearExplosion.transform.Rotate(0, 0, UnityEngine.Random.Range(0.0f, 360.0f));

        if (collision.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }

    
}
