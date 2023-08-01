using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shitblood : PooledObject
{
    GameManager manager;
    public Color clo;
    SpriteRenderer spriteRneder;
    public float lifeTime;
    float lifeCopy;
    int randomindex = 0;
    bool shitDead = false;



    private void Awake()
    {
        manager = GameManager.Inst;
        spriteRneder = GetComponent<SpriteRenderer>();
        spriteRneder.color = clo;
    }

    private void OnEnable()
    {
        lifeTime = 4f;
        lifeCopy = lifeTime;
        randomindex = Random.Range(0, manager.BloodSprite.Length);
        spriteRneder.sprite = manager.BloodSprite[randomindex];
        EnamvleChoosAction(shitDead);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        shitDead = false;
    }

    public void EnamvleChoosAction(bool DeadAction)
    {
        shitDead = DeadAction;
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
        while (true)
        {
            lifeTime -= Time.deltaTime;
            yield return null;
            float guage = Mathf.Lerp(0f, 1.1f, lifeTime / lifeCopy);
            clo.a = guage;
            spriteRneder.color = clo;
            if (lifeTime < 0)
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
