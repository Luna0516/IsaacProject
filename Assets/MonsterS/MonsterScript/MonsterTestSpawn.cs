using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterTestSpawn : TestBase
{
    MonsterSpawner monsterspawn;

    protected override void Awake()
    {
        base.Awake();
        monsterspawn = FindObjectOfType<MonsterSpawner>();
    }
    protected override void Test1(InputAction.CallbackContext context)
    {
        monsterspawn.SpawnNow = true;
    }
}
