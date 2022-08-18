using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class CoinContents : SubUI
{
    enum Buttons
    {
        CoinPackage1,
        CoinPackage2,
        CoinPackage3,
        CoinPackage4,
    }

    private ShopPopupUI _shop;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.CoinPackage1).gameObject, OnCoinPackage1, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CoinPackage2).gameObject, OnCoinPackage2, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CoinPackage3).gameObject, OnCoinPackage3, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CoinPackage4).gameObject, OnCoinPackage4, UIEvents.Click);

        _shop = UIManager.Instance.Get<ShopPopupUI>();
    }

    private void OnCoinPackage1(PointerEventData data)
    {
        int coin = 3000;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
    private void OnCoinPackage2(PointerEventData data)
    {
        int coin = 5550;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
    private void OnCoinPackage3(PointerEventData data)
    {
        int coin = 10000;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
    private void OnCoinPackage4(PointerEventData data)
    {
        int coin = 15000;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
}
