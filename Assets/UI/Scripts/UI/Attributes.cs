using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attributes : MonoBehaviour
{
    Player player;
    TextMeshProUGUI coinCount = null;
    TextMeshProUGUI bomCount = null;
    TextMeshProUGUI keyCount = null;

    void Start() {
        player = GameManager.Inst.Player;
        coinCount = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        bomCount = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        keyCount = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate() {
        coinCount.text = $"{player.Coin:00}";
        bomCount.text = $"{player.Bomb:00}";
        keyCount.text = $"{player.Key:00}";
    }
}
