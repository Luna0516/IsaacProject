using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tear : MonoBehaviour
{
    public float interval=0.1f;
    public GameObject tears;
    Vector3 positionman;
    

    IEnumerator shotActive()
    {
        while(true) 
        { 
        spawn(tears);
        yield return new WaitForSeconds(interval);
        }
    }

    private void Start()
    {
        
            StartCoroutine(shotActive());
        
    }

    private void Awake()
    {
        positionman = this.gameObject.transform.position;
    }

    void spawn(GameObject spawnobject)
    {
        Instantiate(spawnobject, positionman,new Quaternion(0,0,0,0));
    }


}
