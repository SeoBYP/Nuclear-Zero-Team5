using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class CoinContents : SubUI
{
    private CoinPackagePurching coinPackage;
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        coinPackage = GetComponent<CoinPackagePurching>();
        if (coinPackage != null)
            coinPackage.InitCoinPackage();
    }


}
