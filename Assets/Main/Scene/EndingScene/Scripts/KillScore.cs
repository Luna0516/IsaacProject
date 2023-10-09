using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillScore : MonoBehaviour
{
    TextMeshProUGUI score;

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Debug.Log(GameManager.Inst.totalKill);
        score.text = $"{GameManager.Inst.totalKill:F0}";
    }
}
