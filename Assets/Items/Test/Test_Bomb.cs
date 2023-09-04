using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Bomb : TestBase {
    public ItemData propsBomb;
    public Player player;
    public GameObject bomb;

    private void Start() {
        player.Coin++;
        player.Bomb++;
        player.Key++;
    }

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.Inst.CreateItem(propsBomb, Vector2.one * 2.0f);
    }

    protected override void Test2(InputAction.CallbackContext context) {
        GameObject bombObj = Instantiate(bomb);
        bombObj.transform.position = Vector2.right * 3.0f;
    }

    protected override void Test3(InputAction.CallbackContext context) {
        player.Bomb++;
    }
}
