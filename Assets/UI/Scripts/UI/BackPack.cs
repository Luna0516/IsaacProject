using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour {
    public PausePanel pausePanel;
    Animator anim;
    CanvasGroup canvasGroup;
    float activeSec;

    void Awake() {
        anim = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
        activeSec = pausePanel.activeSceond;
    }

    public void SetActivation(bool value) {
        if (value) {
            anim.SetBool("IsPause", true);
            StartCoroutine(DelayActiveTime(activeSec));
        } else {
            anim.SetBool("IsPause", false);
            canvasGroup.interactable = false;
        }
    }

    IEnumerator DelayActiveTime(float time) {
        yield return new WaitForSecondsRealtime(time);
        canvasGroup.interactable = true;
    }
}
