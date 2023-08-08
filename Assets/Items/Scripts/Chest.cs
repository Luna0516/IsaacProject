using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;

    bool IsOpen = false;

    protected Player player = null;

    [System.Serializable]
    public struct DropItem {
        public ItemData data;
        [Range(0.0f, 1.0f)]
        public float dropPercentage;
    }

    public DropItem[] dropItems;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    protected virtual void Start() {
        player = GameManager.Inst.Player;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (!IsOpen) {
                IsOpen = true;
                anim.SetTrigger("Open");
                ChestOpen();
            }
        }
    }

    protected virtual void ChestOpen() {
        foreach (var item in dropItems) {
            if (Random.value < item.dropPercentage) {
                ItemManager.CreateItem(item.data, transform.position);
            }
        }
    }
}
