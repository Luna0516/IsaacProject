using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    PlayerAction playerAction;

    public float shotSpeed = 1.00f;
    private void Update()
    {
       transform.position += Time.deltaTime * shotSpeed * Vector3.right;
    }
}
