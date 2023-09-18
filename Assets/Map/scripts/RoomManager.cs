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
    /// 시작 방의 프리펩
    /// </summary>
    public GameObject startRoomPrefab;

    /// <summary>
    /// 기본 방의 프리펩
    /// </summary>
    public GameObject baseRoomPrefab;

    /// <summary>
    /// 보스 방의 프리펩
    /// </summary>
    public GameObject bossRoomPrefab;

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
                    currentRoom.IsVisit = true;
                    onChangeRoom?.Invoke(currentRoom);
                }
            }
        }
    }

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
        isLoading = true;

        createRoomCount = Random.Range(minCreateRoomCount, maxCreateRoomCount + 1);

        CreateStartRoom();

        CreateBaseRoom();

        connectReadyRooms.Clear();

        RefreshRoomPosition();

        isLoading = false;

        CurrentRoom = listRooms[0];
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
            attachCount = Random.Range(minAttachRoomCount, maxAttachRoomCount + 1);

            for (int i = 0; i < attachCount; )
            {
                createSuccess = false;

                if (NeighborRoomCount() >= attachCount)
                {
                    break;
                }

                createRoomDir = Random.Range(0, 4);

                switch (createRoomDir)
                {
                    case 0:
                        if (CurrentRoom.leftRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.left;

                            GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                            Room room = roomObj.GetComponent<Room>();

                            room.roomtype = RoomType.Base;
                            room.MyPos = Pos;

                            roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                            SettingNeighborRoom(room);

                            listRooms.Add(room);
                            connectReadyRooms.Add(room);

                            createSuccess = true;
                        }
                        break;
                    case 1:
                        if (CurrentRoom.rightRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.right;

                            GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                            Room room = roomObj.GetComponent<Room>();

                            room.roomtype = RoomType.Base;
                            room.MyPos = Pos;

                            roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                            SettingNeighborRoom(room);

                            listRooms.Add(room);
                            connectReadyRooms.Add(room);

                            createSuccess = true;
                        }
                        break;
                    case 2:
                        if (CurrentRoom.topRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.up;

                            GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                            Room room = roomObj.GetComponent<Room>();

                            room.roomtype = RoomType.Base;
                            room.MyPos = Pos;

                            roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                            SettingNeighborRoom(room);

                            listRooms.Add(room);
                            connectReadyRooms.Add(room);

                            createSuccess = true;
                        }
                        break;
                    case 3:
                        if (CurrentRoom.bottomRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.down;

                            // 만들어진 방의 4방향에 있는 방이 maxAttach값을 넘는지 확인
                            // or 랜덤으로 붙는 값을 방이 만들어 질때 위에서 그 방에 넣고 확인 => 어차피 만들어 지다가 총 만들 방의 개수가 되면 자동으로 그 방에 붙는 값을 자신 주변에 있는 값으로 바꾸기

                            GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                            Room room = roomObj.GetComponent<Room>();

                            room.roomtype = RoomType.Base;
                            room.MyPos = Pos;

                            roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                            SettingNeighborRoom(room);

                            listRooms.Add(room);
                            connectReadyRooms.Add(room);

                            createSuccess = true;
                        }
                        break;
                    default:
                        break;
                }

                if (createSuccess)
                {
                    roomNum++;
                    i++;

                    if (NeighborRoomCount() >= attachCount)
                    {
                        break;
                    }
                }

                // 안에도 넣어야 붙이는 방을 만들때 총 만들어진 카운트가 createRoomCount를 안넘긴다 => 안쓰면 오류남
                if (createRoomCount == listRooms.Count || createRoomCount == roomNum)
                {
                    break;
                }
            }

            if(createRoomCount == listRooms.Count || createRoomCount == roomNum)
            {
                break;
            }

            if (createSuccess)
            {
                connectReadyRooms.RemoveAt(index);
            }

            index = Random.Range(0, connectReadyRooms.Count);

            CurrentRoom = connectReadyRooms[index];
        }
    }

    /// <summary>
    /// 현재 방에 연결된 다른 방의 개수
    /// </summary>
    /// <param name="room">확인할 방</param>
    /// <returns>연결된 방의 개수</returns>
    private int NeighborRoomCount()
    {
        int result = 0;

        if (CurrentRoom.leftRoom != null)
        {
            result++;
        }

        if (CurrentRoom.rightRoom != null)
        {
            result++;
        }

        if (CurrentRoom.topRoom != null)
        {
            result++;
        }

        if (CurrentRoom.bottomRoom != null)
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
        for (int i = 0; i < listRooms.Count; i++)
        {
            float xPos = listRooms[i].MyPos.x * listRooms[i].width;
            float yPos = listRooms[i].MyPos.y * listRooms[i].height;

            listRooms[i].transform.position += new Vector3(xPos, yPos);

            //SettingNeighborRoom(listRooms[i]);

            RefreshDoor(listRooms[i]);
        }
    }

    /// <summary>
    /// 방의 문의 활성화 상태를 변경할 함수
    /// </summary>
    /// <param name="room">문의 활성화 상태를 변경할 방</param>
    private void RefreshDoor(Room room)
    {
        if (room.leftRoom == null)
        {
            room.doors[0].gameObject.SetActive(false);
        }

        if (room.rightRoom == null)
        {
            room.doors[1].gameObject.SetActive(false);
        }

        if (room.topRoom == null)
        {
            room.doors[2].gameObject.SetActive(false);
        }

        if (room.bottomRoom == null)
        {
            room.doors[3].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 방의 상하좌우에 있는 방의 정보를 세팅하는 함수
    /// </summary>
    /// <param name="room">세팅할 방</param>
    private void SettingNeighborRoom(Room room)
    {
        Room findRoom;
        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x - 1 && serchRoom.MyPos.y == room.MyPos.y);
        if (findRoom != null)
        {
            if (room.leftRoom == null)
            {
                room.leftRoom = findRoom;
                findRoom.rightRoom = room;
            }
        }

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x + 1 && serchRoom.MyPos.y == room.MyPos.y);
        if (findRoom != null)
        {
            if (room.rightRoom == null)
            {
                room.rightRoom = findRoom;
                findRoom.leftRoom = room;
            }
        }

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x && serchRoom.MyPos.y == room.MyPos.y - 1);
        if (findRoom != null)
        {
            if (room.bottomRoom == null)
            {
                room.bottomRoom = findRoom;
                findRoom.topRoom = room;
            }
        }

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x && serchRoom.MyPos.y == room.MyPos.y + 1);
        if (findRoom != null)
        {
            if (room.topRoom == null)
            {
                room.topRoom = findRoom;
                findRoom.bottomRoom = room;
            }
        }
    }

    //----------------------------------------------------------------------
    // 테스트용 함수
    public void TestRemoveWallColl()
    {
        CurrentRoom.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
    }
}
