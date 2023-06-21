using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTear : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rigid;

    public float speed = 5.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        /*Vector2 direction = (Vector2)target.position - rigid.position;
        direction.Normalize();

        rigid.velocity = transform.up * speed;*/
    }




}



