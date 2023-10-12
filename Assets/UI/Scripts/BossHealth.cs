using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    Slider bossSlider;

    private void Awake()
    {
        bossSlider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Inst.onBossHealthChange += ChangeGage;
    }

    void ChangeGage(float ratio)
    {
        bossSlider.value = ratio;
    }
}
