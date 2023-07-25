using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : EnemyBase
{
    Vector3 headto;

    private void Update()
    {
        headto = (target.position - transform.position).normalized;
        this.gameObject.transform.Translate(Time.deltaTime*speed* headto);
    }
}
