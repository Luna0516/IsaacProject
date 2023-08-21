using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Damage : TestBase
{
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    protected override void Test1(InputAction.CallbackContext context)
    {
        player.Damaged();
    }
}
