using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : PooledObject
{
    private void OnEnable()
    {
     StartCoroutine(Gravity_Life(0.8f));
    }


}
