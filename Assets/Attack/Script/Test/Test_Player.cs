using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Player : MonoBehaviour
{
    [Header("총알 프리펩")]
    public GameObject bulletPrefab;

    [Header("총알이 아닌 칼인지 확인")]
    public bool IsKnife = false;

    /// <summary>
    /// 총알 발사 위치
    /// </summary>
    Vector2 firePos;

    /// <summary>
    /// 이동 방향 입력값
    /// </summary>
    Vector2 moveInputVec;

    /// <summary>
    /// 총알 발사 입력값
    /// </summary>
    Vector2 fireInputVec;

    /// <summary>
    /// 칼 오브젝트 전역 변수
    /// </summary>
    GameObject knife = null;

    AttackInputActions inputActions;

    private void Awake()
    {
        inputActions = new AttackInputActions();

        // 변수 초기화
        firePos = Vector2.zero;
        moveInputVec = Vector2.zero;
        fireInputVec = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Move.performed += OnMove;
        inputActions.Test.Move.canceled += OnMove;
        inputActions.Test.FireV.performed += OnFireV;
        inputActions.Test.FireV.canceled += OnFireV;
        inputActions.Test.FireH.performed += OnFireH;
        inputActions.Test.FireH.canceled += OnFireH;

        if (IsKnife)
        {
            knife = Instantiate(bulletPrefab);
            knife.transform.position = transform.position + Vector3.down;
            knife.transform.localRotation = Quaternion.Euler(0, 0, 180.0f);
        }
    }

    private void OnDisable()
    {
        inputActions.Test.FireH.canceled -= OnFireH;
        inputActions.Test.FireH.performed -= OnFireH;
        inputActions.Test.FireV.canceled -= OnFireV;
        inputActions.Test.FireV.performed -= OnFireV;
        inputActions.Test.Move.canceled -= OnMove;
        inputActions.Test.Move.performed -= OnMove;
        inputActions.Test.Disable();
    }

    /// <summary>
    /// 이동 입력값 확인 함수 ( 이동은 안됩니다.)
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInputVec = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 총알 발사 입력값 확인 함수 (상하)
    /// </summary>
    private void OnFireV(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fireInputVec.y = context.ReadValue<float>();

            if (fireInputVec.x != 0)
            {
                fireInputVec.y = 0;
            }

            // 총알 발사 위치 조정
            firePos = (Vector2)transform.position + (fireInputVec * 0.5f);

            // 일단 한번 누를때 한발씩만 발사 되게 만들었습니다.
            Fire();
        }
        else
        {
            fireInputVec.y = context.ReadValue<float>();
        }
    }

    /// <summary>
    /// 총알 발사 입력값 확인 함수 (좌우)
    /// </summary>
    private void OnFireH(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fireInputVec.x = context.ReadValue<float>();

            if (fireInputVec.y != 0)
            {
                fireInputVec.y = 0;
            }

            // 총알 발사 위치 조정
            firePos = (Vector2)transform.position + (fireInputVec * 0.5f);

            // 일단 한번 누를때 한발씩만 발사 되게 만들었습니다.
            Fire();
        }
        else
        {
            fireInputVec.x = context.ReadValue<float>();
        }
    }

    /// <summary>
    /// 총알 발사 함수 ( Factory로 만드는게 아닌 Instantiate로 총알을 만들어서 발사합니다.)
    /// </summary>
    private void Fire()
    {
        if (!IsKnife)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            bullet.transform.position = firePos;

            AttackBase attackBase = bullet.GetComponent<AttackBase>();

            if (attackBase == null)
            {
                Debug.LogWarning("이 총알 프리펩은 AttackBase를 상속받지 않았습니다.");
                return;
            }

            attackBase.speed = 5.0f;
            attackBase.lifeTime = 5.0f;       // 총알 사거리 (지속시간)
            attackBase.rangeToLife = 2.5f;    // 플레이어 눈물 사거리 lifeTime(초)으로 나눌 수치 
            attackBase.dropDuration = 10.0f;  // 밑으로 떨어지는 시간
            attackBase.startGravity = 0.8f;   // 중력적용 시점
            attackBase.gravityScale = 3.0f;   // 중력 정도

            attackBase.moveDir = moveInputVec;
            attackBase.dir = fireInputVec;
        }
        else
        {
            // 총알 프리펩이 칼이면 실행
            if (moveInputVec == Vector2.zero)
            {
                bulletPrefab.transform.position = transform.position + ((Vector3)fireInputVec * 0.7f);
            }
        }
    }
}