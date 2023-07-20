using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : EnemyBase
{
    Vector3 headto;

    private void Update()
    {
        headto = target.position - this.transform.position;
        this.gameObject.transform.Translate(Time.deltaTime*speed*target.transform.position);

    }
}
