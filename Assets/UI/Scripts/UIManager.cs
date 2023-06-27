using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> 
{
    Player player;

    public HP hp;

    void Start() {
        player = GameManager.Inst.Player;
    }

    public void PlayerUIInitialize() {
        hp.UpdateHP();
    }
}
