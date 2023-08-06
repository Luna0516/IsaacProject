using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Active : TestBase {
    public Player player;
    
    public ItemData itemData;

    protected override void Test1(InputAction.CallbackContext context) {
        ItemManager.CreateItem(itemData, Vector2.one * 2.0f);
    }
}
