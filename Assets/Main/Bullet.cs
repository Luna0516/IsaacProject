using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 1.0f;
    public float lifeTime = 5.0f;

    Animator anim;
    GameObject tearExplosion;

    public Vector2 dir = Vector2.right;

    protected virtual void Awake() {
        tearExplosion = transform.GetChild(0).gameObject;
    }

    void Update() {
        transform.Translate(Time.deltaTime * speed * dir); // 위, 아래, 양 옆으로 Input에 따라 공격 변경 예정 
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            tearExplosion.transform.SetParent(null);
            tearExplosion.transform.position = collision.contacts[0].point;
            tearExplosion.SetActive(true);
            Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Wall")) {
            tearExplosion.transform.SetParent(null);
            tearExplosion.transform.position = collision.contacts[0].point;
            tearExplosion.SetActive(true);
            Destroy(gameObject);
        }
    }
}
