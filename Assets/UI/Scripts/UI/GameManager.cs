using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindObjectOfType<Player>();
    }

    public void PauseGame() {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }
    public void QuitPauseGame() {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}
