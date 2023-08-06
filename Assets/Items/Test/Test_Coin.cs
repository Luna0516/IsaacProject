using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Coin : TestBase {
    public Player player;
    
    public ItemData coin;

    protected override void Test1(InputAction.CallbackContext context) {
        ItemManager.CreateItem(coin, Vector2.one * 2.0f);
    }
}
