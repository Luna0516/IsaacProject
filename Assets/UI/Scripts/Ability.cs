using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Ability : MonoBehaviour
{
    Player player = null;

    TextMeshProUGUI damageValue = null;
    TextMeshProUGUI speedValue = null;
    TextMeshProUGUI shotSpeedValue = null;
    TextMeshProUGUI tearSpeedValue = null;
    TextMeshProUGUI rangeValue = null;

    void Start() {
        Transform child = transform.GetChild(0);
        damageValue = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(1);
        speedValue = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        shotSpeedValue = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        tearSpeedValue = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(4);
        rangeValue = child.GetChild(1).GetComponent<TextMeshProUGUI>();

        player = GameManager.Inst.Player;
    }

    void LateUpdate() {
        damageValue.text = $"{player.Damage:F1}";
        speedValue.text = $"{player.Speed:F1}";
        shotSpeedValue.text = $"{player.ShotSpeed:F1}";
        tearSpeedValue.text = $"{player.TearSpeed:F1}";
        rangeValue.text = $"{player.Range:F1}";
    }
}
