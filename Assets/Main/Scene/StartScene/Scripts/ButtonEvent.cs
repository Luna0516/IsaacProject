using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// 다음에 로딩할 씬의 이름
    /// </summary>
    public string nextSceneName = "LoadingScene";

    /// <summary>
    /// 비동기 명령 처리용
    /// </summary>
    AsyncOperation async;

    /// <summary>
    /// 버튼 타입 Enum
    /// </summary>
    public enum ButtonType
    {
        NewRun,
        ExitGame
    }

    /// <summary>
    /// 이 버튼의 타입
    /// </summary>
    public ButtonType type;

    /// <summary>
    /// 화살표 오브젝트
    /// </summary>
    GameObject arrow;

    // 컴포넌트
    Image image;

    /// <summary>
    /// 초기 색상 저장용
    /// </summary>
    Color defaultColor;

    private void Awake()
    {
        arrow = transform.GetChild(0).gameObject;

        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    /// <summary>
    /// 활성 함수
    /// </summary>
    private void Active()
    {
        image.color = Color.white;
        arrow.SetActive(true);
    }

    /// <summary>
    /// 비활성 함수
    /// </summary>
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
            // 빌드 하면 꺼진다...
            Application.Quit();
        }
    }

    /// <summary>
    /// 다음 씬 로드 코루틴
    /// </summary>
    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
