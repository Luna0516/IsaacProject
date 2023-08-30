using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Item : TestBase
{
    public ActiveItem activeItem;
    public PassiveItem passiveItem;
    public HeartItem heartItem;

    protected override void Test1(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateActiveItem(activeItem);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreatePassiveItem(passiveItem);
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateHeartItem(heartItem);
    }
}
