using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodhelth : MonoBehaviour
{
    Color clo;
    SpriteRenderer spritealpha;
    public float lifeTime;
    float thislifeTime;

    private void Start()
    {
        spritealpha = GetComponent<SpriteRenderer>();
        lifeTime = Random.Range(15f, 50f);
        clo = spritealpha.color;
        thislifeTime = lifeTime;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        float guage = Mathf.Lerp(0f, 1f, lifeTime / thislifeTime);
        clo.a = guage;
        spritealpha.color = clo;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
