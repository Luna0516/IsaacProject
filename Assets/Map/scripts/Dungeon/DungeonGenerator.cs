using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    /// <summary>
    /// DungeonGenerationData 타입의 dungeonGenerationData변수 지정
    /// </summary>
    public DungeonGenerationData dungeonGenerationData;

    /// <summary>
    /// 던전 방의 좌표값을 리스트에 저장
    /// </summary>
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);//던전 룸 좌표값 리스트를 dungeonRooms리스트에 하달
    }


    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
}
