using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_shooter : MonoBehaviour
{
    public GameObject Bullet;
    AttackBase instingbullet;
    public float damage = 1f;
    public float fireRate = 2f;
    public float Damage
    {
        get => damage;
        private set => damage = value;
    }

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            GameObject bull = Instantiate(Bullet.gameObject,this.transform.position,Quaternion.identity);
            instingbullet = bull.GetComponent<AttackBase>();
            instingbullet.dir = this.transform.position.normalized;
            yield return new WaitForSeconds(fireRate);
        }
    }
}
