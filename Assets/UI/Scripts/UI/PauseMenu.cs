using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public PausePanel pausePanel;
    public Button newRun;
    public Button resumeGame;
    public Button exitGame;

    Animator anim;
    CanvasGroup canvasGroup;

    float activeSec;

    void Awake() {
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        activeSec = pausePanel.activeSceond;
    }

    private void Start() {
        resumeGame.onClick.AddListener(pausePanel.ExitPanel);
        exitGame.onClick.AddListener(Application.Quit);
    }

    public void SetActivation(bool value) {
        if (value) {
            anim.SetBool("IsPause", true);
            newRun.Select();
            StartCoroutine(DelayActiveTime(activeSec));
        }else {
            anim.SetBool("IsPause", false);
            canvasGroup.interactable = false;
        }
    }

    IEnumerator DelayActiveTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        canvasGroup.interactable = true;
    }
}
