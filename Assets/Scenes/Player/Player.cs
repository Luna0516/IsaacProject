using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Move move;

    Vector3 dir = Vector3.zero;

    public float speed = 1.0f;

    private void Awake()
    {
        move = new Move();
    }

    private void Update()
    {
        transform.position += Time.deltaTime * speed * dir;
    }
    private void OnEnable()
    {
        move.WASD.Enable();
        move.WASD.Move.performed += OnMove;
        move.WASD.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        move.WASD.Move.performed -= OnMove;
        move.WASD.Move.canceled -= OnMove;
        move.WASD.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        dir = value;
        Debug.Log($"{value}");
    }
    

}
