using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BrimStone : PooledObject
{
    /// <summary>
    /// 플레이어 (아이작)
    /// </summary>
    Player player;

    /// <summary>
    /// 현재 방의 위치를 알기 위한 변수
    /// </summary>
    Room room;

    /// <summary>
    /// 스프라이트 렌더러 컴포넌트
    /// </summary>
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// 순서대로 위쪽 벽의 y, 아래 벽의 y, 왼쪽 벽의 x, 오른쪽 벽의 x
    /// </summary>
    float[] roomDistance = new float[4];

    /// <summary>
    /// 발사 방향
    /// </summary>
    int dir;

    /// <summary>
    /// 발사 버튼이 눌렸는지 확인
    /// </summary>
    bool isPressed = false;

    /// <summary>
    /// 발사 가능한지 확인
    /// </summary>
    bool isShootable = false;

    /// <summary>
    /// 발사중인지 확인
    /// </summary>
    bool isFireing = false;
    
    /// <summary>
    /// 발사 속도
    /// </summary>
    float speed;

    /// <summary>
    /// 적에게 줄 피해량 (데미지)
    /// </summary>
    float damage;

    /// <summary>
    /// 게이지 양
    /// </summary>
    float chargeGage = 0f;

    /// <summary>
    /// 게이지 최고치
    /// </summary>
    float maxGage = 1f;
    
    /// <summary>
    /// 발사 시간
    /// </summary>
    float actionTimer;

    /// <summary>
    /// 게이지 충전 관련 확인용 델리게이트
    /// </summary>
    Action signal;
    
    /// <summary>
    /// 브림스톤 크기
    /// </summary>
    Vector2 Spritesize;

    /// <summary>
    /// 브림스톤 기본 사이즈
    /// </summary>
    Vector2 defaultSize;

    /// <summary>
    /// 발사 위치
    /// </summary>
    Vector2 firePos;
    float ChargeGage
    {
        get
        {
            return chargeGage;          
        }

        set
        {
            chargeGage = value;
            if (chargeGage > maxGage)   // 게이지가 가득 차면
            {
                chargeGage = maxGage;   
                isShootable = true;       // 발사 가능한 상황
            }
        }
    }

    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();  // 브림스톤 스프라이트 찾기
        defaultSize = new Vector2(2.95f, 0);    // 브림스톤 기본 크기
        firePos = new Vector2(0, 0.31f);        // 발사 위치   
        Spritesize = defaultSize;               // 브림스톤 스프라이트 사이즈 기본 크기로
        signal = () => { };                     // signal의 null 값 방지용 람다
    }
    private void OnEnable()
    {
        spriteRenderer.size = Spritesize;
    }

    private void Start()
    {
        player = GameManager.Inst.Player;       // 플레이어 찾고

        if (player != null)
        {
            Init();                             // 초기화
        }
    }

    private void FixedUpdate()
    {
        signal();
        this.transform.localPosition = firePos;    // 발사 위치 계속 업데이트
    }
    public void Press()
    {
        if (!isPressed && !isFireing)               // 발사 버튼이 눌리지 않았고, 발사 중이 아니라면
        {
            isPressed = true;                       // 버튼이 눌렸다 알리고
            signal += Charging;                     // 발사 준비를 위해 충전함
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isFireing)      // 대상이 태그가 적이고 발사중이라면
        {
            EnemyBase enemy = collision.transform.GetComponentInChildren<EnemyBase>();  // 적의 위치를 찾고
            if (enemy == null)  // 적이 없다면
            {
                enemy = collision.GetComponentInParent<EnemyBase>();    // 적을 찾음
            }
            enemy.damage = damage;                  // 적은 데미지만큼 피해를 입음
            enemy.Hitten();                         // 피격 판정
            Vector2 nuckBackDir = player.AttackDir; 
            enemy.NuckBack(nuckBackDir.normalized); // 플레이어 공격 방향으로 적이 밀림
        }
    }

    /// <summary>
    /// 발사 방향키를 놓았을 때 실행될 함수
    /// </summary>
    public void Release()
    {
        if (isPressed)          // 발사키가 눌렸었다면
        {
            ChargeGage = 0;     // 충전량을 0으로 초기화
            signal -= Charging; // 충전중이 아니라고 알림
            
            if (isShootable)      // 발사 가능한 상태라면
            {
                actionTimer = 0.7f;     // 0.7초 동안 발사
                isFireing = true;        
                signal += FireActive;   // 발사중이라고 알림
            }
            isPressed = false;          // 현재 발사키가 눌리지 않은 상태
            isShootable = false;          // 새롭게 발사 가능한 상태가 아님
        }
    }

    /// <summary>
    /// 게이지 충전용 함수
    /// </summary>
    public void Charging()
    {
        ChargeGage += Time.deltaTime * speed;   // 충전 게이지는 시간 * 발사 속도로 모임
        FireDirCal(player.AttackDir);           // 발사 방향은 플레이어의 발사 방향
    }

    /// <summary>
    ///  초기화 함수
    /// </summary>
    private void Init()
    {
        if (player != null)                 // 플레이어가 있다면
        {
            speed = player.TearSpeed;       // 공격속도는 플레이어의 공격속도
            damage = player.Damage*0.25f;   // 데미지는 플레이어 공격력의 1/4
        }
    }

    /// <summary>
    /// 발사중인지 확인용 함수
    /// </summary>
    void FireActive()
    {
        if (actionTimer > 0)                    // 발사 시간이 0보다 크다면
        {
            WallCal();                          // 벽까지 거리 계산
            spriteRenderer.size = Spritesize;   // 브림스톤 스프라이트 사이즈를 거리만큼 맞춤
            actionTimer -= Time.fixedDeltaTime; // 쏘는 동안 발사 시간 감소
        }
        else
        {
            isFireing = false;
            Spritesize = defaultSize;
            spriteRenderer.size = Spritesize;
            signal -= FireActive;
        }
    }

    /// <summary>
    /// 발사 방향 계산용 함수
    /// </summary>
    /// <param name="dircal"></param>
    void FireDirCal(Vector2 dircal)
    {
        if (dircal.y >= 1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            dir = 0;
        }
        else if (dircal.y <= -1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            dir = 1;
        }
        else if (dircal.x <= -1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            dir = 2;
        }
        else if (dircal.x >= 1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            dir = 3;
        }
    }

    /// <summary>
    /// 벽까지 거리 계산용 함수
    /// </summary>
    void WallCal()
    {
        room = RoomManager.Inst.CurrentRoom;
        //위
        roomDistance[0] = room.MyPos.y * 10 - (10 * 0.5f - 1);
        //아래
        roomDistance[1] = room.MyPos.y * 10 + (10 * 0.5f - 1);
        //왼
        roomDistance[2] = room.MyPos.x * 17.9f - (17.9f * 0.5f - 1);
        //오
        roomDistance[3] = room.MyPos.x * 17.9f + (17.9f * 0.5f - 1);
        float legth;
        switch (dir)    // 발사 방향에 맞게 길이 계산
        {
            case 0:
                legth = roomDistance[1] - player.transform.position.y;
                Spritesize.y = legth;
                break;
            case 1:
                legth = player.transform.position.y - roomDistance[0];
                Spritesize.y = legth + 0.5f;
                break;
            case 2:
                legth = player.transform.position.x - roomDistance[2];
                Spritesize.y = legth;
                break;
            case 3:
                legth = roomDistance[3] - player.transform.position.x;
                Spritesize.y = legth;
                break;
            default:
                break;
        }
    }
}
