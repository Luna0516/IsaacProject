using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    TextMeshProUGUI coinCount = null;
    TextMeshProUGUI bomCount = null;
    TextMeshProUGUI keyCount = null;

    void Start() {
        Transform child = transform.GetChild(0);
        coinCount = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        bomCount = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        keyCount = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        Player player = GameManager.Inst.Player;

        player.onCoinChange += CoinTextChange;
        player.onBombChange += BombTextChange;
        player.onKeyChange += KeyTextChange;
    }

    /// <summary>
    /// 코인 개수가 변경될때 UI의 코인 개수를 변경해주는 함수
    /// </summary>
    /// <param name="value">변경된 코인 개수</param>
    void CoinTextChange(int value)
    {
        coinCount.text = $"{value:00}";
    }

    /// <summary>
    /// 폭탄 개수가 변경될때 UI의 폭탄 개수를 변경해주는 함수
    /// </summary>
    /// <param name="value">변경된 폭탄의 개수</param>
    void BombTextChange(int value)
    {
        bomCount.text = $"{value:00}";
    }

    /// <summary>
    /// 열쇠 개수가 변경될 때 UI의 열쇠 개수를 변경해주는 함수
    /// </summary>
    /// <param name="value">변경된 열쇠 개수</param>
    void KeyTextChange(int value)
    {
        keyCount.text = $"{value:00}";
    }
}
