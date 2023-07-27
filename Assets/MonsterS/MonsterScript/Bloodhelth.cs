using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodhelth : PooledObject
{
    GameManager manager;
    public Color clo;
    SpriteRenderer spriteRneder;
    public float lifeTime;
    float thislifeTime;
    int randomindex=0;

    private void Awake()
    {
        manager = GameManager.Inst;
        spriteRneder = GetComponent<SpriteRenderer>();   
        clo=spriteRneder.color;
    }
    private void OnEnable()
    {
        randomindex = Random.Range(0, manager.BloodSprite.Length);
        lifeTime = Random.Range(15f, 50f);
        thislifeTime = lifeTime;
        spriteRneder.sprite = manager.BloodSprite[randomindex];
    }
    protected override void OnDisable()
    {
        clo = Color.white;
        base.OnDisable();
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        float guage = Mathf.Lerp(0f, 1f, lifeTime / thislifeTime);
        clo.a = guage;
        spriteRneder.color = clo;

        if (lifeTime < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
