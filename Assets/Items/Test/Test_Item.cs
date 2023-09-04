using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Item : TestBase
{
    [Header("아이템 소환 위치(초기값 Vector2.zero")]
    public Vector2 spawnPos = Vector2.zero;

    [Header("1번 누르면 만들어질 아이템")]
    public ActiveItem activeItem;

    [Header("2번 누르면 만들어질 아이템")]
    public PassiveItem passiveItem;

    [Header("3번 누르면 만들어질 아이템")]
    public HeartItem heartItem;

    [Header("4번 누르면 만들어질 아이템")]
    public PropsItem propsItem;

    [Header("5번 누르면 만들어질 아이템")]
    public GameObject chest;

    [Header("6번 누르면 만들어질 아이템")]
    public GameObject goldenChest;

    protected override void Test1(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateActiveItem(activeItem, spawnPos);
    }

    protected override void Test2(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreatePassiveItem(passiveItem, spawnPos);
    }

    protected override void Test3(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreateHeartItem(heartItem, spawnPos);
    }

    protected override void Test4(InputAction.CallbackContext context)
    {
        ItemFactory.Inst.CreatePropsItem(propsItem, spawnPos);
    }

    protected override void Test5(InputAction.CallbackContext context)
    {
        GameObject chestObj = Instantiate(chest);
        chestObj.transform.position = spawnPos;
    }

    protected override void Test6(InputAction.CallbackContext context)
    {
        GameObject goldenChestObj = Instantiate(goldenChest);
        goldenChestObj.transform.position = spawnPos;
    }
}
