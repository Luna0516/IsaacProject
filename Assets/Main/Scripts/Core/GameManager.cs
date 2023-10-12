using System;
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
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindObjectOfType<Player>();
            }
            return player;
        }
    }

    /// <summary>
    /// 최종 킬수
    /// </summary>
    public int totalKill = 0;

    /// <summary>
    /// 엔딩씬으로 갈때 클리어 여부(true면 성공, false면 실패)
    /// </summary>
    public bool clear = false;

    /// <summary>
    /// 엔딩 씬의 이름
    /// </summary>
    public string endingSceneName = "EndingScene";

    /// <summary>
    /// 비동기 명령 처리용
    /// </summary>
    AsyncOperation async;

    /// <summary>
    /// 보스 체력이 변경 될 때마다 보내는 델리게이트(파라메터 : 보스의 남은 체력 비율)
    /// </summary>
    public Action<float> onBossHealthChange;

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
