using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(this.gameObject,1.0f);
    }
}
