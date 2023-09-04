using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Item : TestBase
{
    public ItemData passiveItemData;
    public ItemData activeItemData;

    public Vector2 spawnPos = new Vector2(-3, 3);

    protected override void Test1(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(passiveItemData, spawnPos);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(activeItemData, spawnPos);
    }
}
