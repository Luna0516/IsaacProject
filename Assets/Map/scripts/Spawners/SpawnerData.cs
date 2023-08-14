using UnityEngine;

[CreateAssetMenu(fileName ="SpawnerData.asset", menuName = "Spawners/Spawner")]


public class SpawnerData : ScriptableObject

{
    /// <summary>
    /// 스폰할 아이템 GameObject
    /// </summary>
    public GameObject itemToSpawn;

    /// <summary>
    /// 최소 숫자
    /// </summary>
    public int minSpawn;


    /// <summary>
    /// 최대 숫자
    /// </summary>
    public int maxSpawn;
}
