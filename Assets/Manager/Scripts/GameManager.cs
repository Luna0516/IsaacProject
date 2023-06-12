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
    /// ����
    /// </summary>
    public int coin = 0;
    public int Coin {
        get => coin;
        set {
            coin += value;
            Debug.Log("���� ȹ�� : " + coin);
        }
    }

    /// <summary>
    /// ��ź
    /// </summary>
    public int bomb = 1;
    public int Bomb {
        get => bomb;
        set {
            bomb += value;
            Debug.Log("��ź ȹ�� : " + bomb);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public int key = 0;
    public int Key {
        get => key;
        set {
            key += value;
            Debug.Log("���� ȹ�� : " + key);

        }
    }
}
