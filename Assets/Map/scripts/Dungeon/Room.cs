using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Room : MonoBehaviour
{
    /// <summary>
    /// 방의 가로값
    /// </summary>
    public int Width;

    /// <summary>
    /// 방의 세로값
    /// </summary>
    public int Height;


    /// <summary>
    /// X
    /// </summary>
    public int X;

    /// <summary>
    /// Y
    /// </summary>
    public int Y;


    /// <summary>
    /// 방 문의 참값
    /// </summary>
    private bool updatedDoors = false;

    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="x"> X 변수에 int x 대입</param>
    /// <param name="y"> Y 변수에 int y 대입</param>
    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// 왼쪽 문 Door 클래스
    /// </summary>
    public Door leftDoor;

    /// <summary>
    /// 오른쪽 문 Door 클래스
    /// </summary>
    public Door rightDoor;

    /// <summary>
    /// 윗쪽 문 Door 클래스
    /// </summary>
    public Door topDoor;

    /// <summary>
    /// 아랫쪽 문 Door 클래스
    /// </summary>
    public Door bottomDoor;

    /// <summary>
    /// Door 클래스 List doors
    /// </summary>
    public List<Door>doors = new List<Door>();

    private void Awake()
    {
        
    }

    void Start()
    {
        if (RoomController.instance == null)//룸 컨트롤러가 없으면 
        {
            Debug.Log("You preesed play in the  wrong scene!");
            return; //if문 정지
        }

        Door[] ds = GetComponentsInChildren<Door>();//자식 개체의 Door 컴포넌트들을 모아서 ds 배열에 저장
        foreach(Door d in ds) //ds만큼 Door 클래스의 d를 반복
        {
            doors.Add(d);//리스트에 d 입력
            switch (d.doorType)//d 
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        } 

        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch(door.doorType)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        } 
    }

    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;

    }

    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3( X * Width, Y * Height);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
