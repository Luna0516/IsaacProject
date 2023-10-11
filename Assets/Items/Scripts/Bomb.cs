using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator anim;
    CircleCollider2D coll;

    WaitForSeconds wait;
    float getTime;

    /// <summary>
    /// 폭탄의 데미지
    /// </summary>
    float damage;
    /// <summary>
    /// 폭탄의 데미지 프로퍼티
    /// </summary>
    public float Damage {
        get => damage;
        set {
            damage = value;
        }
    }

    protected virtual void Awake() {
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();

        // 애니메이션 폭탄이 터지기 전의 시간을 기록
        getTime = anim.GetCurrentAnimatorStateInfo(0).length;
        wait = new WaitForSeconds(getTime);
    }

    protected virtual void Start() {
        StartCoroutine(BombStart());
    }

    /// <summary>
    /// 폭탄이 생성 되었을 때 실행될 코루틴(폭탄 애니메이션 실행 및 삭제)
    /// </summary>
    IEnumerator BombStart() {
        yield return wait;
        anim.SetTrigger("Bomb");
        Explosion();
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    /// <summary>
    /// 폭탄이 터지면서 다른 오브젝트들을 밀어내는 함수
    /// </summary>
    public void Explosion() {
        Collider2D[] coll = Physics2D.OverlapCircleAll(transform.position, 2f);
        
        if(coll != null) {
            foreach (Collider2D one in coll) {
                Vector3 force = (one.transform.position - transform.position).normalized;
                Rigidbody2D targetRigid = one.gameObject.GetComponent<Rigidbody2D>();

                targetRigid.AddForce(force * 5, ForceMode2D.Impulse);
            }
        }
    }

    /// <summary>
    /// 트리거 밖으로 빠져나가면 콜리전 키기
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision) {
        // 콜라이더가 비활성화와 밖으로 빠져나간 오브젝트의 태그가 Player 라면
        if (!coll.enabled && collision.CompareTag("Player")) {
            coll.enabled = true;
        }
    }
}
