using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Shitblood : PooledObject
{
    /// <summary>
    /// 게임매니저 변수
    /// </summary>
    Factory manager;

    /// <summary>
    /// 색상 변수(public 으로 해서 게임오브젝트에서 수정 가능)
    /// </summary>
    public Color clo;

    /// <summary>
    /// 똥색 피의 스프라이트 렌더러 변수
    /// </summary>
    SpriteRenderer spriteRneder;

    /// <summary>
    /// 스프라이트를 랜덤값으로 가져오기 위한 int변수
    /// </summary>
    int randomindex = 0;

    /// <summary>
    /// 똥피가 사라지는 속도
    /// </summary>
    public float speed=1f;

    System.Action shitdis;
    float timecounting=1;
    private void Awake()
    {
        shitdis = nullF;
        //게임매니저에서 매니저 불러오기
        manager = Factory.Inst;

        //스프라이트 렌더러 불러오기
        spriteRneder = GetComponent<SpriteRenderer>();

        //게임오브젝트에서 지정한 색상을 스프라이트 렌더러 색상값에 넣기(현재 똥색)
        spriteRneder.color = clo;
    }

    private void OnEnable()
    {
        timecounting = 1;

        //랜덤 인덱스 값에 0부터 매니저에서 불러온 BloodSprite의 길이값을 대입한다.
        randomindex = Random.Range(0, manager.BloodSprite.Length);

        //BloodSprite 배열에서 가져온 랜덤한 스프라이트를 스프라이트 렌더러의 스프라이트 이미지에 대입해서 이미지를 넣는다. 
        spriteRneder.sprite = manager.BloodSprite[randomindex];
    }
    private void Update()
    {
        shitdis();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        shitdis -= disa;
    }
    /// <summary>
    /// 똥피의 패턴 선택기
    /// </summary>
    /// <param name="DeadAction">DeadAction에 true를 넣으면 죽었을때 패턴을, false를 넣으면 살아있을때 패턴을 작동한다.</param>
    public void EnamvleChoosAction(bool DeadAction)
    {
        if (DeadAction)
        {
            DieShitBlood();
        }
        else
        {
            shitdis += disa;
        }
    }

    void nullF()
    {

    }


    /// <summary>
    /// 똥이 살아있을때 코루틴 행동패턴(생겨났다가 천천히 페이드 아웃되며 비활성화된다.)
    /// </summary>
    /// <returns></returns>
    private void disa()
    {
        timecounting -= Time.deltaTime * speed;
        clo.a = timecounting;
        spriteRneder.color = clo;
        if (timecounting < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 똥이 죽었을때 작동할 행동패턴(랜덤한 투명값을 가지고 비활성화되지 않는다.)
    /// </summary>
    public void DieShitBlood()
    {
        float guage = Random.Range(0.2f, 1);
        clo.a = guage;
        spriteRneder.color = clo;
    }
}
