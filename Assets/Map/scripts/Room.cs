using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum RoomType
{
    Start,
    Base,
    Shop,
    Boss
}

public class Room : MonoBehaviour
{
    bool isVisit = false;
    public bool IsVisit
    {
        get => isVisit;
        set
        {
            // 한번 방문하면 다시 신호 안줌
            if (!isVisit && roomtype != RoomType.Start)
            {
                isVisit = true;
                Debug.Log(spawner.transform.parent.name);
                spawner.playerIn?.Invoke();
            }
        }
    }

    public int width;
    public int height;

    float playerMoveDistance = 2.5f;

    Vector2Int myPos = Vector2Int.zero;
    public Vector2Int MyPos
    {
        get => myPos;
        set
        {
            if(myPos == Vector2Int.zero && value != Vector2Int.zero)
            {
                myPos = value;
            }
        }
    }

    public RoomType roomtype = RoomType.Base;

    public Room leftRoom = null;
    public Room rightRoom = null;
    public Room topRoom = null;
    public Room bottomRoom = null;

    public Door[] doors = new Door[4];

    MonsterSpawner spawner;

    private void Awake()
    {
        Transform child;
        for(int i = 0; i< doors.Length; i++)
        {
            child = transform.GetChild(i);
            doors[i] = child.GetComponent<Door>();
            doors[i].DoorType = (DoorType)i;
            doors[i].onPlayerMove += MoveSignal;
            doors[i].onPlayerMove += MovePlayer;
        }

        child = transform.GetChild(4);
        spawner = child.GetComponent<MonsterSpawner>();

        Tilemap tileMap = GetComponentInChildren<Tilemap>();
        width = tileMap.size.x;
        height = tileMap.size.y;
    }

    //private void Start()
    //{
    //    Transform child = transform.GetChild(4);
    //    spawner = child.GetComponent<MonsterSpawner>();
    //}

    private void MovePlayer(DoorType type)
    {
        Player player = GameManager.Inst.Player;

        switch (type)
        {
            case DoorType.left:
                player.transform.position += Vector3.left * playerMoveDistance;
                break;
            case DoorType.right:
                player.transform.position += Vector3.right * playerMoveDistance;
                break;
            case DoorType.top:
                player.transform.position += Vector3.up * playerMoveDistance;
                break;
            case DoorType.bottom:
                player.transform.position += Vector3.down * playerMoveDistance;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 문에서 플레이어 이동시키면 실행할 함수
    /// </summary>
    /// <param name="doorType">문의 타입</param>
    void MoveSignal(DoorType doorType)
    {
        switch (doorType)
        {
            case DoorType.left:
                if (leftRoom != null)
                {
                    leftRoom.IsVisit = true;
                }
                RoomManager.Inst.CurrentRoom = leftRoom;
                break;
            case DoorType.right:
                if (rightRoom != null)
                {
                    rightRoom.IsVisit = true;
                }
                RoomManager.Inst.CurrentRoom = rightRoom;
                break;
            case DoorType.top:
                if (topRoom != null)
                {
                    topRoom.IsVisit = true;
                }
                RoomManager.Inst.CurrentRoom = topRoom;
                break;
            case DoorType.bottom:
                if (bottomRoom != null)
                {
                    bottomRoom.IsVisit = true;
                }
                RoomManager.Inst.CurrentRoom = bottomRoom;
                break;
            default:
                break;
        }
    }
}
