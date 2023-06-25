using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    public Transform player;           // ������ �÷��̾�(������) ������

    public GameObject bullet;          // �� �Ѿ� ������
    public float shootCooldown = 3.0f; // �Ѿ� �߻� ��ٿ�
    public float startCooldown;        // �߻� ��ٿ� �ʱ�ȭ �ð�

    private void Start()
    {
        startCooldown = shootCooldown; // ��ٿ� �ð� �ʱ�ȭ
    }

    private void Update()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, 
                                        player.position.y - transform.position.y); // �÷��̾� ����
        
        transform.up = direction; // ȸ�� ��

        if(shootCooldown <= 0)    // �Ѿ� �߻� ����
        {
            Instantiate(bullet, transform.position, transform.rotation);          // �Ѿ� �߻��� ��ġ
            shootCooldown = startCooldown;                                        // �߻� �ʱ�ȭ

        }
        else
        {
            shootCooldown -= Time.deltaTime;                                      // �߻� �ð��� ���� ��� -Time.deltaTime
        }
    }
}


