using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_MapMove : TestBase
{
    protected override void Test1(InputAction.CallbackContext context)
    {
        RoomManager.Inst.TestRemoveWallColl();
    }
}
