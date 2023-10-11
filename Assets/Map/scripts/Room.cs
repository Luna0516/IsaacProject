using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 방의 종류
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
                isVisit = value;

                if(roomtype == RoomType.Boss)
                {
                    foreach(Door door in doors)
                    {
                        door.gameObject.SetActive(false);
                    }
                }

                monsterSpawner.playerIn?.Invoke();
            }
        }
    }

    /// <summary>
    /// 맵의 가로 길이
    /// </summary>
    int width;

    /// <summary>
    /// 맵의 세로 길이
    /// </summary>
    int height;

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
    /// 자신의 위쪽 방
    /// </summary>
    public Room upRoom = null;
    /// <summary>
    /// 자신의 아래쪽 방
    /// </summary>
    public Room downRoom = null;
    /// <summary>
    /// 자신의 왼쪽 방
    /// </summary>
    public Room leftRoom = null;
    /// <summary>
    /// 자신의 오른쪽 방
    /// </summary>
    public Room rightRoom = null;

    /// <summary>
    /// 상하좌우 문들
    /// </summary>
    public Door[] doors = new Door[4];

    /// <summary>
    /// 몬스터 스포너
    /// </summary>
    MonsterSpawner monsterSpawner;

    /// <summary>
    /// 계단 게임 오브젝트
    /// </summary>
    public GameObject stair;

    private void Awake()
    {
        Transform child;
        for(int i = 0; i< doors.Length; i++)
        {
            child = transform.GetChild(i);
            doors[i] = child.GetComponent<Door>();
            doors[i].DoorType = (DoorType)i + 1;
            doors[i].onPlayerMove += MovePlayer;
        }

        child = transform.GetChild(4);
        monsterSpawner = child.GetComponent<MonsterSpawner>();
        // 시작방은 스포너가 없다...
        if (monsterSpawner != null)
        {
            monsterSpawner.onAllEnemyDied += OpenDoor;
        }

        Tilemap tileMap = GetComponentInChildren<Tilemap>();
        width = tileMap.size.x;
        height = tileMap.size.y;
    }

    private void Start()
    {
        if (roomtype == RoomType.Boss)
        {
            stair.SetActive(false);
        }
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
            case DoorType.Up:
                player.transform.position += Vector3.up * playerMoveDistance;
                upRoom.IsVisit = true;
                RoomManager.Inst.CurrentRoom = upRoom;
                break;
            case DoorType.Down:
                player.transform.position += Vector3.down * playerMoveDistance;
                downRoom.IsVisit = true;
                RoomManager.Inst.CurrentRoom = downRoom;
                break;
            case DoorType.Left:
                player.transform.position += Vector3.left * playerMoveDistance;
                leftRoom.IsVisit = true;
                RoomManager.Inst.CurrentRoom = leftRoom;
                break;
            case DoorType.Right:
                player.transform.position += Vector3.right * playerMoveDistance;
                rightRoom.IsVisit = true;
                RoomManager.Inst.CurrentRoom = rightRoom;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 방들의 위치를 초기화 하는 함수
    /// </summary>
    public void RefreshPosition()
    {
        transform.position = RoomPosition();
    }

    /// <summary>
    /// 방의 현재 연결된 방에 따라 문을 비활성화 시키는 함수
    /// </summary>
    public void RefreshDoor()
    {
        if (upRoom == null)
        {
            doors[0].DoorType = DoorType.None;
        }

        if (downRoom == null)
        {
            doors[1].DoorType = DoorType.None;
        }

        if (leftRoom == null)
        {
            doors[2].DoorType = DoorType.None;
        }

        if (rightRoom == null)
        {
            doors[3].DoorType = DoorType.None;
        }
    }

    /// <summary>
    /// 자신 방의 실제 위치 구하는 함수
    /// </summary>
    public Vector2 RoomPosition()
    {
        Vector2 currentRoomPos = new Vector2(MyPos.x * width, MyPos.y * height);

        return currentRoomPos;
    }

    /// <summary>
    /// 자신의 방의 문을 여는 함수
    /// </summary>
    /// <param name="killCount">죽인 몬스터 수</param>
    public void OpenDoor(int killCount)
    {
        GameManager.Inst.totalKill += killCount;

        ItemSpawn();

        foreach (Door door in doors)
        {
            if(roomtype == RoomType.Boss && door.DoorType != DoorType.None)
            {
                door.gameObject.SetActive(true);
            }

            if(door.DoorType != DoorType.None)
            {
                door.IsOpen = true;
            }
        }

        if(roomtype == RoomType.Boss)
        {
            stair.SetActive(true);
        }
    }

    /// <summary>
    /// 방에 있는 몬스터가 다 죽으면 아이템 스포너를 통해 아이템 생성 함수
    /// </summary>
    void ItemSpawn()
    {
        if (ItemFactory.Inst != null)
        {
            Vector2 itemSpawnPos = transform.position;

            if (roomtype == RoomType.Start)
            {
                //GameObject itemObj = ItemFactory.Inst.CreatePassiveItem((PassiveItem)(Random.Range(0, System.Enum.GetValues(typeof(PassiveItem)).Length)));
                GameObject itemObj = ItemFactory.Inst.CreatePassiveItem(PassiveItem.Brimstone);
                itemObj.transform.position = itemSpawnPos;
                return;
            }
            else if (roomtype == RoomType.Base)
            {

                // 0.0 < Active < 0.03 < Passive < 0.2 < Heart < 0.5 < Props < 0.8 < Nothing < 1.0
                float itemType = Random.value;

                if (itemType < 0.03)
                {
                    GameObject itemObj = ItemFactory.Inst.CreateActiveItem((ActiveItem)(Random.Range(0, System.Enum.GetValues(typeof(ActiveItem)).Length)));
                    itemObj.transform.position = itemSpawnPos;
                    return;
                }
                else if (itemType < 0.2)
                {
                    GameObject itemObj = ItemFactory.Inst.CreatePassiveItem((PassiveItem)(Random.Range(0, System.Enum.GetValues(typeof(PassiveItem)).Length)));
                    itemObj.transform.position = itemSpawnPos;
                    return;
                }
                else if (itemType < 0.5)
                {
                    GameObject itemObj = ItemFactory.Inst.CreateHeartItem((HeartItem)(Random.Range(0, System.Enum.GetValues(typeof(HeartItem)).Length)));
                    itemObj.transform.position = itemSpawnPos;
                    return;
                }
                else if (itemType < 0.8)
                {
                    GameObject itemObj = ItemFactory.Inst.CreatePropsItem((PropsItem)(Random.Range(0, System.Enum.GetValues(typeof(PropsItem)).Length)));
                    itemObj.transform.position = itemSpawnPos;
                    return;
                }
                else
                {
                    return;
                }
            }
            else if(roomtype == RoomType.Boss)
            {
                // 패시브 or 액티브 아이템 소환
                float itemType = Random.value;

                if (itemType < 0.05)
                {
                    GameObject itemObj = ItemFactory.Inst.CreateActiveItem((ActiveItem)(Random.Range(0, System.Enum.GetValues(typeof(ActiveItem)).Length)));
                    itemObj.transform.position = itemSpawnPos;
                }
                else
                {
                    GameObject itemObj = ItemFactory.Inst.CreatePassiveItem((PassiveItem)(Random.Range(0, System.Enum.GetValues(typeof(PassiveItem)).Length)));
                    itemObj.transform.position = itemSpawnPos;
                }

                // 하트 or 프롭스 아이템 소환 x2
                for (int i = 0; i< 2; i++)
                {
                    itemType = Random.value;
                    itemSpawnPos = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)) + Vector3.down;
                    if (itemType < 0.5)
                    {
                        GameObject itemObj = ItemFactory.Inst.CreateHeartItem((HeartItem)(Random.Range(0, System.Enum.GetValues(typeof(HeartItem)).Length)));
                        itemObj.transform.position = itemSpawnPos;
                    }
                    else
                    {
                        GameObject itemObj = ItemFactory.Inst.CreatePropsItem((PropsItem)(Random.Range(0, System.Enum.GetValues(typeof(PropsItem)).Length)));
                        itemObj.transform.position = itemSpawnPos;
                    }
                }
            }
        }
    }
}