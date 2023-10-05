using System;
using Unity.VisualScripting;
using UnityEngine;

public class Test_KnifeShooter : MonoBehaviour
{
    public KnifeAttacking knife;
    KnifeAttacking instknife;
    Test_Cooltime coolsys;

    public float TearSpeed = 3;
    public float Damage = 1;
    public float Range = 3;
    public Vector2 MoveDir;


    public bool randomButton = false;

    public bool attackActive = false;
    bool copyActive=false;
    public bool AttackActive
    {
        get
        {
            return copyActive;
        }
        set
        {
            if (copyActive != value)
            {
                copyActive = value;
                if (copyActive)
                {
                    Debug.Log("참");
                    instknife.pressButton();
                }
                else
                {
                    Debug.Log("불");
                    instknife.cancleButton();
                }
            }
        }
    }
    Action updater;
    public enum path
    {
        up = 0,
        donwn,
        left,
        right
    }
    public path newpath;

    private void Awake()
    {
        instknife = Instantiate(knife);
    }
    private void OnEnable()
    {

    }
    private void Start()
    {
        coolsys = Test_Cooltime.Inst;
        updater += () => { RandomPattOrNot(randomButton); };
        updater += () => { AttackActiveate(attackActive); };
    }
    void AttackActiveate(bool active)
    {
        if (active)
        {
            AttackActive = true;
        }
        else
        {
            AttackActive = false;
        }
    }
    private void Update()
    {
        updater();
    }
    void RandomPattOrNot(bool checker)
    {
        if (checker)
        {
            int randomint = UnityEngine.Random.Range(0, 4);
            newpath = (path)randomint;
        }
        patternSellect(newpath);
    }
    void patternSellect(path sellector)
    {
        switch (sellector)
        {
            case path.up:
                MoveDir = Vector2.up;
                break;
            case path.donwn:
                MoveDir = Vector2.down;
                break;
            case path.left:
                MoveDir = Vector2.left;
                break;
            case path.right:
                MoveDir = Vector2.right;
                break;
            default:
                break;
        }
    }

}
