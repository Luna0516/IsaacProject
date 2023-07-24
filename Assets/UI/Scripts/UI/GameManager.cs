using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// 플레이어 프로퍼티
    /// </summary>
    public Player Player
    {
        get 
        { 
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            return player; 
        }
    }

    /// <summary>
    /// 액티브인벤토리
    /// </summary>
    ActiveInventory activeInventory;

    /// <summary>
    /// 액티브인벤토리 프로퍼티
    /// </summary>
    public ActiveInventory ActiveInventory
    {
        get 
        {
            if (activeInventory == null) 
            {
                activeInventory = FindObjectOfType<ActiveInventory>();
            }
            return activeInventory;
        }
    }

    /// <summary>
    /// 아이템로드 액션 델리게이트
    /// </summary>
    public Action LoadItem;

    protected override void OnInitialize()
    {
        base.OnInitialize();   
    }

    /// <summary>
    /// 일시정지
    /// </summary>
    public void PauseGame() 
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }

    /// <summary>
    /// 일시정지 해제
    /// </summary>
    public void QuitPauseGame() 
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }








    public GameObject bloodObject;
    public GameObject meatObject;
    public Sprite[] BloodSprite;
    public Sprite[] MeatSprite;
}
