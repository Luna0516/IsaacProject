using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// 로딩 완료 변수
    /// </summary>
    bool loadingDone = false;

    /// <summary>
    /// 다음에 로딩할 씬의 이름
    /// </summary>
    public string nextSceneName = "LoadingScene";

    /// <summary>
    /// 비동기 명령 처리용
    /// </summary>
    AsyncOperation async;

    public enum ButtonType
    {
        NewRun,
        ExitGame
    }

    public ButtonType type;

    GameObject arrow;

    Image image;

    Color defaultColor;

    private void Awake()
    {
        arrow = transform.GetChild(0).gameObject;

        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    private void Active()
    {
        image.color = Color.white;
        arrow.SetActive(true);
    }

    private void Inactive()
    {
        image.color = defaultColor;
        arrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Active();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inactive();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(type == ButtonType.NewRun)
        {
            StartCoroutine(LoadScene());
        }
        else if(type == ButtonType.ExitGame)
        {
            Debug.Log("게임 종료!!!");
        }
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        loadingDone = true;

        async.allowSceneActivation = true;
    }
}
