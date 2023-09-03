using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Props Item Data", menuName = "Scriptable Object/Props ItemData", order = 4)]
public class PropsItemData : ItemData, IConsumable
{
    public PropsItem propsType;

    public int itemValues;

    /// <summary>
    /// 아이템을 먹으면 같은 종류의 플레이어의 아이템의 개수를 증가 시키는 함수
    /// </summary>
    /// <param name="target">아이템을 먹는 오브젝트(플레이어)</param>
    /// <returns>아이템 삭제 여부</returns>
    public bool Consume(GameObject target)
    {
        bool result = false;

        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            switch (propsType)
            {
                case PropsItem.Penny:
                case PropsItem.Nickel:
                case PropsItem.Dime:
                    result = false;
                    break;
                case PropsItem.Bomb:
                case PropsItem.DoubleBomb:
                    result = true;
                    break;
                case PropsItem.Key:
                case PropsItem.KeyRing:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
        }

        return result;
    }
}
