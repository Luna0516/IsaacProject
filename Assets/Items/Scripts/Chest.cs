using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    protected Animator anim;

    /// <summary>
    /// 상자가 열려 있는지 확인용
    /// </summary>
    protected bool IsOpen = false;

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

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (!IsOpen) {
                IsOpen = true;
                anim.SetTrigger("Open");
                ChestOpen();
            }
        }
    }

    /// <summary>
    /// 상자가 열렸을 때 아이템 생성 함수
    /// </summary>
    void ChestOpen() {
        foreach (var item in dropItems) {
            if (Random.value < item.dropPercentage) {
                ItemFactory.Inst.CreateItem(item.data, transform.position);
            }
        }
    }
}
