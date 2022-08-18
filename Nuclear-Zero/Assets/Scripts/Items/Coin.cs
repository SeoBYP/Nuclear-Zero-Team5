using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemController
{
    public override void Init()
    {
        base.Init();
    }

    protected override void SetItemInfo()
    {
        GameAudioManager.Instance.Play2DSound("Coin");
        UIManager.Instance.Get<GameUI>().SetCoin();
    }
}
