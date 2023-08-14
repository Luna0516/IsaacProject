using UnityEngine;

[CreateAssetMenu(fileName ="DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]

public class DungeonGenerationData : ScriptableObject
{
    //총평 : 방의 생성에 대한 데이터를 담는 스크립터블 오브젝트


    public int numberOfCrawlers; //방의 총 수

    public int iterationMin; // 반복횟수 지정용

    public int iterationMax; // 반복횟수 지정용
}
