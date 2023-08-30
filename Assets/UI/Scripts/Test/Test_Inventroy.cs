using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventroy : TestBase
{
    public ItemDataManager itemManager;

    public ItemData itemData1;
    public ItemData itemData2;

    protected override void Test1(InputAction.CallbackContext context) {
        //ItemFactory.Inst.CreateItem(itemManager.propsItemDatas[10]);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        GameManager.Inst.Player.Health--;
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(itemData1, new Vector2(1, 1));
    }
    protected override void Test4(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(itemData2, new Vector2(1, -1));
    }
}
