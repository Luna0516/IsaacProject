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
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
