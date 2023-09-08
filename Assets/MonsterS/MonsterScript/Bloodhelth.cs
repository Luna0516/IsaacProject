using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodhelth : PooledObject
{
    Factory factory;
    public Color clo;
    SpriteRenderer spriteRneder;
    int randomindex=0;

    private void Awake()
    {
        factory = Factory.Inst;
        spriteRneder = GetComponent<SpriteRenderer>();   
        clo=spriteRneder.color;
    }
    private void OnEnable()
    {
        randomindex = Random.Range(0, factory.BloodSprite.Length);
        spriteRneder.sprite = factory.BloodSprite[randomindex];
        float guage = Random.Range(0.2f, 1);
        clo.a = guage;
        spriteRneder.color = clo;
    }
}
