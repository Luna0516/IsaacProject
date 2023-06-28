using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator anim;
    float getTime;

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        getTime = anim.GetCurrentAnimatorStateInfo(0).length;
    }

    protected virtual void OnEnable() {
        StartCoroutine(BombStart());
    }

    IEnumerator BombStart() {
        yield return new WaitForSeconds(getTime);
        anim.SetTrigger("Bomb");
        Explosion();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    void Explosion() {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach(Collider2D one in coll) {
            Vector3 force = (one.transform.position - transform.position).normalized;
            Rigidbody2D targetRigid = one.gameObject.GetComponent<Rigidbody2D>();
            
            if(targetRigid == null) {
                return;
            }

            if (one.gameObject.CompareTag("Player")) {
                targetRigid.AddForce(force * 5, ForceMode2D.Impulse);
                StartCoroutine(KnockBack(targetRigid));
            }

            if (one.gameObject.CompareTag("Enemy")) {
                targetRigid.AddForce(force * 5, ForceMode2D.Impulse);
                StartCoroutine(KnockBack(targetRigid));
            }
        }
    }

    IEnumerator KnockBack(Rigidbody2D rigid) {
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector2.zero;
    }
}
