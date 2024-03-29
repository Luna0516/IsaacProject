using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    /// <summary>
    /// 로딩 중일 때 다른 상태를 변경하지 않게 하기 위한 변수
    /// </summary>
    public bool isLoading = true;

    /// <summary>
    /// 방이 성공적으로 생성되었는지 확인하는 값
    /// </summary>
    bool createSuccess = false;

    /// <summary>
    /// 현재 방에 생성할 다른 방의 방향 (0 = Left / 1 = Right / 2 = Up / 3 = Down)
    /// </summary>
    int createRoomDir = 0;

    /// <summary>
    /// 생성된 방의 개수
    /// </summary>
    int roomNum = 0;

    /// <summary>
    /// 생성할 방의 개수
    /// </summary>
    int createRoomCount;

    /// <summary>
    /// 다음 방을 생성하기 전에 리스트에서 탐색할 방의 번호
    /// </summary>
    int index = 0;

    /// <summary>
    /// 방 생성 최소 개수
    /// </summary>
    public int minCreateRoomCount = 3;

    /// <summary>
    /// 방 생성 최대 개수
    /// </summary>
    public int maxCreateRoomCount = 12;

    /// <summary>
    /// 방에 붙일 다른 방의 개수
    /// </summary>
    int attachCount = 0;

    /// <summary>
    /// 방 하나에 붙을수 있는 다른 방의 최소 개수
    /// </summary>
    public int minAttachRoomCount = 2;

    /// <summary>
    /// 방 하나에 붙을수 있는 다른 방의 최대 개수
    /// </summary>
    public int maxAttachRoomCount = 3;

    /// <summary>
    /// 방향에 따른 Vector2Int 값 ( 0 = up, 1 = down, 2 = left, 3 = right )
    /// </summary>
    Vector2Int[] dirVec = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    /// <summary>
    /// 시작 방의 프리펩
    /// </summary>
    public GameObject startRoomPrefab;

    /// <summary>
    /// 기본 방의 프리펩
    /// </summary>
    public GameObject baseRoomPrefab;

    /// <summary>
    /// 상점 방의 프리펩
    /// </summary>
    public GameObject shopRoomPrefab;

    /// <summary>
    /// 보스 방의 프리펩
    /// </summary>
    public GameObject bossRoomPrefab;

    /// <summary>
    /// 보스 체력 바 오브젝트
    /// </summary>
    public GameObject bossBarObject;

    /// <summary>
    /// 현재 방(생성 중, 플레이중)
    /// </summary>
    Room currentRoom = null;
    /// <summary>
    /// 현재 방에 알리고 설정하기 위한 프로퍼티
    /// </summary>
    public Room CurrentRoom
    {
        get => currentRoom;
        set
        {
            if(currentRoom != value)
            {
                currentRoom = value;
                if (!isLoading)
                {
                    if (!currentRoom.IsVisit && currentRoom.roomtype == RoomType.Boss)
                    {
                        bossBarObject.SetActive(true);
                    }
                    currentRoom.IsVisit = true;
                    onChangeRoom?.Invoke(currentRoom);
                }
            }
        }
    }

    Room bossRoom;
    public Room BossRoom => bossRoom;

    /// <summary>
    /// 생성된 방들의 리스트
    /// </summary>
    List<Room> listRooms = new List<Room>();

    /// <summary>
    /// 방 연결 대기 중인 방들의 리스트
    /// </summary>
    List<Room> connectReadyRooms = new List<Room>();

    /// <summary>
    /// 방이 변경되었음을 알리는 델리게이트
    /// </summary>
    public System.Action<Room> onChangeRoom;

    protected override void OnInitialize()
    {
        BossHealth bossHealth = FindObjectOfType<BossHealth>();
        if (bossHealth != null)
        {
            bossBarObject = bossHealth.gameObject;
        }

        isLoading = true;

        ListRoomsInit();

        createRoomCount = Random.Range(minCreateRoomCount, maxCreateRoomCount + 1);

        CreateStartRoom();

        CreateBaseRoom();

        CreateBossRoom();

        RefreshRoomPosition();

        RefreshBossRoom();

        isLoading = false;

        CurrentRoom = listRooms[0];

        if(CurrentRoom.roomtype == RoomType.Start)
        {
            CurrentRoom.OpenDoor(0);
        }
    }

    /// <summary>
    /// 방들의 리스트 초기화 함수
    /// </summary>
    private void ListRoomsInit()
    {
        roomNum = 0;

        if (listRooms.Count < 1) { return; }

        for(int i = 0; i< listRooms.Count; i++)
        {
            Destroy(listRooms[i].gameObject);
        }

        listRooms.Clear();
    }

    /// <summary>
    /// 시작 방을 만드는 함수
    /// </summary>
    public void CreateStartRoom()
    {
        GameObject startRoomObj = Instantiate(startRoomPrefab, transform);

        Room startRoom = startRoomObj.GetComponent<Room>();

        startRoom.roomtype = RoomType.Start;
        startRoom.MyPos = Vector2Int.zero;
        startRoomObj.name = $"StartRoom_({startRoom.MyPos.x}, {startRoom.MyPos.y})";

        CurrentRoom = startRoom;

        listRooms.Add(startRoom);
        connectReadyRooms.Add(startRoom);

        roomNum++;
    }

    /// <summary>
    /// 기본 방들을 만드는 함수
    /// </summary>
    public void CreateBaseRoom()
    {
        while (true)
        {
            createSuccess = false;

            attachCount = Random.Range(minAttachRoomCount, maxAttachRoomCount + 1);

            for (int i = 0; i < attachCount;)
            {
                createSuccess = false;

                if (NeighborRoomCount(CurrentRoom) >= attachCount)
                {
                    break;
                }

                createRoomDir = Random.Range(0, 4);

                Vector2Int Pos = CurrentRoom.MyPos + dirVec[createRoomDir];

                if (!CheckCreateRoom(Pos))
                {
                    break;
                }

                GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                Room room = roomObj.GetComponent<Room>();

                room.roomtype = RoomType.Base;
                room.MyPos = Pos;

                roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                SettingNeighborRoom(room);

                listRooms.Add(room);
                connectReadyRooms.Add(room);

                createSuccess = true;

                if (createSuccess)
                {
                    roomNum++;
                    i++;

                    if (NeighborRoomCount(CurrentRoom) >= attachCount)
                    {
                        break;
                    }
                }

                // 안에도 넣어야 붙이는 방을 만들때 총 만들어진 카운트가 createRoomCount를 안넘긴다 => 안쓰면 오류남
                if (createRoomCount <= listRooms.Count || createRoomCount <= roomNum)
                {
                    break;
                }
            }

            if (createRoomCount <= listRooms.Count || createRoomCount <= roomNum)
            {
                break;
            }

            if (createSuccess)
            {
                connectReadyRooms.Remove(CurrentRoom);
            }

            index = Random.Range(0, connectReadyRooms.Count);

            CurrentRoom = connectReadyRooms[index];
        }

        connectReadyRooms.Clear();
    }

    /// <summary>
    /// 보스 방을 만드는 함수
    /// </summary>
    private void CreateBossRoom()
    {
        GameObject bossRoomObj = Instantiate(bossRoomPrefab, transform);

        Room room = bossRoomObj.GetComponent<Room>();

        Room lastRoom = listRooms[listRooms.Count - 1];

        room.MyPos = lastRoom.MyPos;
        room.roomtype = RoomType.Boss;

        bossRoomObj.name = $"BossRoom_({room.MyPos.x}, {room.MyPos.y})";
        
        listRooms.Remove(lastRoom);

        Destroy(lastRoom.gameObject);

        listRooms.Add(room);

        SettingNeighborRoom(room);

        bossRoom = room;
    }

    /// <summary>
    /// 보스방을 다른방과 하나만 연결시키는 함수
    /// </summary>
    private void RefreshBossRoom()
    {
        Room bossRoom = listRooms[listRooms.Count - 1];

        List<DoorType> connectDoors = new List<DoorType>();

        foreach(Door one in bossRoom.doors)
        {
            if(one != null && one.DoorType != DoorType.None)
            {
                connectDoors.Add(one.DoorType);
            }
        }

        while(connectDoors.Count > 1)
        {
            DoorType doorDir = connectDoors[Random.Range(0, connectDoors.Count)];
            switch (doorDir)
            {
                case DoorType.Up:
                    bossRoom.upRoom.downRoom = null;
                    bossRoom.upRoom.doors[1].DoorType = DoorType.None;
                    bossRoom.upRoom = null;
                    bossRoom.doors[0].DoorType = DoorType.None;
                    break;

                case DoorType.Down:
                    bossRoom.downRoom.upRoom = null;
                    bossRoom.downRoom.doors[0].DoorType = DoorType.None;
                    bossRoom.downRoom = null;
                    bossRoom.doors[1].DoorType = DoorType.None;
                    break;

                case DoorType.Left:
                    bossRoom.leftRoom.rightRoom = null;
                    bossRoom.leftRoom.doors[3].DoorType = DoorType.None;
                    bossRoom.leftRoom = null;
                    bossRoom.doors[2].DoorType = DoorType.None;
                    break;

                case DoorType.Right:
                    bossRoom.rightRoom.leftRoom = null;
                    bossRoom.rightRoom.doors[2].DoorType = DoorType.None;
                    bossRoom.rightRoom = null;
                    bossRoom.doors[3].DoorType = DoorType.None;
                    break;

                case DoorType.None:
                default:
                    Debug.LogError("수식 오류!!");
                    break;
            }

            connectDoors.Remove(doorDir);
        }
    }

    /// <summary>
    /// 생성할 방의 주변 방들이 조건에 맞는 방인지 확인하는 함수
    /// </summary>
    /// <param name="Pos">생성할 방의 그리드 위치</param>
    /// <returns>생성 가능 여부 조건(true면 생성 가능, false면 생성 불가능)</returns>
    private bool CheckCreateRoom(Vector2Int pos)
    {
        bool result = true;

        Room findRoom = FindRoom(pos);
        if (findRoom != null)
        {
            return false;
        }

        for (int i = 0; i < dirVec.Length; i++)
        {
            findRoom = FindRoom(pos + dirVec[i]);
            if (findRoom != null)
            {
                if(NeighborRoomCount(findRoom) >= maxAttachRoomCount)
                {
                    return false;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 현재 방에 연결된 다른 방의 개수
    /// </summary>
    /// <param name="room">확인할 방</param>
    /// <returns>연결된 방의 개수</returns>
    private int NeighborRoomCount(Room room)
    {
        int result = 0;

        if (room.leftRoom != null)
        {
            result++;
        }

        if (room.rightRoom != null)
        {
            result++;
        }

        if (room.upRoom != null)
        {
            result++;
        }

        if (room.downRoom != null)
        {
            result++;
        }

        return result;
    }
    
    /// <summary>
    /// 방들의 위치를 초기화 하는 함수
    /// </summary>
    private void RefreshRoomPosition()
    {
        foreach (Room room in listRooms)
        {
            room.RefreshPosition();
            room.RefreshDoor();
        }
    }

    /// <summary>
    /// 방의 상하좌우에 있는 방의 정보를 세팅하는 함수
    /// </summary>
    /// <param name="room">세팅할 방</param>
    private void SettingNeighborRoom(Room room)
    {
        Room findRoom = null;
        findRoom = FindRoom(room.MyPos + dirVec[0]);
        if (findRoom != null)
        {
            room.upRoom = findRoom;
            findRoom.downRoom = room;
        }

        findRoom = null;
        findRoom = FindRoom(room.MyPos + dirVec[1]);
        if (findRoom != null)
        {
            room.downRoom = findRoom;
            findRoom.upRoom = room;
        }

        findRoom = null;
        findRoom = FindRoom(room.MyPos + dirVec[2]);
        if (findRoom != null)
        {
            room.leftRoom = findRoom;
            findRoom.rightRoom = room;
        }

        findRoom = null;
        findRoom = FindRoom(room.MyPos + dirVec[3]);
        if (findRoom != null)
        {
            room.rightRoom = findRoom;
            findRoom.leftRoom = room;
        }
    }

    /// <summary>
    /// 그리드 위치의 방이 생성되었는지 확인하는 함수
    /// </summary>
    /// <param name="pos">확인할 그리드 좌표</param>
    /// <returns>찾는 값 반환, 없으면 null</returns>
    private Room FindRoom(Vector2Int pos)
    {
        return listRooms.Find(serchRoom => serchRoom.MyPos.x == pos.x && serchRoom.MyPos.y == pos.y);
    }
}