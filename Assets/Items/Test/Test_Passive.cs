using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Passive : TestBase {
    public Player player;
    
    public ItemData itemData;

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.Inst.CreateItem(itemData, Vector2.one * 2.0f);
    }
}
