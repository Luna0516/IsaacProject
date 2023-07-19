using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player
    {
        get 
        { 
            if (player == null)
            {
                OnInitialize();
            }
            return player; 
        }
    }

    ActiveInventory activeInventory;
    public ActiveInventory ActiveInventory
    {
        get 
        {
            if (activeInventory == null) 
            {
                OnInitialize();
            }
            return activeInventory;
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindObjectOfType<Player>();
        activeInventory = FindObjectOfType<ActiveInventory>();
    }

    public Action LoadItem;

    public void PauseGame() 
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }

    public void QuitPauseGame() 
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}
