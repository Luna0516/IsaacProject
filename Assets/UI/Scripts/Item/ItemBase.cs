using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemBase : MonoBehaviour {
    Player player = null;

    protected Sprite sprite;

    public Action<PassiveItem> getItem;

    protected virtual void Awake() {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnEnable() {
        GameManager.Inst.LoadItem?.Invoke();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        player = collision.gameObject.GetComponent<Player>();

        if (player == null)
            return;
    }
}