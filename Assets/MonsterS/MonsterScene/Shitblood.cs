using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Shitblood : PooledObject
{
    GameManager manager;
    public Color clo;
    SpriteRenderer spriteRneder;
    public float lifeTime;

    int randomindex = 0;

    public float speed=1f;

    private void Awake()
    {
        manager = GameManager.Inst;
        spriteRneder = GetComponent<SpriteRenderer>();
        spriteRneder.color = clo;
    }

    private void OnEnable()
    {
        lifeTime = 4f;
        randomindex = Random.Range(0, manager.BloodSprite.Length);
        spriteRneder.sprite = manager.BloodSprite[randomindex];
    }


    public void EnamvleChoosAction(bool DeadAction)
    {
        if (DeadAction)
        {
            DieShitBlood();
        }
        else
        {
            StartCoroutine(disapear());
        }
    }

    public IEnumerator disapear()
    {
        float guage = 1;
        while (true)
        {
            yield return null;
            guage -= Time.deltaTime * speed;
            //float guage = Mathf.Lerp(0f, 1.1f, lifeTime / lifeCopy);
            clo.a = guage;
            spriteRneder.color = clo;
            if (guage < 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void DieShitBlood()
    {
        float guage = Random.Range(0.2f, 1);
        clo.a = guage;
        spriteRneder.color = clo;
    }
}
