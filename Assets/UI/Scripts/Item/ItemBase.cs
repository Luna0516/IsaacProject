using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemBase : MonoBehaviour {
    ActiveInventory activeInventory;

    protected Sprite sprite;

    public PassiveItem passiveItem = null;
    public ActiveItem activeItem = null;

    public Action<ActiveItem> setItem;
    public Action<PassiveItem> getItem;

    protected virtual void Awake() {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Die() {
        DieEffect();
    }
    protected virtual void DieEffect() {

    }

    private void OnEnable() {
        GameManager.Inst.LoadItem?.Invoke();
        activeInventory = GameManager.Inst.ActiveInventory;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            
            if (passiveItem != null) {
                getItem?.Invoke(passiveItem);
                Destroy(this.gameObject);
            }
            if (activeItem != null) {
                
                if(activeInventory.transform.childCount > 2) {
                    GameObject active = activeInventory.transform.GetChild(2).gameObject;
                    active.SetActive(true);
                    active.transform.parent = null;
                    active.transform.position = GameManager.Inst.Player.transform.position + Vector3.up * 1.5f;

                }

                setItem?.Invoke(activeItem);

                this.gameObject.SetActive(false);
                this.transform.parent = activeInventory.gameObject.transform;
            }

        }
    }
}