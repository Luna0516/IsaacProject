using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    TestPlayer player;

    public TestPlayer Player {
        get {
            if (player == null) {
                Init();
            }
            return player;
        }
    }

    protected override void Init() {
        base.Init();
        player = FindObjectOfType<TestPlayer>();
    }

    /*
     
    Player player;

    public Player Player {
        get {
            if (player == null) {
                Init();
            }
            return player;
        }
    }

    protected override void Init() {
        base.Init();
        player = FindObjectOfType<Player>();
    }
     
     */
}
