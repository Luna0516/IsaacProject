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

    [SerializeField]
    public struct AllMonsterSpawners
    {
        public AllMonsterSpawners(MonsterList monster = MonsterList.Muligun , int spawncount=1 , Vector2 pos = default(Vector2))
        {
            this.monsterList = monster;
            this.spawnCount = spawncount;
            this.spawnArea = pos;
        }
        public MonsterList monsterList;
        public int spawnCount;
        public Vector2 spawnArea;
    }

    public List<AllMonsterSpawners> spawnDatas;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 spawnV1 = Vector3.up * spawnArea;
            Vector3 spawnV2 = Vector3.up * (-spawnArea);
            Vector3 spawnV3 = Vector3.right * (-spawnArea);
            Vector3 spawnV4 = Vector3.right * spawnArea;
            *//*        Gizmos.DrawLine(this.transform.position + new Vector3(spawnArea.x,spawnArea.y,0),new Vector3(-spawnArea.x,spawnArea.y,));*//*
            Gizmos.DrawLine(this.transform.position + spawnV3 + spawnV2, this.transform.position + spawnV2 + spawnV4);
            Gizmos.DrawLine(this.transform.position + spawnV2 + spawnV4, this.transform.position + spawnV1 + spawnV4);
            Gizmos.DrawLine(this.transform.position + spawnV1 + spawnV4, this.transform.position + spawnV1 + spawnV3);
            Gizmos.DrawLine(this.transform.position + spawnV1 + spawnV3, this.transform.position + spawnV3 + spawnV2);
        }*/
}