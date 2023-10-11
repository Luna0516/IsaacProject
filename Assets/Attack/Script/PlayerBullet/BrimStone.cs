using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class BrimStone : PooledObject
{

    Action signal;

    /// <summary>
    /// 플레이어 (아이작)
    /// </summary>
    Player player;

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    Transform firePos;

    /// <summary>
    /// 현재 방의 위치를 알기 위한 변수
    /// </summary>
    Room room;

    /// <summary>
    /// 시작 점 X
    /// </summary>
    float startDotX;
    /// <summary>
    /// 시작 점 Y
    /// </summary>
    float startDotY;

    /// <summary>
    /// 순서대로 위쪽 벽의 y, 아래 벽의 y, 왼쪽 벽의 x, 오른쪽 벽의 x
    /// </summary>
    float[] roomDistance = new float[4];

    /// <summary>
    /// 총 늘어날 거리
    /// </summary>
    float totalDistance;

    /// <summary>
    /// 발사 방향
    /// </summary>
    int dir;

    bool isPressed = false;

    // 계수 관련 변수---------------------------------------------- 

    float speed;
    float damage;

    float chargeGage = 0f;
    float maxGage = 0.8f;
    // ------------------------------------------------------------


    private void Awake()
    {
        player = new Player();
    }
    private void Init()
    {
        if(player != null)
        {
            speed = player.TearSpeed;
            damage = player.Damage;
        }      
    }

    float ChargeGage
    { 
        get
        {
            return chargeGage;
        }

        set
        {
            chargeGage = value;
            if(chargeGage > maxGage)
            {
                chargeGage = maxGage;
            }
        }
    }


    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        room = RoomManager.Inst.CurrentRoom;

        if (player.AttackDir.y >= 1.0f && player.AttackDir.x == 0)
        {
            dir = 0;
        }
        else if (player.AttackDir.y <= -1.0f && player.AttackDir.x == 0)
        {
            dir = 1;
        }
        else if (player.AttackDir.x <= -1.0f && player.AttackDir.y == 0)
        {
            dir = 2;
        }
        else if (player.AttackDir.x >= 1.0f && player.AttackDir.y == 0)
        {
            dir = 3;
        }

        if (dir >= 2)
        {
            transform.rotation = Quaternion.Euler(Vector3.forward * -90);
        }
    }

    private void Start()
    {
        player = GameManager.Inst.Player;
        firePos = player.gameObject.transform.GetChild(0);

        roomDistance[0] = room.MyPos.y * 10 - (10 * 0.5f - 1);
        roomDistance[1] = room.MyPos.y * 10 + (10 * 0.5f - 1);
        roomDistance[2] = room.MyPos.x * 17.9f - (17.9f * 0.5f - 1);
        roomDistance[3] = room.MyPos.x * 17.9f + (17.9f * 0.5f - 1);
    }

    private void FixedUpdate()
    {
        signal();
        if (player.AttackDir != Vector2.zero)
        {
            transform.position = firePos.position;

            startDotX = firePos.position.y;
            startDotY = firePos.position.x;

            if (dir >= 2)
            {
                totalDistance = startDotX - roomDistance[dir];
            }
            else
            {
                totalDistance = startDotY - roomDistance[dir];
            }

            transform.localScale = new Vector3(1, totalDistance * 0.25f, 1);
        }
        else
        {
            StartCoroutine(Gravity_Life(2.0f));
        }
    }
   public void Press()
    {
        if(!isPressed)
        {
            isPressed = true;
            signal += Charging;

        }
    }

    public void Release()
    {
        gameObject.SetActive(true);
        signal -= Charging;
        ChargeGage = 0;
    }

    public void Charging()
    {
        chargeGage += Time.deltaTime * speed;
    }

  /*  void BrimstoneOn()
    {
        if(!isFiring)
        {
            signal += BrimstoneOff;
            signal -= BrimstoneOn;
        }
        
    }

    void BrimstoneOff()
    {
        signal -= BrimstoneOn;
    }*/
}

/*
 * 슈도코드 => 글로 다 적어서 구현하게끔 작성?
 * 
 * 내 위치를 총알 발사 위치로 이동한다. => Factory 로 하면 자동으로 위치 설정될거에요 => 플레이어에서 Factory로 만들면서 자기 눈물 위치에 붙이거든요
 * 플레이어의 총알 발사 방향을 받아와서 저장한다. => 상 하 좌 우
 * 이 방향에 맞게 벽의 x값이나 y값을 받아와서 거리를 계산한다 => 업데이트나 픽스드 업데이트에서 해야한다. => 계속 계산해야 해서
 * 거리값에 맞게 스케일 조절을 해야하기 때문에 0.25를 곱해준다(4로 나눈다)
 * 플레이어가 발사 방향에 손을 떼면 발사되고 일정 시간 지난뒤에 비활성화 되어야 한다.
 * 이 오브젝트랑 닿는 Enemy는 데미지를 입어야 한다
*/