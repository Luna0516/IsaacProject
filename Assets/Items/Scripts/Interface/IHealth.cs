using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth {
    /// <summary>
    /// 체력 회복용 함수
    /// </summary>
    /// <param name="target">회복 대상</param>
    bool Heal(GameObject target);
}
