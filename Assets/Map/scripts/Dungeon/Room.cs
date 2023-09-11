using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Room : MonoBehaviour
{
    Vector2Int myPos = Vector2Int.zero;

    public Vector2Int MyPos
    {
        get => myPos;
        set
        {
            if (myPos == Vector2Int.zero)
            {
                myPos = value;
            }
        }
    }

    public Room(Vector2Int vec)
    {
        MyPos = vec;
    }

    public int width;
    public int height;

    private bool updatedDoors = false;

    public Door leftDoor;

    public Door rightDoor;

    public Door topDoor;

    public Door bottomDoor;

    public List<Door>doors = new List<Door>();

    void Start()
    {
        if (RoomManager.Inst == null)
        {
            Debug.Log("You preesed play in the  wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
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

        RoomManager.Inst.RegisterRoom(this);
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

    Room GetRight()
    {
        Room result = null;

        Vector2Int rightRoomPos = MyPos + Vector2Int.right;

        if (RoomManager.Inst.DoesRoomExist(rightRoomPos))
        {
            result = RoomManager.Inst.FindRoom(rightRoomPos);
        }

        return result;
    }

    Room GetLeft()
    {
        Room result = null;

        Vector2Int leftRoomPos = MyPos + Vector2Int.left;

        if (RoomManager.Inst.DoesRoomExist(leftRoomPos))
        {
            result = RoomManager.Inst.FindRoom(leftRoomPos);
        }

        return result;
    }

    Room GetTop()
    {
        Room result = null;

        Vector2Int upRoomPos = MyPos + Vector2Int.up;

        if (RoomManager.Inst.DoesRoomExist(upRoomPos))
        {
            result = RoomManager.Inst.FindRoom(upRoomPos);
        }

        return result;
    }

    Room GetBottom()
    {
        Room result = null;

        Vector2Int downRoomPos = MyPos + Vector2Int.down;

        if (RoomManager.Inst.DoesRoomExist(downRoomPos))
        {
            result = RoomManager.Inst.FindRoom(downRoomPos);
        }

        return result;
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3( myPos.x * width, myPos.y * height);
    }
}
