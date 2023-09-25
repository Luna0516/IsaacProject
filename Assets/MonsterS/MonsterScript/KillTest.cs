using UnityEngine;
using UnityEngine.InputSystem;

public class KillTest : TestBase
{
    protected override void Test1(InputAction.CallbackContext context)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 100);
        foreach (var coll in colls)
        {
            EnemyBase enemyBase = coll.GetComponentInChildren<EnemyBase>();
            if (enemyBase == null)
            {
                enemyBase = coll.GetComponentInParent<EnemyBase>();
            }

            if (coll.CompareTag("Enemy"))
            {
                enemyBase.HP -= 100;
            }
        }
    }
}
