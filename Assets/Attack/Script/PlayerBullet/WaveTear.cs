using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTear : AttackBase
{
    /// <summary>
    /// 웨이브 (위,아래 움직임) 크기 
    /// </summary>
    public float waveSize = 3.0f;

    /// <summary>
    /// 웨이브 (위,아래 움직임) 빈도
    /// </summary>
    public float waveFrequency = 10.0f; 

    /// <summary>
    /// 위아래 움직임 빈도 조절 변수
    /// </summary>
    float elapsed;


    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        elapsed += (Time.fixedDeltaTime * waveFrequency);   // 웨이브 빈도 조절
        
        //위아래 움직임
        float verticalOffset = Mathf.Cos(elapsed) * waveSize;           
        Vector2 verticalMovement = new Vector2(0f, verticalOffset);     

        // 주어진 방향과 위아래 움직임을 결합하여 총알 이동
        Vector2 combinedMovement = dir * speed * Time.fixedDeltaTime + verticalMovement * Time.fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + combinedMovement);
        
    }
}
