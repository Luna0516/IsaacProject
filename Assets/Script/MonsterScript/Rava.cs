using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rava : EnemyBase
{
    Vector3 targetPosition;

    protected override void Movement()
    {
        transform.Translate(Time.deltaTime * speed * targetPosition);
    }
    private void Start()
    {
        transform.position = transform.position;
        StopAllCoroutines();
        StartCoroutine(moveingRava());
    }

    IEnumerator moveingRava()
    {
        for (int i = 0; i > -1;)
        {
            Movement();
            yield return new WaitForSeconds(1.25f);
            SetNextTargetPosition();
            i++;
        }
    }
    private void SetNextTargetPosition()
    {
        float x;
        float y;
        x= Random.Range(-2f, 2f);
        y = Random.Range(-2f, 2f);
        if(x>0)
        {
            transform.Rotate(new Vector3(0,0,0));
        }
        else 
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
        targetPosition = new Vector3(x, y, 0);
    }

}
