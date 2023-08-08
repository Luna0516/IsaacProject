using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTear : AttackBase
{
    float pulsingSize = 3f;
    EnemyBase enemy;
    bool onehitCount = false;
    float damageCopy;
    float damageCopy1;
    Vector3 scale;
    protected override void Init()
    {
        base.Init();
        damageCopy1 = (Damage + 4) * 2;
        damageCopy = damageCopy1;        
        this.transform.localScale *= pulsingSize;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        onehitCount = true;
        if (collision.gameObject.CompareTag("Enemy") && onehitCount)
        {
            this.Damage = damageCopy1;
            enemy = collision.gameObject.GetComponent<EnemyBase>();
            damageCopy1 = damageCopy1 - enemy.HP;

            scale = Vector3.one* Mathf.Clamp(pulsingSize * (damageCopy1 / damageCopy),1.5f,3f);
            this.transform.localScale = scale;
            if (damageCopy1 <= 0)
            {
                StopAllCoroutines();
                TearDie();
            }
            onehitCount = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopAllCoroutines();
            TearDie();
        }
    }
    protected override void TearExplosion()
    {
        Factory.Inst.GetObject(PoolObjectType.TearExplosion, transform.position, scale);
    }
}
