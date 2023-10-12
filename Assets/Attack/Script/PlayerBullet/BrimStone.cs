using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class BrimStone : PooledObject
{

    SpriteRenderer spriteRenderer;

    Action signal;

    float actionTimer;

    Vector2 spritesize;
    Vector2 defaltsize;
    /// <summary>
    /// 플레이어 (아이작)
    /// </summary>
    Player player;

    /// <summary>
    /// 현재 방의 위치를 알기 위한 변수
    /// </summary>
    Room room;

    /// <summary>
    /// 순서대로 위쪽 벽의 y, 아래 벽의 y, 왼쪽 벽의 x, 오른쪽 벽의 x
    /// </summary>
    float[] roomDistance = new float[4];

    /// <summary>
    /// 발사 방향
    /// </summary>
    int dir;

    bool isPressed = false;
    bool shotAble = false;
    bool isFireing = false;
    // 계수 관련 변수---------------------------------------------- 

    float speed;
    float damage;

    float chargeGage = 0f;
    float maxGage = 1f;
    // ------------------------------------------------------------



    float ChargeGage
    {
        get
        {
            return chargeGage;
        }

        set
        {
            chargeGage = value;
            if (chargeGage > maxGage)
            {
                chargeGage = maxGage;
                shotAble = true;
            }
        }
    }

    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        defaltsize = new Vector2(2.95f, 0);
        spritesize = defaltsize;
        signal = () => { };
    }
    private void OnEnable()
    {
        spriteRenderer.size = spritesize;
    }

    private void Start()
    {
        player = GameManager.Inst.Player;

        if (player != null)
        {
            Init();
        }
    }

    private void FixedUpdate()
    {
        signal();
    }
    public void Press()
    {
        if (!isPressed && !isFireing)
        {
            isPressed = true;
            signal += Charging;
        }
    }

    public void Release()
    {
        if (isPressed)
        {
            ChargeGage = 0;
            signal -= Charging;
            if (shotAble)
            {
                actionTimer = 0.7f;
                isFireing = true;
                signal += FireActive;
            }
            isPressed = false;
            shotAble = false;
        }
    }

    public void Charging()
    {
        ChargeGage += Time.deltaTime * speed;
        fireDirCal(player.AttackDir);
    }

    private void Init()
    {
        if (player != null)
        {
            speed = player.TearSpeed;
            damage = player.Damage;
        }
    }

    void FireActive()
    {
        if (actionTimer > 0)
        {
            wallCal();
            spriteRenderer.size = spritesize;
            actionTimer -= Time.fixedDeltaTime;
        }
        else
        {
            isFireing = false;
            spritesize = defaltsize;
            spriteRenderer.size = spritesize;
            signal -= FireActive;
        }
    }

    void fireDirCal(Vector2 dircal)
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
    void wallCal()
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
        switch (dir)
        {
            case 0:
                legth = roomDistance[1] - player.transform.position.y;
                spritesize.y = legth;
                break;
            case 1:
                legth = player.transform.position.y - roomDistance[0];
                spritesize.y = legth + 0.5f;
                break;
            case 2:
                legth = player.transform.position.x - roomDistance[2];
                spritesize.y = legth;
                break;
            case 3:
                legth = roomDistance[3] - player.transform.position.x;
                spritesize.y = legth;
                break;
            default:
                break;
        }
    }
}
