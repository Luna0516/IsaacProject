using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Coin : TestBase {
    public Player player;
    
    public ItemData Dime;
    public ItemData Nickel;
    public ItemData Penny;

    private void Start() {

    }

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.Inst.CreateItem(Dime, Vector2.one * 2.0f);
    }

    protected override void Test2(InputAction.CallbackContext context) {
        ItemFactory.Inst.CreateItem(Nickel, Vector2.right * 2.0f);
    }
    protected override void Test3(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(Penny, Vector2.left * 2.0f);
    }
}
