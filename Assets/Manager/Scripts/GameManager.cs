using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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


    /// <summary>
    /// ÄÚÀÎ
    /// </summary>
    public int coin = 0;
    public int Coin {
        get => coin;
        set {
            coin += value;
            Debug.Log("ÄÚÀÎ È¹µæ : " + coin);
        }
    }

    /// <summary>
    /// ÆøÅº
    /// </summary>
    public int bomb = 1;
    public int Bomb {
        get => bomb;
        set {
            bomb += value;
            Debug.Log("ÆøÅº È¹µæ : " + bomb);
        }
    }

    /// <summary>
    /// ¿­¼è
    /// </summary>
    public int key = 0;
    public int Key {
        get => key;
        set {
            key += value;
            Debug.Log("¿­¼è È¹µæ : " + key);

        }
    }
}
