using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //총평 : 이 클래스는 플레이어의 위치에 따라 방의 값을 받아 그 방의 값의 중심으로 카메라를 이동한다.(이 클래스는 카메라에 들어갑니다.)



    /// <summary>
    /// 정적 변수에 CameraController 타입의 instance 변수 선언
    /// </summary>
    public static CameraController instance;

    /// <summary>
    /// Room 클래스의 currRomm 변수 선언
    /// </summary>
    public Room currRoom;

    /// <summary>
    /// moveSpeedWhenRoomChange 라는 실수형 변수 선언(아마도 카메라 이동 속도 추정)
    /// </summary>
    public float moveSpeedWhenRoomChange;

    void Awake()
    {

        //instance 변수에 CameraController가 들어있는 개체의 CameraController 클래스 대입
        instance = this;
        
    }
     

    void Update()
    {
        UpdatePosition();//카메라의 이동과 방의 변화를 책임지는 함수
    }

    /// <summary>
    /// 카메라의 이동과 방의 변화를 책임지는 함수 Update에서 실행
    /// </summary>
    void UpdatePosition()
    {
        if(currRoom == null)//방이 null일때
        {
            return;//함수 종료
        }

        Vector3 targetPos = GetCameraTargetPosition();//다음 목표를 구하는 함수

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);//이 개체의 위치를 연결된 값 만큼 이동합니다.(이 개체를, 목표로 , 속도의 값 만큼)
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)//방이 없으면
        {
            return Vector3.zero;//0,0,0으로 이동
        }

        Vector3 targetPos = currRoom.GetRoomCentre();//Vector3값에 방의 중심값을 구하는 함수를 실행하는듯 하다.
        targetPos.z = transform.position.z;//타깃의 z값은 이 개체의 z값이다.

        return targetPos; //구한값을 out해준다.
    }


    /// <summary>
    /// 사용하지 않는 함수
    /// </summary>
    /// <returns></returns>
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
