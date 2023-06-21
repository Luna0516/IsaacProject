using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimeCoin : CoinBase
{
    protected override void OnEnable() {
        base.OnEnable();
        coinCount = 10;
    }

}
