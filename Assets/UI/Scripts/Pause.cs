using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    /// <summary>
    /// 메뉴가 열려있는지 확인하기 위한 bool값
    /// </summary>
    bool isOpen = false;

    Animator anim;

    UIInputAction UiInput;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        UiInput = new UIInputAction();

        Transform child = transform.GetChild(0);
        Button newRun = child.GetComponent<Button>();
        newRun.onClick.AddListener(NewRun);

        child = transform.GetChild(1);
        Button resumeGame = child.GetComponent<Button>();
        resumeGame.onClick.AddListener(ResumeGame);

        child = transform.GetChild(2);
        Button exitGame = child.GetComponent<Button>();
        exitGame.onClick.AddListener(ExitGame);
    }

    private void OnEnable()
    {
        UiInput.UI.Enable();
        UiInput.UI.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        UiInput.UI.Pause.performed -= OnPause;
        UiInput.UI.Disable();
    }

    private void OnPause(InputAction.CallbackContext _)
    {
        StartCoroutine(PauseDelay(isOpen));
    }

    /// <summary>
    /// Esc버튼을 눌렀을 때 실행할 코루틴
    /// </summary>
    /// <param name="active">메뉴가 열려있는지 확인하는 bool값</param>
    IEnumerator PauseDelay(bool active)
    {
        if (active)
        {
            anim.SetBool("Open", false);
            yield return new WaitForSeconds(0.5f);
            isOpen = !active;
        }
        else
        {
            anim.SetBool("Open", true);
            yield return new WaitForSeconds(0.5f);
            isOpen = !active;
        }
    }

    /// <summary>
    /// NewRun을 눌렀을 때 실행할 함수
    /// </summary>
    private void NewRun()
    {
        Debug.Log("NewRun");
    }

    /// <summary>
    /// ResumeGame 눌렀을 때 실행할 함수
    /// </summary>
    private void ResumeGame()
    {
        Debug.Log("ResumeGame");
    }

    /// <summary>
    /// ExitGame 눌렀을 때 실행할 함수
    /// </summary>
    private void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}