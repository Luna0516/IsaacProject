using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class shop : MonoBehaviour
{
    int childCount;

    struct PriseList
    {
        public ItemData itemdata;
        public Sprite Prise_Sprite;
        public int intiprise;
    }


    PriseList[] itemPrices;

    Transform[] childeList;

    Player player;

    public GameObject[] itemsOBJ;

    CircleCollider2D[] col;

    int prise1 = 3;

    int prise2 = 5;

    int prise3 = 15;

    public Sprite[] priceSprites;

    public ItemDataManager items;

    SpriteRenderer[] spriterenter;

    shop_chiled[] shopchi;

    public float elapse = 10f;

    bool purchased = false;
    public bool Purchased
    {
        get => purchased;
        set
        {
            if (purchased != value)
            {
                purchased = value;
                if (purchased)
                {
                    elapse = 0f;
                    Activate();
                }
                else
                {
                    DeActivate();
                }

            }
        }
    }

    private void Awake()
    {
        childCount = transform.childCount;
        childeList = new Transform[childCount];
        itemsOBJ = new GameObject[childCount];
        col = new CircleCollider2D[childCount];
        spriterenter = new SpriteRenderer[childCount];
        shopchi = new shop_chiled[childCount];

        for (int i = 0; i < childeList.Length; i++)
        {
            childeList[i] = this.transform.GetChild(i);
            spriterenter[i] = childeList[i].GetComponent<SpriteRenderer>();
            shopchi[i] = childeList[i].GetComponent<shop_chiled>();
        }

        itemPrices = new PriseList[childCount];
        for (int i = 0; i < childeList.Length; i++)
        {
            int path = Random.Range(0, 3);
            if (path == 0)
            {
                itemPrices[i].itemdata = items.activeItemDatas[Random.Range(0, items.activeItemDatas.Length)];
                itemPrices[i].Prise_Sprite = priceSprites[2];
                itemPrices[i].intiprise = prise3;
                spriterenter[i].sprite = itemPrices[i].Prise_Sprite;

            }
            else if (path == 1)
            {
                itemPrices[i].itemdata = items.passiveItemDatas[Random.Range(0, items.passiveItemDatas.Length)];
                itemPrices[i].Prise_Sprite = priceSprites[2];
                itemPrices[i].intiprise = prise3;
                spriterenter[i].sprite = itemPrices[i].Prise_Sprite;
            }
            else
            {
                //itemPrices[i].itemdata = items.propsItemDatas[Random.Range(0, items.propsItemDatas.Length)];
                itemPrices[i].Prise_Sprite = priceSprites[1];
                itemPrices[i].intiprise = prise2;
                spriterenter[i].sprite = itemPrices[i].Prise_Sprite;
            }

            shopchi[i].prises = itemPrices[i].intiprise;

        }
    }

    private void Start()
    {
        for (int i = 0; i < childeList.Length; i++)
        {

            //itemsOBJ[i] = ItemFactory.Inst.CreateItem(itemPrices[i].itemdata, childeList[i].position + Vector3.up);
            col[i] = itemsOBJ[i].GetComponent<CircleCollider2D>();
            if (player.Coin > itemPrices[i].intiprise)
            {
                col[i].enabled = true;
            }
            else
            {
                col[i].enabled = false;
            }
        }
    }
    private void OnEnable()
    {
        player = GameManager.Inst.Player;
    }

    private void Activate()
    {
        for (int i = 0; i < childeList.Length; i++)
        {
            if (col[i] != null)
            {
                if (Purchased)
                {
                    col[i].enabled = true;
                }
            }
        }
    }
    private void DeActivate()
    {
        for (int i = 0; i < childeList.Length; i++)
        {
            if (col[i] != null)
            {
                col[i].enabled = false;
            }
        }
    }


    void timecounter()
    {
        elapse += Time.deltaTime;
    }

    private void Update()
    {
        timecounter();
        if(elapse > 0.5f)
        {
            Purchased = false;
        }
    }
}
