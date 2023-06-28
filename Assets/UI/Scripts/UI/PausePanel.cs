using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {
    public PauseMenu pauseMenu;
    public BackPack backPack;

    bool isActivation = false;
    bool isControllable = false;
    [HideInInspector]
    public float activeSceond = 0.5f;

    Player player;

    void Start() {
        player = GameManager.Inst.Player;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isActivation && !isControllable) {
                EnterPanel();
            } else if (isActivation && isControllable) {
                ExitPanel();
            }
        }
    }

    public void EnterPanel() {
        StartCoroutine(DelayPasue(true));
    }

    public void ExitPanel() {
        StartCoroutine(DelayPasue(false));
    }

    IEnumerator DelayPasue(bool active) {
        if (active) {
            //player.PlayerPause();
        } else {
            GameManager.Inst.QuitPauseGame();
        }
        isActivation = active;
        isControllable = active;
        backPack.SetActivation(active);
        pauseMenu.SetActivation(active);
        yield return new WaitForSeconds(0.5f);
        if (active) {
            GameManager.Inst.PauseGame();
        } else {
            //player.PlayerQuitPause();
        }
    }
}
