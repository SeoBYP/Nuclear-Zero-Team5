using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ShopPopupUI : PopupUI
{
    enum Buttons
    {
        CoinMenu,
        ItemMenu,
        PackageMenu,
        Exit,
    }

    enum GameObjects
    {
        CoinContents,
        ItemContents,
        PackageContents,
    }

    enum ItemPackages
    {
        ItemPackage1,
        ItemPackage2,
        ItemPackage3,
    }

    enum Texts
    {
        CoinCountText,
        LifeCountText,
        ShieldCountText,
        MagnetCountText,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<ItemPackage>(typeof(ItemPackages));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.CoinMenu).gameObject, OnCoinMenuBtn, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ItemMenu).gameObject, OnItemMenuBtn, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.PackageMenu).gameObject, OnPackageMenuBtn, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnClose, UIEvents.Click);

        FindItemPackages();

        SetShopMenuBtn(Buttons.CoinMenu);
        SetShopContents(GameObjects.CoinContents);
        SetTexts();
    }

    public void DefaultSet()
    {
        SetTexts();
    }

    public void SetCoinMenu()
    {
        CumfumBuyPopupUI cumfumBuyPopupUI = UIManager.Instance.Get<CumfumBuyPopupUI>();
        if (cumfumBuyPopupUI != null)
            cumfumBuyPopupUI.ClosePopupUI();

        SetShopMenuBtn(Buttons.CoinMenu);
        SetShopContents(GameObjects.CoinContents);
        SetTexts();
    }

    private void FindItemPackages()
    {
        for(int i = 0; i <= (int)ItemPackages.ItemPackage3; i++)
        {
            Get<ItemPackage>(i).Init();
        }
    }

    private void OnCoinMenuBtn(PointerEventData data)
    {
        SetShopMenuBtn(Buttons.CoinMenu);
        SetShopContents(GameObjects.CoinContents);
    }

    private void OnItemMenuBtn(PointerEventData data)
    {
        SetShopMenuBtn(Buttons.ItemMenu);
        SetShopContents(GameObjects.ItemContents);
    }

    private void OnPackageMenuBtn(PointerEventData data)
    {
        SetShopMenuBtn(Buttons.PackageMenu);
        SetShopContents(GameObjects.PackageContents);
    }

    private void SetShopMenuBtn(Buttons menu)
    {
        for(int i = 0; i <= Buttons.PackageMenu.GetHashCode(); i++)
        {
            GetButton(i).image.color = new Color(1, 1, 1, 0.3f);
        }
        GetButton(menu.GetHashCode()).image.color = new Color(1, 1, 1, 1);
    }

    private void SetShopContents(GameObjects contents)
    {
        for (int i = 0; i <= GameObjects.PackageContents.GetHashCode(); i++)
        {
            GetGameObject(i).gameObject.SetActive(false);
        }
        GetGameObject(contents.GetHashCode()).gameObject.SetActive(true);
    }

    private void SetTexts()
    {
        int coin = DataManager.Instance.playerInfo.Gold;
        string coinCount = string.Format("{0:#,###}", coin);
        GetText((int)Texts.CoinCountText).text = coinCount;

        int lifeCount = DataManager.Instance.playerInfo.LifeItem;
        GetText((int)Texts.LifeCountText).text = lifeCount.ToString();

        int ShieldCount = DataManager.Instance.playerInfo.ShieldItem;
        GetText((int)Texts.ShieldCountText).text = ShieldCount.ToString();

        int magnetCount = DataManager.Instance.playerInfo.MagnetItem;
        GetText((int)Texts.MagnetCountText).text = magnetCount.ToString();
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

}
