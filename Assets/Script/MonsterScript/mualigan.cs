using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mualigan : EnemyBase
{
    Transform target;
    Vector3 Headto;



    protected override void Movement()
    {
        Headto = transform.position - target.position;
        
        
        if(Headto.x>0&&Headto.x>Headto.y)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else
        {

        }
        transform.Translate(target.position*Time.deltaTime*speed);
        
    }




}
