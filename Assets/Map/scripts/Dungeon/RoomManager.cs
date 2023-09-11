using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;

    public int X;

    public int Y;
}
public class RoomManager : Singleton<RoomManager>
{
    /// <summary>
    /// 현재 플레이어가 위치한 방
    /// </summary>
    Room currentRoom;
    /// <summary>
    /// 현재 플레이어가 위치한 방 설정용 프로퍼티
    /// </summary>
    public Room CurrentRoom
    {
        get => currentRoom;
        set
        {
            if (currentRoom != value)
            {
                currentRoom = value;
                onChangeRoom?.Invoke(currentRoom);
            }
        }
    }

    /// <summary>
    /// 플레이어의 방의 위치가 바뀌면 사용할 델리게이트
    /// </summary>
    public Action<Room> onChangeRoom;

    const string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if(!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if(spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms=true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.MyPos);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.MyPos.x == tempRoom.MyPos.x && r.MyPos.y == tempRoom.MyPos.y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.MyPos.x, tempRoom.MyPos.y);
        }

    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(new Vector2Int(x, y)))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(new Vector2Int(currentLoadRoomData.X, currentLoadRoomData.Y)))
        {
            room.transform.position = new Vector3(currentLoadRoomData.X * room.width, currentLoadRoomData.Y * room.height, 0);
            room.MyPos = new Vector2Int(currentLoadRoomData.X, currentLoadRoomData.Y);
            room.name = currentWorldName + "-" + currentLoadRoomData.name + "-" + room.MyPos.x + "," + room.MyPos.y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CurrentRoom = room;
            }


            loadedRooms.Add(room);
            
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }


    }

    public bool DoesRoomExist(Vector2Int searchRoom)
    {
        bool result = loadedRooms.Find(room => room.MyPos.x == searchRoom.x && room.MyPos.y == searchRoom.y);

        return result;
    }

    public Room FindRoom(Vector2Int findRoom)
    {
        Room result = null;

        result = loadedRooms.Find(room => room.MyPos.x == findRoom.x && room.MyPos.y == findRoom.y);

        return result;
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
            {
                "Empty",
                "Basic"
            };

        return possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Length)];
    }
}

