using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Active : TestBase {
    public ItemData itemData;

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.Inst.CreateItem(itemData, Vector2.one * 2.0f);
    }
}
