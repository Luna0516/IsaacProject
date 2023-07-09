using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemAP : MonoBehaviour
{
    Player player = null;

    public Sprite sprite;

    public Action<PassiveItem> getItem;

    void Awake() {
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