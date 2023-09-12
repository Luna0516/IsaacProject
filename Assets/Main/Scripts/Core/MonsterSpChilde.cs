using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpChilde : MonoBehaviour
{

    public Action SapwnActive;

    public MonsterSpawner.AllMonsterSpawners allMonsterSpawners = new MonsterSpawner.AllMonsterSpawners();

    public bool spawnactive;
    bool SpawnActiveate
    {
        get
        {
            return spawnactive;
        }
        set
        {
            if(spawnactive != value)
            {
                if(spawnactive)
                {
                spawnactive = value;
                SapwnActive?.Invoke();

                }
            }
        }
    }

    private void Awake()
    {
        SapwnActive = Spawn;
        SpawnActiveate = false;
    }
    void Spawn()
    {
        for(int i = 0; i < allMonsterSpawners.spawnCount; i++)
        {
            Vector2 spawnpoint = new Vector2(this.transform.position.x + UnityEngine.Random.Range(allMonsterSpawners.spawnArea.x, -allMonsterSpawners.spawnArea.x), this.transform.position.y + UnityEngine.Random.Range(allMonsterSpawners.spawnArea.y, -allMonsterSpawners.spawnArea.y));
            Factory.Inst.GetObject(PoolObjectType.SpawnEffectPool, spawnpoint);
            Factory.Inst.GetObject(allMonsterSpawners.monsterList, spawnpoint);

        }
    }
    private void Update()
    {
        if(spawnactive)
        {
            SpawnActiveate = true;
            SpawnActiveate = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 spawnV1 = Vector3.up * allMonsterSpawners.spawnArea;
        Vector3 spawnV2 = Vector3.up * (-allMonsterSpawners.spawnArea);
        Vector3 spawnV3 = Vector3.right * (-allMonsterSpawners.spawnArea);
        Vector3 spawnV4 = Vector3.right * allMonsterSpawners.spawnArea;
        Gizmos.DrawLine(this.transform.position + spawnV3 + spawnV2, this.transform.position + spawnV2 + spawnV4);
        Gizmos.DrawLine(this.transform.position + spawnV2 + spawnV4, this.transform.position + spawnV1 + spawnV4);
        Gizmos.DrawLine(this.transform.position + spawnV1 + spawnV4, this.transform.position + spawnV1 + spawnV3);
        Gizmos.DrawLine(this.transform.position + spawnV1 + spawnV3, this.transform.position + spawnV3 + spawnV2);
    }
}
