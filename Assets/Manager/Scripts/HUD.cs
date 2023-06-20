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
                    myText.text = $"{Inventory.Instance.Coin:00}";
                    break;
                case InfoType.Bomb:
                    myText.text = $"{Inventory.Instance.Bomb:00}";
                    break;
                case InfoType.Key:
                    myText.text = $"{Inventory.Instance.Key:00}";
                    break;
            }
        }
    }
}
