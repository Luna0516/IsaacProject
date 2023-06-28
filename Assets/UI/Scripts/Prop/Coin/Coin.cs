using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    protected Player player = null;

    protected int count;
    public int Count {
        get => count;
        protected set {
            count = value;
        }
    }

    protected virtual void Start() {
        player = GameManager.Inst.Player;
    }
}
