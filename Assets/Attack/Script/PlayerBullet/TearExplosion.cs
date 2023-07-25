using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearExplosion : PooledObject
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(LifeOver(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length));
    }
}
