using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shitblood : PooledObject
{
    GameManager manager;
    public Color clo;
    SpriteRenderer spriteRneder;
    public float lifeTime=4;
    int randomindex = 0;

    private void Awake()
    {
        manager = GameManager.Inst;
        spriteRneder = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        randomindex = Random.Range(0, manager.BloodSprite.Length);
        spriteRneder.sprite = manager.BloodSprite[randomindex];
        float guage = Random.Range(0.2f,1);
        clo.a = guage;
        spriteRneder.color = clo;
    }

    public void disapear()
    {
            lifeTime -= Time.deltaTime;
            float guage = Mathf.Lerp(0f, 1f, lifeTime / 4);
            clo.a = guage;
            spriteRneder.color = clo;
            if (lifeTime < 0)
            {
                this.gameObject.SetActive(false);
            }
        
    }
}
