using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : AttackBase
{

    
    
    Vector2 startPoint = Vector2.zero;
    Player player;

    bool isReturning = false;


    public float returnSpeed = 3.0f;
    public float returnDelay = 1.0f;

    protected override void Awake()
    {
        //player = GetComponent<Player>();
        player = FindObjectOfType<Player>();    
    }


    private void Start()
    {
        startPoint = transform.position;
        StartCoroutine(LifeOver(returnDelay));
    }

    private void Update()
    {
        AddGravity();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    protected override void AddGravity()
    {
        startPoint = player.transform.position;
        player.range = 3.0f;
        Vector2 targetPosition = new Vector2(startPoint.x + 0.5f, startPoint.y); // ���� �� ȸ�� ����

        if (!isReturning)
        {
            //transform.position = Vector2.MoveTowards(targetPosition, new Vector2(player.range,0), speed * Time.deltaTime);
            transform.Translate(Time.deltaTime * speed * dir, Space.World);
        }
        else
        {
            
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, returnSpeed * Time.deltaTime);
        
            //transform.position = Vector2.MoveTowards(transform.position, startPoint, returnSpeed * Time.deltaTime);
        }


    }
    protected override void TearDie(Collision2D collision)
    {
        //base.TearDie(collision);
    }

    // �ش� ��ũ��Ʈ������ Į ���� ������ ������ ��
    protected override IEnumerator LifeOver(float delay) 
    {
        yield return new WaitForSeconds(delay);

        isReturning = true;

        
    }


}
