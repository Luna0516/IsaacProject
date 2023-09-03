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

    /// <summary>
    /// 아이템 종류와 드랍 확률
    /// </summary>
    [System.Serializable]
    public struct DropPropsItem {
        public PropsItem propsItemCode;

        [Range(0.0f, 1.0f)]
        public float dropPercentage;
    }

    /// <summary>
    /// 드랍 아이템 구조체 배열
    /// </summary>
    public DropPropsItem[] dropPropsItems;

    /// <summary>
    /// 아이템 종류와 드랍 확률
    /// </summary>
    [System.Serializable]
    public struct DropHeartItem
    {
        public HeartItem heartItemCode;

        [Range(0.0f, 1.0f)]
        public float dropPercentage;
    }

    /// <summary>
    /// 드랍 아이템 구조체 배열
    /// </summary>
    public DropHeartItem[] dropHeartItem;

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
    protected virtual void ChestOpen() {
        foreach (var item in dropPropsItems) {
            if (Random.value < item.dropPercentage) {
                Vector3 spawnPos = Vector2.one * Random.Range(-0.2f, 0.2f);

                GameObject itemObj = ItemFactory.Inst.CreatePropsItem(item.propsItemCode, transform.position + spawnPos);

                Rigidbody2D targetRigid = itemObj.gameObject.GetComponent<Rigidbody2D>();

                if (targetRigid != null)
                {
                    Vector3 force = (itemObj.transform.position - transform.position).normalized;

                    targetRigid.AddForce(force, ForceMode2D.Impulse);
                }
            }
        }

        foreach (var item in dropHeartItem)
        {
            if (Random.value < item.dropPercentage)
            {
                Vector3 spawnPos = Vector2.one * Random.Range(-0.2f, 0.2f);

                GameObject itemObj = ItemFactory.Inst.CreateHeartItem(item.heartItemCode, transform.position + spawnPos);

                Rigidbody2D targetRigid = itemObj.gameObject.GetComponent<Rigidbody2D>();

                if (targetRigid != null)
                {
                    Vector3 force = (itemObj.transform.position - transform.position).normalized;

                    targetRigid.AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
    }
}
