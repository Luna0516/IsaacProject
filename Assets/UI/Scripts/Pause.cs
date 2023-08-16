using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    Animator anim;

    // 퍼지 할때 할꺼
    Button newRun;
    Button resumeGame;
    Button exitGame;

    readonly int Hash_IsPause = Animator.StringToHash("IsPause");

    private void Awake()
    {
        anim = GetComponent<Animator>();

        Transform child = transform.GetChild(0);
        newRun = child.GetComponent<Button>();
        newRun.onClick.AddListener(NewRun);

        child = transform.GetChild(1);
        resumeGame = child.GetComponent<Button>();
        resumeGame.onClick.AddListener(ResumGame);

        child = transform.GetChild(2);
        exitGame = child.GetComponent<Button>(); 
        exitGame.onClick.AddListener(ExitGame);
    }

    void NewRun()
    {
        Debug.Log("NewRun");
    }

    void ResumGame()
    {
        Debug.Log("ResumGame");
    }

    void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}