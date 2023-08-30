using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInven : MonoBehaviour
{
    [Serializable]
    public struct ActiveIcons
    {
        public int coolTime;
        public Sprite[] coolTimes;
    }
    [SerializeField]
    public ActiveIcons[] itemStacks;

    Image itemImage;
    Image coolTimeImage;

    CanvasGroup group;

    ActiveIcons currentItem;
    int currentCool = 0;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        itemImage = child.GetComponent<Image>();

        child = transform.GetChild(1);
        coolTimeImage = child.GetComponent<Image>();

        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    private void Start()
    {
        GameManager.Inst.Player.getActiveItem += GetItem;
        GameManager.Inst.Player.onUseActive += UseItem;
    }

    /// <summary>
    /// 플레이어가 액티브아이템을 먹으면 실행할 함수
    /// </summary>
    /// <param name="itemData">플레이어가 먹은 액티브 아이템</param>
    private void GetItem(ActiveItemData itemData)
    {
        group.alpha = 1;

        itemImage.sprite = itemData.icon;

        currentItem = itemStacks[itemData.coolTime];

        currentCool = itemData.coolTime;

        coolTimeImage.sprite = currentItem.coolTimes[currentCool];
    }

    /// <summary>
    /// 플레이어가 액티브 아이템을 사용하면 실행할 함수
    /// </summary>
    void UseItem()
    {
        if (currentCool == currentItem.coolTime)
        {
            currentCool = 0;
            coolTimeImage.sprite = currentItem.coolTimes[currentCool];
        }
    }
}
