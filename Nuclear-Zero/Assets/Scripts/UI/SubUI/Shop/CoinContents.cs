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

    enum Texts
    {
        TimeCount,
    }

    enum GameObjects
    {
        NonRecItem,
        RecItem,
    }

    private ShopPopupUI _shop;

    public override void Init()
    {
        base.Init();
        Binds();
        GoogleMobileAdsManager.Instance.RequestAndLoadRewardedInterstitialAd();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.CoinPackage1).gameObject, OnCoinPackage1, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CoinPackage2).gameObject, OnCoinPackage2, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CoinPackage3).gameObject, OnCoinPackage3, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.CoinPackage4).gameObject, OnCoinPackage4, UIEvents.Click);

        _shop = UIManager.Instance.Get<ShopPopupUI>();
    }


    private void OnCoinPackage1(PointerEventData data)
    {
        int coin = 8000;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
    private void OnCoinPackage2(PointerEventData data)
    {
        int coin = 13400;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
    private void OnCoinPackage3(PointerEventData data)
    {
        int coin = 20000;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
    private void OnCoinPackage4(PointerEventData data)
    {
        int coin = 28500;
        DataManager.Instance.playerInfo.SetCoin(coin);
        _shop.DefaultSet();
    }
}
