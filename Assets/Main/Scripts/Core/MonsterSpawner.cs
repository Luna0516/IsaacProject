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

    MonsterSpChilde[] mssp;

    List<GameObject> spawnGameObject;

    [Header("스폰 리스트")]
    public AllMonsterSpawners[] allspawn;
    [Space(10)]

    [Header("스포너 갯수")]
    public int spawnercount = 0;
    [Space(5)]
    [Header("스폰 몬스터 갯수")]
    public int allspawncount = 0;
    [Space(15)]


    [Header("스폰 버튼")]
    public bool SpawnNow = false;

    public Action playerIn;
    public Action onAllEnemyDied;


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
                allspawncount += objectspawn.allMonsterSpawners.spawnCount;
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
        foreach (GameObject obj in spawnGameObject)
        {
            mssp[county] = obj.GetComponent<MonsterSpChilde>();
            allspawn[county] = mssp[county].allMonsterSpawners;
            county++;
        }
    }
}