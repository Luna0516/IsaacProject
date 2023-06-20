using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public static Inventory Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<Inventory>();
                if (instance == null) {
                    GameObject singleton = new GameObject("Inventory");
                    instance = singleton.AddComponent<Inventory>();
                }
            }
            return instance;
        }
    }

    public List<ItemBase> itemList = new();

    public delegate void OnGetItem();
    public OnGetItem onGetItem;

    public void AddItem(ItemBase item) {
        if (item != null) {
            ItemBase newItem = Instantiate(item);
            itemList.Add(newItem);
        }
    }

    // ������ ����
    public void RemoveItem(ItemBase item) {
        if (item != null) {
            itemList.Remove(item);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public int coin = 0;
    public int Coin {
        get => coin;
        set {
            coin += value;
            Debug.Log("���� ȹ�� : " + coin);
        }
    }
    /// <summary>
    /// ��ź
    /// </summary>
    public int bomb = 1;
    public int Bomb {
        get => bomb;
        set {
            bomb += value;
            Debug.Log("��ź ȹ�� : " + bomb);
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public int key = 0;
    public int Key {
        get => key;
        set {
            key += value;
            Debug.Log("���� ȹ�� : " + key);

        }
    }
}