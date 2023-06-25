using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBase : MonoBehaviour
{
    public float speed = 10.0f;
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
