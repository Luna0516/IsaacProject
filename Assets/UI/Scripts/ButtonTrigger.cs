using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
        defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);

        Inactive();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Active();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inactive();
    }
}
