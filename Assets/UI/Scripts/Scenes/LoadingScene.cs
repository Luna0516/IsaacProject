using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    /// <summary>
    /// 로딩이 완료(게이지가 끝까지 다 찼을 때가 완료)되었다고 표시하는 변수
    /// </summary>
    bool loadingDone = false;

    /// <summary>
    /// 로딩바가 증가하는 속도
    /// </summary>
    public float loadingBarSpeed = 1.0f;

    /// <summary>
    /// 다음에 로딩할 씬의 이름
    /// </summary>
    public string nextSceneName = "BasementMain";

    /// <summary>
    /// 비동기 명령 처리용
    /// </summary>
    AsyncOperation async;

    public Sprite[] loadingSprites;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        child.GetComponent<Image>().sprite = loadingSprites[Random.Range(0, loadingSprites.Length)];
    }

    private void Start()
    {
        StartCoroutine(LoadScene());

        if (loadingDone)
        {
            async.allowSceneActivation = true;
        }
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            Debug.Log($"Progress : {async.progress}");
            yield return null;
        }

        Debug.Log("Loading Complete");

        loadingDone = true;

        yield return new WaitForSeconds(0.5f);
    }
}
