using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 방의 타입
/// </summary>
public enum RoomType
{
    Start,
    Base,
    Shop,
    Boss
}

public class Room : MonoBehaviour
{
    /// <summary>
    /// 한번 방문 했음을 확인하는 변수
    /// </summary>
    bool isVisit = false;
    /// <summary>
    /// 방문 여부를 확인하는 프로퍼티
    /// </summary>
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

    /// <summary>
    /// 맵의 가로 길이
    /// </summary>
    public int width;
    /// <summary>
    /// 맵의 세로 길이
    /// </summary>
    public int height;

    /// <summary>
    /// 문을 통해 플레이어를 이동시킬 거리
    /// </summary>
    float playerMoveDistance = 3.2f;

    float distance = 0.0f;
    public float Distance
    {
        get => distance;
        private set
        {
            if(value > 0)
            {
                distance = value;
            }
        }
    }

    /// <summary>
    /// 그리드 상 자신의 방의 위치
    /// </summary>
    Vector2Int myPos = Vector2Int.zero;
    /// <summary>
    /// 자신의 방의 위치를 설정하는 프로퍼티
    /// </summary>
    public Vector2Int MyPos
    {
        get => myPos;
        set
        {
            if(myPos == Vector2Int.zero && value != Vector2Int.zero)
            {
                myPos = value;
                Distance = Vector2.SqrMagnitude(MyPos);
            }
        }
    }

    /// <summary>
    /// 자신의 방의 타입
    /// </summary>
    public RoomType roomtype = RoomType.Base;

    /// <summary>
    /// 자신의 왼쪽 방
    /// </summary>
    public Room leftRoom = null;
    /// <summary>
    /// 자신의 오른쪽 방
    /// </summary>
    public Room rightRoom = null;
    /// <summary>
    /// 자신의 위쪽 방
    /// </summary>
    public Room topRoom = null;
    /// <summary>
    /// 자신의 아래쪽 방
    /// </summary>
    public Room bottomRoom = null;

    /// <summary>
    /// 상하좌우 문들
    /// </summary>
    public Door[] doors = new Door[4];

    /// <summary>
    /// 스포너
    /// </summary>
    MonsterSpawner spawner;

    private void Awake()
    {
        Transform child;
        for(int i = 0; i< doors.Length; i++)
        {
            child = transform.GetChild(i);
            doors[i] = child.GetComponent<Door>();
            doors[i].doorType = (DoorType)i + 1;
            doors[i].onPlayerMove += MoveSignal;
            doors[i].onPlayerMove += MovePlayer;
        }

        child = transform.GetChild(4);
        spawner = child.GetComponent<MonsterSpawner>();
        if (spawner != null)
        {
            spawner.onAllEnemyDied += OpenDoor;
        }

        Tilemap tileMap = GetComponentInChildren<Tilemap>();
        width = tileMap.size.x;
        height = tileMap.size.y;
    }

    /// <summary>
    /// 문을 통해 플레이어를 이동 시킬 함수
    /// </summary>
    /// <param name="type"></param>
    private void MovePlayer(DoorType type)
    {
        Player player = GameManager.Inst.Player;

        switch (type)
        {
            case DoorType.Left:
                player.transform.position += Vector3.left * playerMoveDistance;
                break;
            case DoorType.Right:
                player.transform.position += Vector3.right * playerMoveDistance;
                break;
            case DoorType.Up:
                player.transform.position += Vector3.up * playerMoveDistance;
                break;
            case DoorType.Down:
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
            case DoorType.Left:
                if (leftRoom != null)
                {
                    leftRoom.IsVisit = true;
                    RoomManager.Inst.CurrentRoom = leftRoom;
                }
                break;
            case DoorType.Right:
                if (rightRoom != null)
                {
                    rightRoom.IsVisit = true;
                    RoomManager.Inst.CurrentRoom = rightRoom;
                }
                break;
            case DoorType.Up:
                if (topRoom != null)
                {
                    topRoom.IsVisit = true;
                    RoomManager.Inst.CurrentRoom = topRoom;
                }
                break;
            case DoorType.Down:
                if (bottomRoom != null)
                {
                    bottomRoom.IsVisit = true;
                    RoomManager.Inst.CurrentRoom = bottomRoom;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 자신의 방의 문을 여는 함수
    /// </summary>
    public void OpenDoor()
    {
        foreach(Door door in doors)
        {
            if(door != null)
            {
                door.IsOpen = true;
            }
        }
    }
}
