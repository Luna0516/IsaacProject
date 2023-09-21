using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimStone : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    Vector3 currentScale;   // 현재 빔 크기


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
        {
            
            boxCollider.size.Scale(currentScale);
        }
        if (spriteRenderer != null)
        {
           
            currentScale = transform.localScale;
            currentScale.y = 0.2f;      // 사이즈 변경되는지 확인용으로 임시 설정

            //currentScale.y = transform.position.y - collision.transform.position.y; // 길이 만큼? 
            transform.localScale = currentScale;
        }
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
