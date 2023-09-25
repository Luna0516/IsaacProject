using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    /// <summary>
    /// 플레이어 프로퍼티
    /// </summary>
    public Player Player => player;

    ItemDataManager itemDataManager;
    public ItemDataManager ItemData => itemDataManager;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindObjectOfType<Player>();
        itemDataManager = FindObjectOfType<ItemDataManager>();
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
}
