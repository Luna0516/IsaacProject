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
        tearSpeedValue = transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        rangeValue = transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate() {
        damageValue.text = $"{player.Damage:F1}";
        speedValue.text = $"{player.Speed:F1}";
        shotSpeedValue.text = $"{player.ShotSpeed:F1}";
        tearSpeedValue.text = $"{player.TearSpeed:F1}";
        rangeValue.text = $"{player.Range:F1}";
    }
}
