using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int totalKill = 0;

    public bool clear = false;

    /// <summary>
    /// 엔딩 씬의 이름
    /// </summary>
    public string endingSceneName = "EndingScene";

    /// <summary>
    /// 비동기 명령 처리용
    /// </summary>
    AsyncOperation async;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindObjectOfType<Player>();
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

    /// <summary>
    /// 게임 종료시 호출할 함수
    /// </summary>
    /// <param name="playerLive">플레이어 생존 여부</param>
    public void FinshGame(bool playerLive)
    {
        clear = playerLive;

        StartCoroutine(LoadScene(playerLive));
    }

    /// <summary>
    /// 다음 씬 로드 코루틴
    /// </summary>
    IEnumerator LoadScene(bool playerLive)
    {
        
        float delayTime = clear ? 0.1f : 1.0f;

        yield return new WaitForSeconds(delayTime);

        async = SceneManager.LoadSceneAsync(endingSceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
