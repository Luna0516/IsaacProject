using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : PooledObject
{
    public void SpawnEffect_Done()
    {
        this.gameObject.SetActive(false);
    }
}
