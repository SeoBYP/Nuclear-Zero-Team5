using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class PackageContents : SubUI
{
    enum Buttons
    {
        Package1,
        Package2,
        Package3,
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

        BindEvent(GetButton((int)Buttons.Package1).gameObject, OnPackage1, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Package2).gameObject, OnPackage2, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Package3).gameObject, OnPackage3, UIEvents.Click);

        _shop = UIManager.Instance.Get<ShopPopupUI>();
    }

    private void OnPackage1(PointerEventData data)
    {
        //print("OnCoinPackage1");
        //int coin = 3000;
        //DataManager.Instance.playerInfo.SetCoin(coin);
        //_shop.DefaultSet();
    }
    private void OnPackage2(PointerEventData data)
    {
        int coin = 5500;
        int shield = 1;
        int magnet = 1;
        int life = 1;
        DataManager.Instance.playerInfo.SetCoin(coin);
        DataManager.Instance.playerInfo.SetLifeItemCount(life);
        DataManager.Instance.playerInfo.SetShieldItemCount(shield);
        DataManager.Instance.playerInfo.SetMagnetItemCount(magnet);

        _shop.DefaultSet();
    }
    private void OnPackage3(PointerEventData data)
    {
        int coin = 5000;
        int shield = 1;
        int magnet = 2;
        int life = 2;
        DataManager.Instance.playerInfo.SetCoin(coin);
        DataManager.Instance.playerInfo.SetLifeItemCount(life);
        DataManager.Instance.playerInfo.SetShieldItemCount(shield);
        DataManager.Instance.playerInfo.SetMagnetItemCount(magnet);
        _shop.DefaultSet();
    }
}
