using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shit : EnemyBase
{
    Rigidbody2D rig;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector3 Headto;
    public Color bloodColor = Color.white;
    public float power = 1f;
    public float coolTime;
    float coolTimeCopy;
    public GameObject flyer;

    protected override void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }
    private void Start()
    {
        StartCoroutine(attackcool());
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

    protected override void OnEnable()
    {
        base.OnEnable();
        coolTime = Random.Range(1,3);
        coolTimeCopy = coolTime;
    }


    void Attack()
    {
        rig.AddForce(Headto*power);

        animator.SetTrigger("Attack");
    }
    IEnumerator attackcool()
    {
        while (true)
        {
            if (coolTime > 0)
            {
                coolTime -= Time.deltaTime;
                yield return null;
            }
            else
            {
                Attack();
                coolTime = coolTimeCopy;
                yield return new WaitForSeconds(coolTime); // 5초 기다림
            }
        }
    }

    protected override void Die()
    {
        bloodshatter();
        int rand = Random.Range(2, 5);
        float ranX = Random.Range(-1, 1.1f);
        float ranY = Random.Range(-1, 1.1f);

        Vector2 pos = new Vector2(ranX,ranY);
        for(int i = 0; i<rand; i++)
        {
        GameObject fly = Instantiate(flyer,(Vector2)this.transform.position + pos, this.transform.rotation);
        }
        Destroy(this.gameObject);//피를 다 만들고 나면 이 게임 오브젝트는 죽는다.
    }

    protected override void bloodshatter()
    {
        int bloodCount = UnityEngine.Random.Range(3, 6);//피의 갯수 1~3 사이 정수를 만든다.

        for (int i = 0; i < bloodCount; i++)//피의 갯수만큼 반복작업
        {
            float X = UnityEngine.Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);//피의 위치 조절용 X축
            float Y = UnityEngine.Random.Range(transform.position.y - 0.3f, transform.position.y);//피의 위치 조절용 Y축
            Vector3 bloodpos = new Vector3(X, Y, 0);//피의 위치 설정용 변수 bloodpos
            GameObject bloodshit = Factory.Inst.GetObject(PoolObjectType.EnemyBlood);
            bloodshit.transform.position = bloodpos;
            bloodshit.GetComponent<Bloodhelth>().clo = bloodColor;
        }
    }

}
