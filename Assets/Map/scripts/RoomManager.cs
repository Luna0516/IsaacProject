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
    /// 상점 방의 프리펩
    /// </summary>
    public GameObject shopRoomPrefab;

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

        if(CurrentRoom.roomtype == RoomType.Start)
        {
            CurrentRoom.OpenDoor();
        }
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

                if (NeighborRoomCount(CurrentRoom) >= attachCount)
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
                        }
                        break;
                    case 1:
                        if (CurrentRoom.rightRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.right;

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
                        }
                        break;
                    case 2:
                        if (CurrentRoom.topRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.up;

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
                        }
                        break;
                    case 3:
                        if (CurrentRoom.bottomRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.down;

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
                        }
                        break;
                    default:
                        break;
                }

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
    /// 생성할 방의 주변 방들이 조건에 맞는 방인지 확인하는 함수
    /// </summary>
    /// <param name="Pos">생성할 방의 그리드 위치</param>
    /// <returns>생성 가능 여부 조건(true면 생성 가능, false면 생성 불가능)</returns>
    private bool CheckCreateRoom(Vector2Int pos)
    {
        bool result = true;

        Vector2Int[] checkDir = new Vector2Int[4] { Vector2Int.left, Vector2Int.right, Vector2Int.down, Vector2Int.up };

        for (int i = 0; i < checkDir.Length; i++)
        {
            Room findRoom = FindRoom(pos + checkDir[i]);
            if (findRoom != null)
            {
                if(NeighborRoomCount(findRoom) >= maxAttachRoomCount)
                {
                    result = false;
                    break;
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

        if (room.topRoom != null)
        {
            result++;
        }

        if (room.bottomRoom != null)
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
