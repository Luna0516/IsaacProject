using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BigTear : AttackBase
{
    /// <summary>
    /// 눈물의 크기를 조절해주는 변수값
    /// </summary>
    float pulsingSize = 3f;

    /// <summary>
    /// EnemyBase의 HP값을 불러오기 위한 변수
    /// </summary>
    EnemyBase enemy;

    /// <summary>
    /// 적과 충돌시 판정체크를 위한 bool변수
    /// </summary>
    bool onehitCount = false;

    /// <summary>
    /// 데미지 복사용 첫번째 변수
    /// </summary>
    float damageCopy;

    /// <summary>
    /// 데미지 복사용 두번째 변수
    /// </summary>
    float damageCopy1;
    
    protected override void Init() //오브젝트 활성화때 실행되는 함수
    {
        base.Init(); //부모 클래스의 기본 함수 실행 후..
        damageCopy1 = (Damage + 4) * 2; //데미지 커피 변수에 총알의 데미지를 계산해서 입력
        damageCopy = damageCopy1;     //데미지 커피 변수를 다른 커피 변수에 복제
        this.transform.localScale *= pulsingSize; //이 개체의 크기값에 크기 조절 변수 곱한값을 지정
        scale = this.transform.localScale;//크기값을 scale값에 저장
        onehitCount = false; //충돌 판정체크 초기화
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        onehitCount = true; //아무 개체와 충돌시 충돌체크 활성화
        if (collision.gameObject.CompareTag("Enemy") && onehitCount)//적이고 충돌체크가 활성화일때(중복 체크로 적을 여러번 때리기 방지)
        {
            this.Damage = damageCopy1;//데미지 프로퍼티에 데미지 복사본을 대입
            enemy = collision.gameObject.GetComponent<EnemyBase>();//부딫힌 대상이 적인 경우 EnemyBase값을 불러온다.
            damageCopy1 = damageCopy1 - enemy.HP;//데미지 복사 변수의 데미지에서 적의 체력을 뺀 값을 계산해 다시 대입

//                                                          수정된 데미지 / 초기 데미지
            scale = Vector3.one* Mathf.Clamp(pulsingSize * (damageCopy1 / damageCopy),1.5f,3f);//크기 계산.최소1.5,최대3의 값 사이의 숫자 도출


            this.transform.localScale = scale;//이 개체의 크기에 sclae변수 대입
            if (damageCopy1 <= 0)//데미지 복사본이 0보다 같거나 작을때 실행
            {
                StopAllCoroutines();
                TearDie();//눈물 사망처리
            }
            onehitCount = false;//모든 과정이 끝나면 충돌체크 비활성화
        }
        if (collision.gameObject.CompareTag("Wall"))//벽일 경우
        {
            scale = this.transform.localScale;//scale 변수에 현재 크기 지정(scale값은 TearExplosion에서 사용된다.)
            StopAllCoroutines();
            TearDie();//눈물 사망 처리
        }
    }
    protected override IEnumerator Gravity_Life(float delay = 0.0f)
    {
        dropDuration = lifeTime * startGravity;                 // 눈물 중력 적용 되는 시간 (길이)
        yield return new WaitForSeconds(dropDuration);

        rigidBody.gravityScale = gravityScale;                          // 눈물에 적용될 중력 수치
        yield return new WaitForSeconds(delay - dropDuration);

        TearExplosion();
        gameObject.SetActive(false);
    }


}
