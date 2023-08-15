using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_HUD : TestBase
{
    public Vector2 spawnPos;

    public ItemData PennyCoin;
    public ItemData Bomb;
    public ItemData Key;

    public Player player;

    protected override void Test1(InputAction.CallbackContext context)
    {
        player.Coin++;
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        player.Bomb++;
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        player.Key++;
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(PennyCoin, spawnPos);
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(Bomb, spawnPos);
    }

    protected override void Test6(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateItem(Key, spawnPos);
    }
}
