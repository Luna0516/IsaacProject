//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObjectRoomSpawner : MonoBehaviour
//{
//    // 총평 : 오브젝트를 렌덤 포인트에서 소환할수 있게 포인트를 렌덤으로 지정해두는 클래스로 보입니다.

//    [System.Serializable]
//    public struct RandomSpawner
//    {
//        public string name;

//        public SpawnerData spawnerData;
//    }

//    //public GridController grid;

//    public RandomSpawner[] spawnerData;

//    void Start()
//    {
//        //grid = GetComponentInChildren<GridController>();
//    }

//    public void InitialiseObjectSpawning()
//    {
//        foreach (RandomSpawner rs in spawnerData)
//        {
//            SpawnObjects(rs);
//        }
//    }

//    void SpawnObjects(RandomSpawner data)
//    {
//        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

//        for (int i = 0; i < randomIteration; i++)
//        {
//            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
//            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos],
//                Quaternion.identity, transform) as GameObject;
//            grid.availablePoints.RemoveAt(randomPos);
//            Debug.Log("Spawned Object!");
//        }
//    }
//}