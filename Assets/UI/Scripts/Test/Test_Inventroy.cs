using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventroy : TestBase
{
    public ItemManager itemManager;

    protected override void Test1(InputAction.CallbackContext context) {
        ItemFactory.CreateItem(itemManager.propsItemDatas[10]);
    }
}
