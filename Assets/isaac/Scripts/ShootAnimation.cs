using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootAnimation : MonoBehaviour
{
    Player player;
    Animation ani;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        player.isFire += isShoot;
        ani = GetComponent<Animation>();
    }

    void isShoot()
    {
        // 애니메이션에 적용
        // 눈이 감기는 타이밍에 눈물 발사
    }
}
