using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    int roomNum = 0;

    public GameObject baseRoomPrefab;

    Room currentRoom = null;
    public Room CurrentRoom
    {
        get => currentRoom;
        set
        {
            if(currentRoom != value)
            {
                currentRoom = value;
            }
        }
    }

    List<Room> listRooms = new List<Room>();

    public int minAttachRoomCount = 1;
    public int maxAttachRoomCount = 3;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        CreateStartRoom();

        CreateOtherRoom();

        RefreshRoomPosition();

        CurrentRoom = listRooms[0];
    }

    public void CreateStartRoom()
    {
        GameObject startRoomObj = Instantiate(baseRoomPrefab, transform);

        Room startRoom = startRoomObj.GetComponent<Room>();

        startRoom.MyPos = Vector2Int.zero;
        startRoom.roomtype = RoomType.Base;

        startRoomObj.name = $"StartRoom_({startRoom.MyPos.x}, {startRoom.MyPos.y})";

        CurrentRoom = startRoom;
        listRooms.Add(startRoom);
    }

    public void CreateOtherRoom()
    {
        int attachCount = Random.Range(minAttachRoomCount, maxAttachRoomCount + 1);

        bool createSuccess = false;

        for (int i = 0; i < attachCount; )
        {
            if(NeighborRoomCount(CurrentRoom) >= attachCount)
            {
                break;
            }

            createSuccess = false;
            int createDir = Random.Range(0, 4);

            switch (createDir)
            {
                case 0:
                    if(CurrentRoom.leftRoom == null)
                    {
                        GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                        Room room = roomObj.GetComponent<Room>();

                        room.MyPos = CurrentRoom.MyPos + Vector2Int.left;
                        room.roomtype = RoomType.Base;
                        room.rightRoom = CurrentRoom;

                        roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                        CurrentRoom.leftRoom = room;
                        listRooms.Add(room);

                        createSuccess = true;
                    }
                    break;
                case 1:
                    if (CurrentRoom.rightRoom == null)
                    {
                        GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                        Room room = roomObj.GetComponent<Room>();

                        room.MyPos = CurrentRoom.MyPos + Vector2Int.right;
                        room.roomtype = RoomType.Base;
                        room.leftRoom = CurrentRoom;

                        roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                        CurrentRoom.rightRoom = room;
                        listRooms.Add(room);

                        createSuccess = true;
                    }
                    break;
                case 2:
                    if (CurrentRoom.topRoom == null)
                    {
                        GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                        Room room = roomObj.GetComponent<Room>();
                        
                        room.MyPos = CurrentRoom.MyPos + Vector2Int.up;
                        room.roomtype = RoomType.Base;
                        room.bottomRoom = CurrentRoom;

                        roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                        CurrentRoom.topRoom = room;
                        listRooms.Add(room);

                        createSuccess = true;
                    }
                    break;
                case 3:
                    if (CurrentRoom.bottomRoom == null)
                    {
                        GameObject roomObj = Instantiate(baseRoomPrefab, transform);

                        Room room = roomObj.GetComponent<Room>();

                        room.MyPos = CurrentRoom.MyPos + Vector2Int.down;
                        room.roomtype = RoomType.Base;
                        room.topRoom = CurrentRoom;

                        roomObj.name = $"BaseRoom_({room.MyPos.x}, {room.MyPos.y})";

                        CurrentRoom.bottomRoom = room;
                        listRooms.Add(room);

                        createSuccess = true;
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
        }

        roomNum++;
    }

    private void RefreshRoomPosition()
    {
        for(int i = 0; i< listRooms.Count; i++)
        {
            float xPos = listRooms[i].MyPos.x * listRooms[i].width;
            float yPos = listRooms[i].MyPos.y * listRooms[i].height;

            listRooms[i].transform.position += new Vector3(xPos, yPos);

            RefreshDoor(listRooms[i]);
        }
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
}
