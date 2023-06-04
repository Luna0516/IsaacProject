using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputAction InputAction;

    Vector2 inputVec;

    Animator anim;

    private void Awake() {
        InputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
    }

    private void OnEnable() {
        InputAction.Player.Move.Enable();
        InputAction.Player.Move.performed += OnMove;
        InputAction.Player.Move.canceled += OnMove;
    }

    private void Update() {
        transform.Translate(Time.deltaTime * 2f * inputVec);
    }

    private void OnDisable() {
        InputAction.Player.Move.canceled -= OnMove;
        InputAction.Player.Move.performed -= OnMove;
        InputAction.Player.Move.Disable();
    }

    void OnMove(InputAction.CallbackContext context) {
        inputVec = context.ReadValue<Vector2>();
        anim.SetFloat("InputX", inputVec.x);
        anim.SetFloat("InputY", inputVec.y);
    }
}
