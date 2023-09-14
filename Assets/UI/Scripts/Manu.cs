using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manu : MonoBehaviour
{
    /// <summary>
    /// 메뉴가 열려있는지 확인하기 위한 bool값
    /// </summary>
    bool isOpen = false;

    Animator pauseAnim;
    Animator passiveInvenAnim;

    UIInputActions UiInput;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        pauseAnim = child.GetComponent<Animator>();

        child = transform.GetChild(1);
        passiveInvenAnim = child.GetComponent<Animator>();

        UiInput = new UIInputActions();
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
            pauseAnim.SetBool("Open", false);
            passiveInvenAnim.SetBool("Open", false);
            yield return new WaitForSeconds(0.5f);
            isOpen = !active;
        }
        else
        {
            pauseAnim.SetBool("Open", true);
            passiveInvenAnim.SetBool("Open", true);
            yield return new WaitForSeconds(0.5f);
            isOpen = !active;
        }
    }
}
