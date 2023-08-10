using UnityEngine;

[CreateAssetMenu(fileName ="SpawnerData.asset", menuName = "Spawners/Spawner")]


public class SpawnerData : ScriptableObject

{
    public GameObject itemToSpawn;

    public int minSpawn;

    public int maxSpawn;
}
