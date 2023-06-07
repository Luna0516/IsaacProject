using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("#GameManager")]
    public int coin = 0;
    public float health = 3;
    public int bombCount = 0;

    void Awake()
    {
        instance = this;
    }
}
