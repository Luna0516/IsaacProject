using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public enum MonsterList
    {
        Blood_Cry = 0,
        Muligun,
        Host,
        Fly,
        Shit,
        Rava,
        Monstro
    }

    [Serializable]
    public struct AllMonsterSpawners
    {
        public AllMonsterSpawners(MonsterList monster = MonsterList.Muligun, int spawncount = 1, Vector2 pos = default(Vector2))
        {
            this.monsterList = monster;
            this.spawnCount = spawncount;
            this.spawnArea = pos;
        }
        public MonsterList monsterList;
        public int spawnCount;
        public Vector2 spawnArea;
    }

    public int spawnercount;
    static int SpawnerCounty;
    public List<GameObject> spawnGameObject = new List<GameObject>(SpawnerCounty);
    public List<AllMonsterSpawners> AllSpawnDatas = new List<AllMonsterSpawners>(SpawnerCounty);

    private void Awake()
    {
        spawnercount = transform.childCount;
        SpawnerCounty = spawnercount;
        GetChilding(this.transform);
        foreach (GameObject obj in spawnGameObject) 
        {
            MonsterSpChilde sp = obj.GetComponent<MonsterSpChilde>();
            AllSpawnDatas.Add(sp.allMonsterSpawners);
        }
    }

    private void Start()
    {

    }

    void GetChilding(Transform parent)
    {
        foreach (Transform obj in parent) 
        {
            spawnGameObject.Add(obj.gameObject);
            GetChilding(obj);
        }
    }


}