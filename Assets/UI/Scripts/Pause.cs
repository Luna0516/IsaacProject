using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private void Awake()
    {
        Transform child = transform.GetChild(0);
        Button newRun = child.GetComponent<Button>();
        newRun.onClick.AddListener(NewRun);

        child = transform.GetChild(1);
        Button resumeGame = child.GetComponent<Button>();
        resumeGame.onClick.AddListener(ResumeGame);

        child = transform.GetChild(2);
        Button exitGame = child.GetComponent<Button>();
        exitGame.onClick.AddListener(ExitGame);
    }

    /// <summary>
    /// NewRun을 눌렀을 때 실행할 함수
    /// </summary>
    private void NewRun()
    {
        Debug.Log("NewRun");
    }

    /// <summary>
    /// ResumeGame 눌렀을 때 실행할 함수
    /// </summary>
    private void ResumeGame()
    {
        Debug.Log("ResumeGame");
    }

    /// <summary>
    /// ExitGame 눌렀을 때 실행할 함수
    /// </summary>
    private void ExitGame()
    {
        Debug.Log("ExitGame");
    }
}