using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackgroundController : MonoBehaviour
{
    bool isMove = false;

    int currentBGNum = 0;
    int CurrentBGNum
    {
        get => currentBGNum;
        set
        {
            if (currentBGNum != value && !isMove)
            {
                isMove = true;
                currentBGNum = Mathf.Clamp(value, 0, transform.childCount - 1);
            }
        }
    }

    float speed = 6000.0f;

    RectTransform rectTransform;

    UIInputActions inputActions;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        inputActions = new UIInputActions();
    }

    private void OnEnable()
    {
        inputActions.Start.Enable();
        inputActions.Start.Press.performed += OnPress;
        inputActions.Start.Back.performed += OnBack;
    }

    private void OnDisable()
    {
        inputActions.Start.Back.performed -= OnBack;
        inputActions.Start.Press.performed -= OnPress;
        inputActions.Start.Disable();
    }

    private void Update()
    {
        Vector2 current = rectTransform.anchoredPosition;
        Vector2 target = currentBGNum * Screen.height * Vector2.up;
        rectTransform.anchoredPosition = Vector2.MoveTowards(current, target, Time.deltaTime * speed);

        if (rectTransform.anchoredPosition.y == CurrentBGNum * Screen.height)
        {
            isMove = false;
        }
    }

    private void OnPress(InputAction.CallbackContext _)
    {
        CurrentBGNum++;
    }

    private void OnBack(InputAction.CallbackContext _)
    {
        CurrentBGNum--;
    }
}
