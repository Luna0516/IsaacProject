using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable
{
    /// <summary>
    /// 아이템을 소비시키는 함수
    /// </summary>
    /// <param name="target">아이템 효과를 받는 대상</param>
    /// <returns>아이템 삭제 여부</returns>
    bool Consume(GameObject target);
}
