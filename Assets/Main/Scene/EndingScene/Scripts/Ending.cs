using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ending : MonoBehaviour
{
    TextMeshProUGUI score;
    TextMeshProUGUI endingText;

    private void Awake()
    {
        Transform child = transform.GetChild(2);
        score = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(3);
        endingText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        score.text = $"{GameManager.Inst.totalKill:F0}";

        endingText.text = GameManager.Inst.clear ? "Clear" : "Fail";
    }
}
