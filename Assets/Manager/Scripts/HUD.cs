using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType { Coin, Bomb, Key }

    public InfoType type;

    TextMeshProUGUI myText;

    private void Awake() {
        myText = GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate() {
        if (myText != null) {
            switch (type) {
                case InfoType.Coin:
                    myText.text = $"{GameManager.Inst.Coin:00}";
                    break;
                case InfoType.Bomb:
                    myText.text = $"{GameManager.Inst.Bomb:00}";
                    break;
                case InfoType.Key:
                    myText.text = $"{GameManager.Inst.Key:00}";
                    break;
            }
        }
    }
}
