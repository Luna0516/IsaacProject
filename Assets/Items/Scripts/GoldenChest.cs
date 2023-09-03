using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenChest : Chest
{
    /// <summary>
    /// 아이템 종류와 드랍 확률
    /// </summary>
    [System.Serializable]
    public struct DropPassiveItem
    {
        public PassiveItem passiveItemCode;

        [Range(0.0f, 1.0f)]
        public float dropPercentage;
    }

    /// <summary>
    /// 드랍 아이템 구조체 배열
    /// </summary>
    public DropPassiveItem[] dropPassiveItem;

    protected override void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player player = collision.gameObject.GetComponent<Player>();

            if (!IsOpen && player.Key > 0) {    // 플레이어의 열쇠 개수가 1개 이상이고 열려있지 않다면 실행
                player.Key--;
                IsOpen = true;
                anim.SetTrigger("Open");
                ChestOpen();
            }
        }
    }

    /// <summary>
    /// 상자가 열렸을 때 아이템 생성 함수
    /// </summary>
    protected override void ChestOpen() {
        base.ChestOpen();

        foreach (var item in dropPassiveItem)
        {
            if (Random.value < item.dropPercentage)
            {
                Vector3 spawnPos = Vector2.one * Random.Range(-0.2f, 0.2f);

                GameObject itemObj = ItemFactory.Inst.CreatePassiveItem(item.passiveItemCode, transform.position + spawnPos);

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
