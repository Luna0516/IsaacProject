using System.Collections;
using UnityEngine;

public class KnifeAttacking : AttackBase
{
    private Vector3 originalPosition; // 투사체 발사 시 초기 위치
    private Vector3 targetPosition;   // 투사체 목표 위치

    protected override void Init()
    {
        base.Init();

        originalPosition = transform.position;
        targetPosition = player.transform.position;
        dir = (targetPosition - originalPosition).normalized;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(MoveAndReturn());
    }

    private IEnumerator MoveAndReturn()
    {
        // 투사체 이동
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = dir * speed * Time.deltaTime;
            yield return null;
        }

        // 플레이어 위치로 회귀
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = dir * speed * Time.deltaTime;
            yield return null;
        }

        TearExplosion();
        gameObject.SetActive(false);
    }
}
