using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    /// <summary>
    /// 다음에 로딩할 로드 씬의 이름
    /// </summary>
    //string nextLoadSceneName = "LoadScene";

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
                prevScene = sceneNum;

                if(value == 4 && ArrowNum == 0)
                {
                    Debug.Log("GameLoadScene Gogo!!");
                }

                sceneNum = Math.Clamp(value, 0, 3);

                StartCoroutine(ChangeBackground(prevScene, sceneNum));
            }
        }
    }

    /// <summary>
    /// 이전 배경화면 번호 (저장용)
    /// </summary>
    int prevScene = 0;

    /// <summary>
    /// 파일 선택 번호
    /// </summary>
    int fileSelectNum = 0;
    /// <summary>
    /// 파일 선택 번호 프로퍼티
    /// </summary>
    int FileSelectNum
    {
        get => fileSelectNum;
        set
        {
            if (fileSelectNum != value)
            {
                prevfileSelectNum = fileSelectNum;

                fileSelectNum = value;

                if (fileSelectNum == -1)
                {
                    fileSelectNum = 1;
                }
                else if(fileSelectNum == 2)
                {
                    fileSelectNum = 0;
                }
            }
        }
    }
    /// <summary>
    /// 이전 파일 선택 번호
    /// </summary>
    int prevfileSelectNum = 1;

    int arrowNum = 0;
    int ArrowNum
    {
        get => arrowNum;
        set
        {
            if (arrowNum != value)
            {
                arrowNum = value;

                if (arrowNum == -1)
                {
                    arrowNum = 2;
                }
                else if (arrowNum == 3)
                {
                    arrowNum = 0;
                }
            }
        }
    }

    /// <summary>
    /// 배경화면 이동 속도
    /// </summary>
    float moveSpeed = 3.5f;

    Vector2 pressButton;

    // x, y 움직임 고정 벡터
    Vector2 xMove = new Vector2(sceneWidth, 0);
    Vector2 yMove = new Vector2(0, sceneHeight);

    // arrow 고정 위치
    Vector2 newRunArrowPos = new Vector2(-280, 240);
    Vector2 statsArrowPos = new Vector2(-240, -160);
    Vector2 optionsArrowPos = new Vector2(-215, -290);

    GameObject[] fileSelects;

    WaitForSeconds wait = new WaitForSeconds(0.3f);

    RectTransform rect;
    RectTransform arrowRect;

    UIInputActions inputActions;

    private void Awake()
    {
        fileSelects = new GameObject[2];
        Transform child = transform.GetChild(1);
        fileSelects[0] = child.GetChild(1).gameObject;
        fileSelects[1] = child.GetChild(2).gameObject;

        rect = GetComponent<RectTransform>();
        child = transform.GetChild(3).GetChild(0);
        arrowRect = child.GetComponent<RectTransform>();

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

    private void OnPress(InputAction.CallbackContext _)
    {
        if (!isMoveScene)
        {
            SceneNum += 1;
        }
    }

    private void OnBack(InputAction.CallbackContext _)
    {
        if (!isMoveScene)
        {
            SceneNum -= 1;
        }
    }

    private void OnSelectArrow(InputAction.CallbackContext context)
    {
        pressButton = context.ReadValue<Vector2>();

        if (SceneNum == 1)
        {
            FileSelectNum += (int)pressButton.x;
            SelectFile();
        }
        else if(SceneNum == 3)
        {
            ArrowNum -= (int)pressButton.y;
            MoveArrow();
        }
    }

    private void SelectFile()
    {
        fileSelects[FileSelectNum].GetComponent<RectTransform>().anchoredPosition += Vector2.up * 60.0f;
        fileSelects[FileSelectNum].GetComponent<Image>().color = Color.white;

        fileSelects[prevfileSelectNum].GetComponent<RectTransform>().anchoredPosition += Vector2.down * 60.0f;
        fileSelects[prevfileSelectNum].GetComponent<Image>().color = Color.black;
    }

    private void MoveArrow()
    {
        switch (ArrowNum)
        {
            case 0:
                arrowRect.anchoredPosition = newRunArrowPos;
                break;
            case 1:
                arrowRect.anchoredPosition = statsArrowPos;
                break;
            case 2:
                arrowRect.anchoredPosition = optionsArrowPos;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 배경화면 이동 코루틴
    /// </summary>
    /// <param name="start">이동 시작 지점</param>
    /// <param name="end">이동 종료 지점</param>
    private IEnumerator ChangeBackground(int start, int end)
    {
        if (!isMoveScene)
        {
            isMoveScene = true;

            int moveDir = end - start;  // -면 뒤로, +면 앞으로 이동

            switch (end)
            {
                case 0:
                    while ((rect.anchoredPosition.y - (sceneHeight * end)) >= 0.001f)
                    {
                        rect.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                        yield return null;
                    }

                    rect.anchoredPosition = yMove * end;
                    break;

                case 1:
                    if (moveDir > 0)
                    {
                        while (((sceneHeight * end) - rect.anchoredPosition.y) >= 0.001f)
                        {
                            rect.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                            yield return null;
                        }
                    }
                    else
                    {
                        while ((rect.anchoredPosition.y - (sceneHeight * end)) >= 0.001f)
                        {
                            rect.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                            yield return null;
                        }
                    }

                    rect.anchoredPosition = yMove * end;
                    break;

                case 2:
                    if (moveDir > 0)
                    {
                        while (((sceneHeight * end) - rect.anchoredPosition.y) >= 0.001f)
                        {
                            rect.anchoredPosition += Time.deltaTime * moveDir * yMove * moveSpeed;
                            yield return null;
                        }
                    }
                    else
                    {
                        while (rect.anchoredPosition.x <= -0.001f)
                        {
                            rect.anchoredPosition -= Time.deltaTime * moveDir * xMove * moveSpeed;
                            yield return null;
                        }
                    }

                    rect.anchoredPosition = yMove * end;
                    break;

                case 3:
                    while ((sceneWidth + rect.anchoredPosition.x) >= 0.001f)
                    {
                        rect.anchoredPosition -= Time.deltaTime * moveDir * xMove * moveSpeed;
                        yield return null;
                    }

                    rect.anchoredPosition = -xMove + yMove * 2;
                    break;
                default:
                    break;
            }


            yield return wait;

            isMoveScene = false;
        }
    }
}
