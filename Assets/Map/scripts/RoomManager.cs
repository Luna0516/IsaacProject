using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public bool isLoading = true;

    int roomNum = 0;
    int createRoomCount = 0;
    
    public int minCreateRoomCount = 3;
    public int maxCreateRoomCount = 12;

    public int minAttachRoomCount = 2;
    public int maxAttachRoomCount = 3;

    public GameObject startRoomPrefab;
    public GameObject baseRoomPrefab;
    public GameObject bossRoomPrefab;

    Room currentRoom = null;
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

    List<Room> listRooms = new List<Room>();

    public System.Action<Room> onChangeRoom;

    protected override void OnInitialize()
    {
        isLoading = true;

        base.OnInitialize();

        createRoomCount = Random.Range(minCreateRoomCount, maxCreateRoomCount);

        CreateStartRoom();

        CreateOtherRoom();

        RefreshRoomPosition();

        isLoading = false;

        CurrentRoom = listRooms[0];
    }

    public void CreateStartRoom()
    {
        GameObject startRoomObj = Instantiate(startRoomPrefab, transform);

        Room startRoom = startRoomObj.GetComponent<Room>();

        startRoom.MyPos = Vector2Int.zero;
        startRoom.roomtype = RoomType.Start;

        startRoomObj.name = $"StartRoom_({startRoom.MyPos.x}, {startRoom.MyPos.y})";

        CurrentRoom = startRoom;
        listRooms.Add(startRoom);
        roomNum++;
        index++;

    }

    int index = 0;

    public void CreateOtherRoom()
    {
        while (true)
        {
            if (roomNum >= createRoomCount)
            {
                break;
            }

            int attachCount = Random.Range(minAttachRoomCount, maxAttachRoomCount + 1);

            for (int i = 0; i < attachCount;)
            {
                if (NeighborRoomCount(CurrentRoom) >= attachCount)
                {
                    break;
                }

                bool createSuccess = false;
                int createDir = Random.Range(0, 4);

                switch (createDir)
                {
                    case 0:
                        if (CurrentRoom.leftRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.left;

                            if (FindRoom(Pos))
                            {
                                i++;
                            }
                            else
                            {
                                GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                                Room room = roomObj.GetComponent<Room>();

                                room.MyPos = Pos;
                                room.roomtype = RoomType.Base;
                                room.rightRoom = CurrentRoom;

                                roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                                CurrentRoom.leftRoom = room;
                                listRooms.Add(room);

                                createSuccess = true;
                            }
                        }
                        break;
                    case 1:
                        if (CurrentRoom.rightRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.right;

                            if (FindRoom(Pos))
                            {
                                i++;
                            }
                            else
                            {
                                GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                                Room room = roomObj.GetComponent<Room>();

                                room.MyPos = Pos;
                                room.roomtype = RoomType.Base;
                                room.leftRoom = CurrentRoom;

                                roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                                CurrentRoom.rightRoom = room;
                                listRooms.Add(room);

                                createSuccess = true;
                            }
                        }
                        break;
                    case 2:
                        if (CurrentRoom.topRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.up;

                            if (FindRoom(Pos))
                            {
                                i++;
                            }
                            else
                            {
                                GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                                Room room = roomObj.GetComponent<Room>();

                                room.MyPos = Pos;
                                room.roomtype = RoomType.Base;
                                room.bottomRoom = CurrentRoom;

                                roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                                CurrentRoom.topRoom = room;
                                listRooms.Add(room);

                                createSuccess = true;
                            }
                        }
                        break;
                    case 3:
                        if (CurrentRoom.bottomRoom == null)
                        {
                            Vector2Int Pos = CurrentRoom.MyPos + Vector2Int.down;

                            if (FindRoom(Pos))
                            {
                                i++;
                            }
                            else
                            {
                                GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                                Room room = roomObj.GetComponent<Room>();

                                room.MyPos = Pos;
                                room.roomtype = RoomType.Base;
                                room.topRoom = CurrentRoom;

                                roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                                CurrentRoom.bottomRoom = room;
                                listRooms.Add(room);

                                createSuccess = true;
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (createSuccess)
                {
                    i++;
                }
                else
                {
                    continue;
                }

                roomNum++;
                //Debug.Log($"현재까지 생성한 방의 개수 : {roomNum}");
            }

            if (index == listRooms.Count)
            {
                Debug.LogWarning("수치 오류!");
                index = Random.Range(0, listRooms.Count - 1);
            }

            index = Random.Range(index, listRooms.Count);

            //Debug.Log($"index : {index}");

            CurrentRoom = listRooms[index];
        }
    }

    private void RefreshRoomPosition()
    {
        for(int i = 0; i< listRooms.Count; i++)
        {
            float xPos = listRooms[i].MyPos.x * listRooms[i].width;
            float yPos = listRooms[i].MyPos.y * listRooms[i].height;

            listRooms[i].transform.position += new Vector3(xPos, yPos);

            SettingNeighborRoom(listRooms[i]);

            RefreshDoor(listRooms[i]);
        }
    }

    private bool FindRoom(Vector2Int pos)
    {
        bool result = false;

        Room room = listRooms.Find(room => room.MyPos.x == pos.x && room.MyPos.y == pos.y);

        if(room != null)
        {
            result = true;
        }

        return result;
    }

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
    /// 방에 연결된 다른 방의 개수
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

    private void SettingNeighborRoom(Room room)
    {
        Room findRoom;

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x - 1 && serchRoom.MyPos.y == room.MyPos.y);
        if(findRoom != null)
        {
            if(room.leftRoom == null)
            {
                room.leftRoom = findRoom;
            }
        }

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x + 1 && serchRoom.MyPos.y == room.MyPos.y);
        if (findRoom != null)
        {
            if (room.rightRoom == null)
            {
                room.rightRoom = findRoom;
            }
        }

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x && serchRoom.MyPos.y == room.MyPos.y + 1);
        if (findRoom != null)
        {
            if (room.topRoom != null)
            {
                room.topRoom = findRoom;
            }
        }

        findRoom = listRooms.Find(serchRoom => serchRoom.MyPos.x == room.MyPos.x && serchRoom.MyPos.y == room.MyPos.y - 1);
        if (findRoom != null)
        {
            if (room.bottomRoom == null)
            {
                room.bottomRoom = findRoom;
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
