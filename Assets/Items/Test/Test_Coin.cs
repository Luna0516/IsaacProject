using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Coin : TestBase {
    public Player player;
    
    public ItemData coin;

    private void Start() {
        player.Coin++;
        player.Bomb++;
        player.Key++;
    }

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.CreateItem(coin, Vector2.one * 2.0f);
    }

    protected override void Test2(InputAction.CallbackContext context) {
        player.Health -= 1;
    }
}
