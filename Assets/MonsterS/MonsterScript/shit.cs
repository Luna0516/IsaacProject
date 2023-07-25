using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shit : EnemyBase
{
    Rigidbody rig;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 Headto;
    public Color bloodColor = Color.white;

    public float coolTime = 0.5f;

    float CoolTime
    {
        get 
        {          
            return coolTime - Time.deltaTime; 
        }
        set {
            float CTimeing = coolTime;
            if (coolTime < 0f)
            {
                coolTime = CTimeing;
            }
            }

    }
    protected override void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Headto = (target.position-transform.position).normalized;
        if(Headto.x<0)
        {
            spriteRenderer.flipX = true;
        }
        else 
        { spriteRenderer.flipX = false; }

    }
    void Attack()
    {
        rig.AddForce(Headto);
        animator.SetTrigger("Attack");
    }

    protected override void Die()
    {
        bloodshatter();
        gameObject.SetActive(false);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }
    protected override void bloodshatter()
    {
        int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Instantiate(blood, bloodpos, Quaternion.identity);//bloodshit이라는 게임 오브젝트 생성 종류는 빈 게임 오브젝트, 위치는 bloodpos, 각도는 기존 각도
            bloodshit.GetComponent<Bloodhelth>().clo = bloodColor;
        }
    }

}
