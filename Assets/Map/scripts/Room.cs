using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum RoomType
{
    Base,
    Shop,
    Boss
}

public enum DoorType
{
    left,
    right,
    top,
    bottom
}

public class Room : MonoBehaviour
{
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

    public Room leftRoom = null;
    public Room rightRoom = null;
    public Room topRoom = null;
    public Room bottomRoom = null;

    public int width;
    public int height;

    float playerMoveDistance = 2.5f;

    public RoomType roomtype = RoomType.Base;

    public Door[] doors = new Door[4];

    Tilemap tileMap;

    private void Awake()
    {
        Transform child;

        for(int i = 0; i< doors.Length; i++)
        {
            child = transform.GetChild(i);
            doors[i] = child.GetComponent<Door>();
            doors[i].onPlayerMove += MoveSignal;
            doors[i].doorType = (DoorType)i;
            doors[i].onPlayerMove += MovePlayer;
        }

        child = transform.GetChild(4);
        tileMap = child.GetComponent<Tilemap>();

        width = tileMap.size.x;
        height = tileMap.size.y;
    }

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
        
    }
}
