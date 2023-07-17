using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodhelth : MonoBehaviour
{
    Color clo;
    SpriteRenderer spritealpha;
    float lifeTime;
    float thislifeTime;

    private void Awake()
    {
        spritealpha = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        lifeTime = Random.Range(8f, 15f);
        clo = spritealpha.color;
        thislifeTime = lifeTime;
    }
    private void Update()
    {
        clo.a = Mathf.Lerp(1f,0f, lifeTime/thislifeTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 )
        {
            Destroy( this );
        }
    }

}
