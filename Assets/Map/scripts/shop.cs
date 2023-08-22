using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class shop : MonoBehaviour
{
    public struct PriseList
    {
        SpriteRenderer[] prises;
        Sprite[] sp;

        public PriseList(int i)
        {
            prises = new SpriteRenderer[i];
            sp = new Sprite[i];
        }
    }

    Transform[] childeList;
    public ItemManager items;
    public GameObject emp;
    PriseList priselist;

    private void Awake()
    {
        priselist = new PriseList(this.transform.childCount);
        childeList = new Transform[this.transform.childCount];
        for (int i = 0; i < childeList.Length; i++)
        {
            childeList[i] = this.transform.GetChild(i);
            GameObject Cost = Instantiate(emp, this.transform.position + Vector3.up, Quaternion.identity);
            prises[i] = Cost;
            prises[i].AddComponent<SpriteRenderer>();
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Player player = collision.GetComponent<Player>();
            foreach (var chiled in childeList)
            {
                int path = Random.Range(0, 3);
                if (path == 0)
                {
                    GameObject itemsOBJ = ItemFactory.Inst.CreateItem(items.activeItemDatas[Random.Range(0, items.activeItemDatas.Length)], chiled.position);
                    CircleCollider2D col = itemsOBJ.GetComponent<CircleCollider2D>();
                    col.enabled = false;
                    prises[2].
                }
                else if (path == 1)
                {
                    GameObject itemsOBJ = ItemFactory.Inst.CreateItem(items.passiveItemDatas[Random.Range(0, items.passiveItemDatas.Length)], chiled.position);
                    CircleCollider2D col = itemsOBJ.GetComponent<CircleCollider2D>();
                    col.enabled = false;
                }
                else
                {
                    GameObject itemsOBJ = ItemFactory.Inst.CreateItem(items.propsItemDatas[Random.Range(0, items.propsItemDatas.Length)], chiled.position);
                    CircleCollider2D col = itemsOBJ.GetComponent<CircleCollider2D>();
                    col.enabled = false;
                }
            }

        }

    }
}
