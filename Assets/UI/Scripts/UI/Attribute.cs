using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attribute : MonoBehaviour
{
    Player player;
    TextMeshProUGUI damageValue = null;
    TextMeshProUGUI speedValue = null;
    TextMeshProUGUI shotSpeedValue = null;
    TextMeshProUGUI tearSpeedValue = null;
    TextMeshProUGUI rangeValue = null;

    void Start() {
        player = GameManager.Inst.Player;
        damageValue = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        speedValue = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        shotSpeedValue = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        tearSpeedValue = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        rangeValue = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate() {
        damageValue.text = $"{player.damage:00}";
        speedValue.text = $"{player.speed:00}";
        shotSpeedValue.text = $"{player.shotSpeed:00}";
        tearSpeedValue.text = $"{player.tearSpeed:00}";
        rangeValue.text = $"{player.range:00}";
    }
}
