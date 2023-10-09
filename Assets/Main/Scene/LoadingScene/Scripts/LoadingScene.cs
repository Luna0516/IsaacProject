using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    /// <summary>
    /// 로딩 완료 변수
    /// </summary>
    bool loadingDone = false;

    /// <summary>
    /// 다음에 로딩할 씬의 이름
    /// </summary>
    public string nextSceneName = "CreateMap";

    /// <summary>
    /// 비동기 명령 처리용
    /// </summary>
    AsyncOperation async;

    /// <summary>
    /// 로딩창 이미지
    /// </summary>
    public Sprite[] loadingSprites;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        child.GetComponent<Image>().sprite = loadingSprites[Random.Range(0, loadingSprites.Length)];
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 게임 씬으로 넘어가는 코루틴
    /// </summary>
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        loadingDone = true;

        if (loadingDone)
        {
            yield return new WaitForSeconds(0.5f);

            async.allowSceneActivation = true;
        }
    }
}
