using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    public Transform player;           // 추적할 플레이어(아이작) 프리팹

    public GameObject bullet;          // 적 총알 프리팹
    public float shootCooldown = 3.0f; // 총알 발사 쿨다운
    public float startCooldown;        // 발사 쿨다운 초기화 시간

    private void Start()
    {
        startCooldown = shootCooldown; // 쿨다운 시간 초기화
    }

    private void Update()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 
                                        player.position.y - transform.position.y); // 플레이어 추적
        
        transform.up = direction; // 회전 축

        if(shootCooldown <= 0)    // 총알 발사 조건
        {
            Instantiate(bullet, transform.position, transform.rotation);          // 총알 발사할 위치
            shootCooldown = startCooldown;                                        // 발사 초기화

        }
        else
        {
            shootCooldown -= Time.deltaTime;                                      // 발사 시간이 남은 경우 -Time.deltaTime
        }
    }
}


