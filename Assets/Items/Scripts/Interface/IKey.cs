using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKey
{
    /// <summary>
    /// 열쇠 획득시 실행될 함수
    /// </summary>
    /// <param name="target">열쇠 획득을 하는 오브젝트</param>
    void GetKey(GameObject target);
}
