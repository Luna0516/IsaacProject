using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rava : EnemyBase
{
    Vector3 movedirtection;
    Vector3 targetPosition;
    Vector3 destination;
    Animator anime;
    WaitForSeconds monsterwait;
    
    private void Awake()
    {
        anime = GetComponent<Animator>();
    }

    protected override void Movement()
    {

    }
    private void Start()
    {
        transform.position = transform.position;
        StartCoroutine(moveingRava());
    }

    IEnumerator moveingRava()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Update()
    {

    }
    private void SetNextTargetPosition()
    {
        float x;
        float y;
        x= Random.Range(-1f, 1f);
        y = Random.Range(0f, 2f);

        targetPosition = new Vector3(x, y, 0);
    }

}
