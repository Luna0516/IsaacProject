using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimStone : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    Vector3 currentScale;   // 현재 빔 크기
    Vector3 startPos;       // 빔 발사 위치
    bool isAttacking = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }
    private void Start()
    {
        DisableBeam();
    }

    private void Update()
    {
        // 테스트 코드
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            // Space 키를 누르면 빔 발사
            StartAttack();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isAttacking)
        {
            isAttacking = false;
            DisableBeam();
        }
        
        if(isAttacking)
        {
            LengthenBeam();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttacking)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
            {
                startPos = transform.position;
                // 충돌한 대상이 적이나 벽이면 빔의 길이를 충돌 거리에 따라 조절
                Vector3 hitPoint = collision.contacts[0].point;
                float newLength = Vector3.Distance(startPos, hitPoint);
                
                LengthChange(newLength);
            }
        }
    }

    void StartAttack()
    {
        // 빔을 발사하는 동안 충돌 처리를 활성화
        isAttacking = true;
        EnableBeam();
    }

    void EnableBeam()
    {
        // 빔 활성화 시 스프라이트와 콜라이더를 활성화
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        
        // 원하는 빔 크기로 설정
        currentScale = new Vector3(1f, 1f, 1f); // 예시로 y축 크기를 10으로 설정
        transform.localScale = currentScale;

         
    }

    void DisableBeam()
    {
        // 빔 비활성화 시 스프라이트와 콜라이더를 비활성화
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }
    float fireSpeed = 3.0f;
    void LengthChange(float length)
    {
        //플레이어 발사 위치에서 부딪힌 거리
        currentScale.y = length;
        transform.localScale = currentScale;
    }

    void LengthenBeam()
    {
        // 빔을 발사 중인 동안 길이를 늘려줌
        float maxLength = 10.0f; // 최대 길이 설정
        currentScale.y = Mathf.Clamp(currentScale.y + Time.deltaTime * fireSpeed, 0f, maxLength);
        transform.localScale = currentScale;
    }





    // 스크립트
    // 1. 브림스톤의 기본 사이즈를 최대한 길게 설정 
    // 2. 브림스톤이 충돌하는 대상에 따라 boxcollider와 sprite의 scale을 변경하도록 설정 

    // 작동 방식 
    // 1. 빔이 길게 발사됨 
    // 2. 만일 충돌 대상이 적이나 벽이라면 
    // 3. currentScale을 줄임 (동시에 콜라이더의 크기가 줄어듬) 
    // 4. 적이 공격 범위에서 사라진다면 다시 길어져야함.
    // + X축 최대길이를 기본으로 설정 (y축이 더 짧기 때문에) 


    // ++ currentScale의 길이를 충돌거리 만큼 조절하는 법?
}
