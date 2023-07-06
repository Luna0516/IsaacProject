using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penny : Coin
{
    Animator anim;
    AnimatorClipInfo[] clipInfo;
    public int value;
    float getTime;

    protected override void Start() {
        base.Start();
        anim = GetComponent<Animator>();
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Count += value;
        getTime = clipInfo[0].clip.length;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.gameObject.CompareTag("Player"))) {
            player.Coin += Count;
            anim.SetTrigger("Get");
            Destroy(this.gameObject, getTime);
        }
    }
}
