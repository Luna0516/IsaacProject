using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nickel : CoinBase
{
    protected override void OnEnable() {
        base.OnEnable();
        coinCount = 5;
    }
}
