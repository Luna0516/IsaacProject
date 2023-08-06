using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBase : MonoBehaviour {
    TestInputActions inputActions;

    protected virtual void Awake() {
        inputActions = new TestInputActions();
    }

    protected virtual void OnEnable() {
        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;
        inputActions.Test.Test2.performed += Test2;
        inputActions.Test.Test3.performed += Test3;
        inputActions.Test.Test4.performed += Test4;
        inputActions.Test.Test5.performed += Test5;
        inputActions.Test.Test6.performed += Test6;
        inputActions.Test.Test6.performed += Test7;
    }

    protected virtual void OnDisable() {
        inputActions.Test.Test6.performed -= Test7;
        inputActions.Test.Test6.performed -= Test6;
        inputActions.Test.Test5.performed -= Test5;
        inputActions.Test.Test4.performed -= Test4;
        inputActions.Test.Test3.performed -= Test3;
        inputActions.Test.Test2.performed -= Test2;
        inputActions.Test.Test1.performed -= Test1;
        inputActions.Test.Disable();
    }

    protected virtual void Test1(InputAction.CallbackContext context) {

    }
    protected virtual void Test2(InputAction.CallbackContext context) {

    }
    protected virtual void Test3(InputAction.CallbackContext context) {

    }
    protected virtual void Test4(InputAction.CallbackContext context) {

    }
    protected virtual void Test5(InputAction.CallbackContext context) {
 
    }
    protected virtual void Test6(InputAction.CallbackContext context) {

    }
    protected virtual void Test7(InputAction.CallbackContext context) {

    }
}