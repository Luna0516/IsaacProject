using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCrawler : MonoBehaviour
{
    //총평 : 아마도 방이 생성 될 때의 위 아래 좌 우 어디로 방을 생성할것인지 정하는것 같다.


    /// <summary>
    /// Vector2Int 타입의 Position 프로퍼티
    /// </summary>
    public Vector2Int Position { get; set; }

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="startPos">Positino 프로퍼티 초기화인듯</param>
    public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="directionMovementMap"></param>
    /// <returns></returns>
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);//0에서 딕셔너리 수 사이 랜덤값 추출 이걸 Direction형으로 변환 변환
        Position += directionMovementMap[toMove];//position에 이동 값이 나온만큼 더해준다.
        return Position;//position 리턴
    }
}
