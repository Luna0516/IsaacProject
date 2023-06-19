using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePenny : CoinBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        coinCount = 2;
    }
}
