using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Start() {
    }

    void OnEnable() {
        Destroy(gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}
