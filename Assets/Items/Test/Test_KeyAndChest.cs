using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_KeyAndChest : TestBase {
    public Player player;
    
    public ItemData Key;
    public GameObject Chest;


    private void Start() {
        player.Coin++;
        player.Bomb++;
        player.Key++;
    }

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.Inst.CreateItem(Key, Vector2.one * 2.0f);
    }

    protected override void Test2(InputAction.CallbackContext context) {
        GameObject chest = Instantiate(Chest);
        chest.transform.position = Vector2.right * 3.0f;
    }
}
