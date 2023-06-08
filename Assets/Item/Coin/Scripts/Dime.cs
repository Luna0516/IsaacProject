using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dime : CoinBase
{
    protected override void OnEnable() {
        base.OnEnable();
        coinCount = 10;
    }

}
