using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_KnifeShooter : MonoBehaviour
{
    public KnifeAttacking knife;
    Test_Cooltime coolsys;

    public float TearSpeed=1;
    public float Damage = 1;
    public float Range = 3;
    public Vector2 MoveDir;



    private void Awake()
    {
        Instantiate(knife);
    }
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        coolsys = Test_Cooltime.Inst;
    }


}
