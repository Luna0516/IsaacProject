using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    /// <summary>
    /// 다음에 로딩할 로드 씬의 이름
    /// </summary>
    string nextLoadSceneName = "LoadScene";

    /// <summary>
    /// 배경화면 움직이고 있는지 확인용 변수
    /// </summary>
    bool isMoveScene = false;

    /// <summary>
    /// x 화면 크기 고정값
    /// </summary>
    const int sceneWidth = 1920;
    /// <summary>
    /// y 화면 크기 고정값
    /// </summary>
    const int sceneHeight = 1080;

    /// <summary>
    /// 배경화면 번호
    /// </summary>
    int sceneNum = 0;
    /// <summary>
    /// 배경화면 번호 프로퍼티
    /// </summary>
    int SceneNum
    {
        get => sceneNum;
        set
        {
            if (sceneNum != value)
            {
                currentScene = sceneNum;

                if(value == 4)
                {
                    Debug.Log("GameLoadScene Gogo!!");
                }

                sceneNum = Math.Clamp(value, 0, 3);

                StartCoroutine(ChangeBackground(currentScene, sceneNum));
            }
        }
    }

    /// <summary>
    /// 현재 배경화면 번호 (저장용)
    /// </summary>
    int currentScene = 0;

    /// <summary>
    /// 배경화면 이동 속도
    /// </summary>
    float moveSpeed = 3.5f;

    // x, y 움직임 고정 벡터
    Vector2 xMove = new Vector2(sceneWidth, 0);
    Vector2 yMove = new Vector2(0, sceneHeight);

    RectTransform rectTR;

    UIInputActions inputActions;

    private void Awake()
    {
        rectTR = GetComponent<RectTransform>();

        inputActions = new UIInputActions();
    }

    private void OnEnable()
    {
        inputActions.Start.Enable();
        inputActions.Start.Press.performed += OnPress;
        inputActions.Start.Back.performed += OnBack;
        inputActions.Start.Move.performed += OnSelectArrow;
    }

    private void OnDisable()
    {
        inputActions.Start.Move.performed -= OnSelectArrow;
        inputActions.Start.Back.performed -= OnBack;
        inputActions.Start.Press.performed -= OnPress;
        inputActions.Start.Disable();
    }

    private void OnPress(InputAction.CallbackContext obj)
    {
        if (!isMoveScene)
        {
            SceneNum += 1;
        }
    }

    private void OnBack(InputAction.CallbackContext obj)
    {
        if (!isMoveScene)
        {
            SceneNum -= 1;
        }
    }

    private void OnSelectArrow(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 배경화면 이동 코루틴
    /// </summary>
    /// <param name="start">이동 시작 지점</param>
    /// <param name="end">이동 종료 지점</param>
    IEnumerator ChangeBackground(int start, int end)
    {
        if (!isMoveScene)
        {
            isMoveScene = true;

            int moveDir = end - start;  // -면 뒤로, +면 앞으로 이동

            switch (end)
            {
                case 0:
                    while ((rectTR.anchoredPosition.y - (sceneHeight * end)) >= 0.001f)
                    {
                        rectTR.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                        yield return null;
                    }

                    rectTR.anchoredPosition = yMove * end;
                    break;

                case 1:
                    if (moveDir > 0)
                    {
                        while (((sceneHeight * end) - rectTR.anchoredPosition.y) >= 0.001f)
                        {
                            rectTR.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                            yield return null;
                        }
                    }
                    else
                    {
                        while ((rectTR.anchoredPosition.y - (sceneHeight * end)) >= 0.001f)
                        {
                            rectTR.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                            yield return null;
                        }
                    }

                    rectTR.anchoredPosition = yMove * end;
                    break;

                case 2:
                    if (moveDir > 0)
                    {
                        while (((sceneHeight * end) - rectTR.anchoredPosition.y) >= 0.001f)
                        {
                            rectTR.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                            yield return null;
                        }
                    }
                    else
                    {
                        while (rectTR.anchoredPosition.x <= -0.001f)
                        {
                            rectTR.anchoredPosition -= Time.deltaTime * moveDir * xMove * moveSpeed;
                            yield return null;
                        }
                    }

                    rectTR.anchoredPosition = yMove * end;
                    break;

                case 3:
                    while ((sceneWidth + rectTR.anchoredPosition.x) >= 0.001f)
                    {
                        rectTR.anchoredPosition -= Time.deltaTime * moveDir * xMove * moveSpeed;
                        yield return null;
                    }

                    rectTR.anchoredPosition = -xMove + yMove * 2;
                    break;
                default:
                    break;
            }


            yield return new WaitForSeconds(0.3f);

            isMoveScene = false;
        }
    }
}
