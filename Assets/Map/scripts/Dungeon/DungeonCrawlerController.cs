using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// 방향 지정을 위한 Direction 자료형
/// </summary>
public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3

};

public class DungeonCrawlerController : MonoBehaviour
{
    //총평 : 랜덤 방을 생성해내는걸 관리하는 클래스인듯 하다.


    /// <summary>
    /// 정적변수 positionsVisited 리스트 생성
    /// </summary>
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    /// <summary>
    /// 맵의 방향을 저장하는 딕셔네리 생성
    /// </summary>
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        { Direction.up, Vector2Int.up},
        { Direction.left, Vector2Int.left},
        { Direction.down, Vector2Int.down},
        { Direction.right, Vector2Int.right}
    };

    /// <summary>
    /// 정적함수 GenerateDungeon 던전 생성 함수인듯
    /// </summary>
    /// <param name="dungeonData"></param>
    /// <returns></returns>
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();//던전 크라울러 리스트 생성
        for(int i = 0; i < dungeonData.numberOfCrawlers; i++)//던전 크라울러 갯수만큼 반복
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));//리스트에 던전 크라울러의 위치를 0,0,0으로 초기화해서 추가
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);//반복 횟수


        for(int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)//총 몬스터 수 만큼 반복
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);//다음 방의 방향값을 newPos에 저장한다.
                positionsVisited.Add(newPos);//위치값을 리스트에 저장
            }
        }

        return positionsVisited;//그 리스트를 리턴
    }

}
