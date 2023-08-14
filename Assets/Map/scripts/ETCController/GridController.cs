using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridController;

public class GridController : MonoBehaviour
{
    //총평 : 지정한 Room 사이즈 만큼 그리드를 자동으로 생성하는 클래스인것 같습니다.




    /// <summary>
    /// Room 클래스의 room 변수
    /// </summary>
    public Room room;



    [System.Serializable]
   public struct Grid //Grid 구조체
    {
        public int columns, rows; //columns, rows int 변수 생성

        public float verticalOffset, horizontalOffset; //verticalOffset, horizontalOffest float 변수 생성
    }

    public Grid grid; // 구조체 재정의

    //public GameObject gridTile; //gridTile 게임 오브젝트 변수 instantiate용(제작용 견본)

    public List<Vector2> availablePoints = new List<Vector2>(); //availablePoint라는 Vector2 배열

    void Awake()
    {
        //grid=GetComponentInParent<Grid>();//P.S
        room = GetComponentInParent<Room>(); //부모 개체에서 Room 클래스 가져오기
        grid.columns = room.Width - 2; //방의 가로 길이 -2
        grid.rows = room.Height - 2; //방의 세로 길이 -2
        //GenerateGrid(); 
    }

    /// <summary>
    /// 그리드 타일 생성용 함수인듯 합니다. 필요 없을것같아요
    /// </summary>
/*    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset),
                    y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Y:" + y;
                availablePoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }*/
}
