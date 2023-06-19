using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penny : CoinBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        coinCount = 1;
    }
}
