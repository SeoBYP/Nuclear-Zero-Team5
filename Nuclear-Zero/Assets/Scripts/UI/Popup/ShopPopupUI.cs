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

    enum Texts
    {
        CoinCountText,
        LifeCountText,
        ShieldCountText,
        MagnetCountText,
    }

    enum SubUIs
    {
        PackageContents,
        CoinContents,
        ItemContents,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));;
        Bind<SubUI>(typeof(SubUIs));
        Bind<Text>(typeof(Texts));

        InitContents();

        BindEvent(GetButton((int)Buttons.CoinMenu).gameObject, OnCoinMenuBtn, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.ItemMenu).gameObject, OnItemMenuBtn, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.PackageMenu).gameObject, OnPackageMenuBtn, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnClose, UIEvents.Click);

        SetShopMenuBtn(Buttons.PackageMenu);
        SetShopContents(SubUIs.PackageContents);
        SetTexts();
    }

    public void OpenCoinPackage()
    {
        SetShopMenuBtn(Buttons.CoinMenu);
        SetShopContents(SubUIs.CoinContents);
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
        SetShopContents(SubUIs.CoinContents);
        SetTexts();
    }

    private void InitContents()
    {
        for(int i = 0;i < 3; i++)
        {
            Get<SubUI>(i).Init();
        }
    }

    private void OnCoinMenuBtn(PointerEventData data)
    {
        SetShopMenuBtn(Buttons.CoinMenu);
        SetShopContents(SubUIs.CoinContents);
    }

    private void OnItemMenuBtn(PointerEventData data)
    {
        SetShopMenuBtn(Buttons.ItemMenu);
        SetShopContents(SubUIs.ItemContents);
    }

    private void OnPackageMenuBtn(PointerEventData data)
    {
        SetShopMenuBtn(Buttons.PackageMenu);
        SetShopContents(SubUIs.PackageContents);
    }

    private void SetShopMenuBtn(Buttons menu)
    {
        for(int i = 0; i <= Buttons.PackageMenu.GetHashCode(); i++)
        {
            GetButton(i).image.color = new Color(1, 1, 1, 0.3f);
        }
        GetButton(menu.GetHashCode()).image.color = new Color(1, 1, 1, 1);
    }

    private void SetShopContents(SubUIs contents)
    {
        for (int i = 0; i <= 2; i++)
        {
            Get<SubUI>(i).SetActive(false);
        }
        Get<SubUI>(contents.GetHashCode()).SetActive(true);
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

        LobbyUI lobby = UIManager.Instance.Get<LobbyUI>();
        if(lobby != null)
            lobby.SetPlayerGoldText();
    }

    private void OnClose(PointerEventData data)
    {
        StageSelectPopupUI popupUI = UIManager.Instance.Get<StageSelectPopupUI>();
        if(popupUI != null)
        {
            popupUI.SetDefault();
        }
        ClosePopupUI();
    }

}
