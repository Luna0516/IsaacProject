using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator anim;
    CircleCollider2D coll;

    private void Awake() {
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            coll.enabled = false;
            anim.SetTrigger("Get");
        }
    }

    /// <summary>
    /// 애니메이션에서 사용할 이벤트 함수
    /// </summary>
    public void Die() {
        Destroy(this.gameObject);
    }
}
