using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodhelth : MonoBehaviour
{
    GameManager manager;
    Color clo;
    SpriteRenderer spriteRneder;
    public float lifeTime;
    float thislifeTime;
    int randomindex=0;


    private void Awake()
    {
        manager = GameManager.Inst;
        randomindex = Random.Range(0, manager.BloodSprite.Length);
    }
    private void OnEnable()
    {
        spriteRneder = GetComponent<SpriteRenderer>();
        lifeTime = Random.Range(15f, 50f);
        clo = spriteRneder.color;
        thislifeTime = lifeTime;
        spriteRneder.sprite = manager.BloodSprite[randomindex];
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        float guage = Mathf.Lerp(0f, 1f, lifeTime / thislifeTime);
        clo.a = guage;
        spriteRneder.color = clo;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
            //this.gameObject.SetActive(false);
        }
    }
}
