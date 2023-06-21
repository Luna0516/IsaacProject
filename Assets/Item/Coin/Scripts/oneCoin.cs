using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneCoin : CoinBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        coinCount = 1;
    }
}
