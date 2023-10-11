using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeHitChack : MonoBehaviour
{
    KnifeAttacking pa;
    private void Start()
    {
        pa = GetComponentInParent<KnifeAttacking>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("닿음");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("적임");
            EnemyBase enemy = collision.transform.GetComponentInChildren<EnemyBase>();
            enemy.damage = pa.Damage;
            enemy.Hitten();
            Vector2 nuckBackDir = pa.dir;
            enemy.NuckBack(nuckBackDir.normalized);
        }
    }
}
