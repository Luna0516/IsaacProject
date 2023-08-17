using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Tear,
    PenetrationTear,
    BigTear,
    GuidedTear,
    TearExplosion,
    EnemyBullet,
    EnemyBlood,
    EnemyMeat,
    EnemyShit
}

public class Factory : Singleton<Factory>
{
    TearPool tearPool;
    PenetrationTearPool penetrationTearPool;
    BigTearPool bigTearPool;
    GuidedMissileTearPool guidedMissileTearPool;
    TearExplosionPool tearExplosionPool;
    BloodPool bloodPool;
    MeatPool meatPool;
    ShitPool shitPool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        tearPool = GetComponentInChildren<TearPool>();
        tearExplosionPool = GetComponentInChildren<TearExplosionPool>();
        bigTearPool = GetComponentInChildren<BigTearPool>();
        guidedMissileTearPool = GetComponentInChildren<GuidedMissileTearPool>();
		bloodPool = GetComponentInChildren<BloodPool>();
        meatPool = GetComponentInChildren<MeatPool>();
        shitPool = GetComponentInChildren<ShitPool>();
        penetrationTearPool = GetComponentInChildren<PenetrationTearPool>();


		tearPool?.Initialize();
        tearExplosionPool?.Initialize();
        bigTearPool?.Initialize();
        guidedMissileTearPool?.Initialize();
        bloodPool?.Initialize();
        meatPool?.Initialize();
        shitPool?.Initialize();
        penetrationTearPool?.Initialize();

    }

    /// <summary>
    /// 오브젝트를 풀에서 하나 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetObject(PoolObjectType type, Transform spawn = null)
    {
        GameObject result = null;
        switch (type) {
            case PoolObjectType.Tear:
                result = tearPool?.GetObject(spawn)?.gameObject;
                break;
            case PoolObjectType.TearExplosion:
                result = tearExplosionPool?.GetObject(spawn)?.gameObject;
                break;
            case PoolObjectType.BigTear:
                result = bigTearPool?.GetObject(spawn)?.gameObject;
                break;
            case PoolObjectType.GuidedTear:
                result = guidedMissileTearPool?.GetObject(spawn)?.gameObject;
                break;
            case PoolObjectType.EnemyBullet:
                break;
            case PoolObjectType.EnemyBlood:
				result = bloodPool?.GetObject(spawn)?.gameObject;
				break;
            case PoolObjectType.EnemyMeat:
				result = meatPool?.GetObject(spawn)?.gameObject;
				break;
            case PoolObjectType.EnemyShit:
                result = shitPool?.GetObject()?.gameObject;
                break;
            case PoolObjectType.PenetrationTear:
                result = penetrationTearPool?.GetObject(spawn)?.gameObject;
                break;
            default:
                break;
        }

        return result;
    }

    /// <summary>
    /// 오브젝트를 풀에서 하나 가져오면서 위치와 각도를 설정하는 함수
    /// </summary>
    /// <param name="type">생성할 오브젝트의 종류</param>
    /// <param name="position">생성할 위치(월드좌표)</param>
    /// <param name="angle">z축 회전 정도</param>
    /// <returns>생성한 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3 position, float angle = 0.0f)
    {
        GameObject obj = GetObject(type);
        obj.transform.position = position;
        obj.transform.Rotate(angle * Vector3.forward);

        switch (type)
        {
            case PoolObjectType.Tear:
                //
                break;
            default:                
                break;
        }
        
        return obj;
    }
    public GameObject GetObject(PoolObjectType type, Vector3 position, Vector3 scale)
    {
        GameObject obj = GetObject(type);
        obj.transform.position = position;
        obj.transform.localScale = scale;

        switch (type)
        {
            case PoolObjectType.Tear:
                //
                break;
            default:
                break;
        }

        return obj;
    }
}
