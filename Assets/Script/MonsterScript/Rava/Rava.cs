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
        transform.position += Time.deltaTime * speed * targetPosition.normalized;
    }
    private void Start()
    {
        transform.position = transform.position;
        StopAllCoroutines();
        StartCoroutine(moveingRava());
    }

    IEnumerator moveingRava()
    {
        Movement();
        for (int i = 0; i > -1;)
        {
            yield return new WaitForSeconds(1.25f);
            SetNextTargetPosition();
            i++;
        }
    }
    private void Update()
    {

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
