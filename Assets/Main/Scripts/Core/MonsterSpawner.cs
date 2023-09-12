using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

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
        public AllMonsterSpawners(PoolObjectType monster = PoolObjectType.EnemyMulligun, int spawncount = 1, Vector2 pos = default(Vector2))
        {
            this.monsterList = monster;
            this.spawnCount = spawncount;
            this.spawnArea = pos;
        }
        public PoolObjectType monsterList;
        public int spawnCount;
        public Vector2 spawnArea;
    }
    public MonsterSpChilde[] mssp;

    public List<GameObject> spawnGameObject;
    public AllMonsterSpawners[] allspawn;
    public AllMonsterSpawners[] Allspawn
    {
        get
        {
            return allspawn;
        }
        set
        {
            if (allspawn != value)
            {

            }
        }
    }

    public int spawnercount = 0;

    public List<AllMonsterSpawners> allSpawnDatas;


    public bool SpawnNow = false;
    private void Awake()
    {
        spawnercount = transform.childCount;
        spawnGameObject = new List<GameObject>(spawnercount);
        GetChilding(this.transform);
        allspawn = new AllMonsterSpawners[spawnercount];
        mssp = new MonsterSpChilde[spawnercount];
        loadSpawnDatas();
    }
    private void Update()
    {
        spawnActive(SpawnNow);
    }



    void spawnActive(bool spawnCheck)
    {
        if (spawnCheck)
        {
            SpawnInitialized();
            foreach (var objectspawn in mssp)
            {
                objectspawn.SapwnActive.Invoke();
            }
            SpawnNow = false;
        }
    }

    void SpawnInitialized()
    {
        int county = 0;
        foreach (var objectspawn in spawnGameObject)
        {
            mssp[county].allMonsterSpawners.monsterList = allspawn[county].monsterList;
            mssp[county].allMonsterSpawners.spawnCount = allspawn[county].spawnCount;
            mssp[county].allMonsterSpawners.spawnArea = allspawn[county].spawnArea;
            county++;
        }
    }

    void GetChilding(Transform parent)
    {
        foreach (Transform obj in parent)
        {
            spawnGameObject.Add(obj.gameObject);
            GetChilding(obj);
        }
    }
    void loadSpawnDatas()
    {
        int county = 0;
        allSpawnDatas.Clear();
        foreach (GameObject obj in spawnGameObject)
        {
            mssp[county] = obj.GetComponent<MonsterSpChilde>();
            allspawn[county] = mssp[county].allMonsterSpawners;
            county++;
        }
    }
}