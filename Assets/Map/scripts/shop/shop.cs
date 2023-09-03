using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour
{
    /// <summary>
    /// 스폰될 상점 아이템 구조체
    /// </summary>
    struct ShopItem
    {
        public int price;
        public Transform tr;
        public Collider2D coll;
        public CheckBox checkBox;
        public Collider2D checkBoxColl;
    }

    /// <summary>
    /// 생성할 아이템들의 구조체 배열
    /// </summary>
    ShopItem[] shopItems;

    /// <summary>
    /// 아이템 스폰 위치 개수
    /// </summary>
    int childCount;

    /// <summary>
    /// 생성한 아이템의 가격을 표시할 스프라이트
    /// </summary>
    [Header("생성한 아이템의 가격을 표시할 스프라이트")]
    public Sprite[] priceSprites;

    private void Start()
    {
        childCount = transform.childCount;

        shopItems = new ShopItem[childCount];

        SpriteRenderer[] spriterenter = new SpriteRenderer[childCount];

        for (int i = 0; i < childCount; i++)
        {
            shopItems[i].tr = transform.GetChild(i);

            spriterenter[i] = shopItems[i].tr.GetComponent<SpriteRenderer>();

            int type = Random.Range(0, 4);

            GameObject shopItemObj = null;

            Vector2 spawnPos = shopItems[i].tr.position + Vector3.up;

            // 랜덤값에 따라 아이템을 생성하고 그 값을 구조체에 저장
            switch (type)
            {
                // 액티브 아이템
                case 0:
                    ActiveItem activeItem = (ActiveItem)Random.Range(0, System.Enum.GetValues(typeof(ActiveItem)).Length);
                    shopItemObj = ItemFactory.Inst.CreateActiveItem(activeItem, spawnPos);
                    spriterenter[i].sprite = priceSprites[2];
                    shopItems[i].price = 15;
                    break;

                // 패시브 아이템
                case 1:
                    PassiveItem passiveItem = (PassiveItem)Random.Range(0, System.Enum.GetValues(typeof(PassiveItem)).Length);
                    shopItemObj = ItemFactory.Inst.CreatePassiveItem(passiveItem, spawnPos);
                    spriterenter[i].sprite = priceSprites[2];
                    shopItems[i].price = 15;
                    break;

                // 기타 아이템
                case 2:
                    PropsItem propsItem = (PropsItem)Random.Range(3, System.Enum.GetValues(typeof(PropsItem)).Length);
                    shopItemObj = ItemFactory.Inst.CreatePropsItem(propsItem, spawnPos);
                    spriterenter[i].sprite = priceSprites[1];
                    shopItems[i].price = 5;
                    break;

                // 하트 아이템
                case 3:
                    HeartItem heartItem = (HeartItem)Random.Range(0, System.Enum.GetValues(typeof(HeartItem)).Length);
                    shopItemObj = ItemFactory.Inst.CreateHeartItem(heartItem, spawnPos);
                    spriterenter[i].sprite = priceSprites[0];
                    shopItems[i].price = 3;
                    shopItemObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    break;
                default:
                    break;
            }

            shopItems[i].coll = shopItemObj.GetComponent<Collider2D>();

            shopItemObj.transform.parent = shopItems[i].tr;

            shopItems[i].checkBox = shopItems[i].tr.GetComponent<CheckBox>();

            shopItems[i].checkBoxColl = shopItems[i].tr.GetComponent<Collider2D>();

            shopItems[i].checkBox.itemPrice = shopItems[i].price;

            shopItems[i].checkBoxColl.enabled = false;
            shopItems[i].coll.enabled = false;
        }

        Player player = GameManager.Inst.Player;

        player.onCoinChange += Refresh;

        Refresh(player.Coin);
    }

    /// <summary>
    /// 플레이어 코인개수에 따라 상점에 스폰된 아이템의 콜라이더 키고 끄기
    /// </summary>
    /// <param name="coinCount">플레이어의 코인 개수</param>
    void Refresh(int coinCount)
    {
        for(int i = 0; i < childCount; i++)
        {
            if(shopItems[i].coll == null)
            {
                continue;
            }

            if (shopItems[i].price <= coinCount)
            {
                shopItems[i].checkBoxColl.enabled = true;
                shopItems[i].coll.enabled = true;
            }
            else
            {
                shopItems[i].coll.enabled = false;
                shopItems[i].checkBoxColl.enabled = false;
            }
        }
    }
}