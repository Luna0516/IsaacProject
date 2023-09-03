using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenChest : Chest
{
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
    void ChestOpen() {
        foreach (var item in dropItems) {
            if (Random.value < item.dropPercentage) {
                //GameObject itemObj = ItemFactory.Inst.CreateItem(item.data, new Vector2(transform.position.x + Random.Range(-1.0f, 1.0f), transform.position.y + Random.Range(-1.0f, 1.0f)));
                //Rigidbody2D itemRigid = itemObj.GetComponent<Rigidbody2D>();
                //if (itemRigid != null)
                //{
                //    Vector2 dir = (itemObj.transform.position - transform.position).normalized;
                //    itemRigid.AddForce(dir * 25.0f, ForceMode2D.Impulse);
                //}
            }
        }
    }
}
