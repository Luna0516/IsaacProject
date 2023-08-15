using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_HPUI : TestBase
{
    public Vector2 spownPos;

    public ItemData R_H_Heart;
    public ItemData R_F_Heart;
    public ItemData S_H_Heart;
    public ItemData S_F_Heart;

    protected override void Test1(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(R_H_Heart, spownPos);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(R_F_Heart, spownPos);
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(S_H_Heart, spownPos);
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(S_F_Heart, spownPos);
    }
}
