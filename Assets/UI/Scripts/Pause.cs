using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour, IPointerUpHandler
{
    // 퍼지 할때 할꺼
    Button newRun;
    Button resumeGame;
    Button exitGame;

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up", gameObject);
    }

    void Awake()
    {
        Transform child = transform.GetChild(0);
        newRun = child.GetComponent<Button>();

        child = transform.GetChild(1);
        resumeGame = child.GetComponent<Button>();

        child = transform.GetChild(2);
        exitGame = child.GetComponent<Button>();
    }
}
