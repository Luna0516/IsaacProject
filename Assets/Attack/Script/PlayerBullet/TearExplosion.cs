using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearExplosion : PooledObject
{
    /// <summary>
    /// 애니메이션 컴포넌트
    /// </summary>
    Animator anim;      

    private void Awake()
    {
        anim = GetComponent<Animator>();    // 애니메이터 컴포넌트 찾기
    }

    private void OnEnable()
    {
        StopAllCoroutines();    // 모든 코루틴 정지
        
        float animTime = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length; // 애니메이션 재생 시간

        StartCoroutine(Gravity_Life(animTime));     // animTime만큼 재생하고, 오브젝트 비활성화
    }
}
