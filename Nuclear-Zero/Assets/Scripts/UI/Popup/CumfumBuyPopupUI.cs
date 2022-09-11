using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class CumfumBuyPopupUI : PopupUI
{
    enum Buttons
    {
        Yes,
        No,
        Minuse,
        Plus,
    }

    enum Texts
    {
        ItemCountText,
        CoinCountText,
    }

    enum Images
    {
        Icon,
        BackGround,
    }

    private int itemCount = 1;
    private int price;
    private Items items;

    public override void Init()
    {
        base.Init();
        Binds();

        itemCount = 1;
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        //Bind<InputField>(typeof(InputFields));

        BindEvent(GetButton((int)Buttons.Yes).gameObject, OnYes, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.No).gameObject, OnNo, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Minuse).gameObject, OnMinuse, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Plus).gameObject, OnPlus, UIEvents.Click);
    }

    public void SetDefaultCount()
    {
        itemCount = 1;
    }

    private void OnYes(PointerEventData data)
    {
        switch (items)
        {
            case Items.Shield:
                DataManager.Instance.playerInfo.SetShieldItemCount(price, itemCount);
                UIManager.Instance.Get<ShopPopupUI>().DefaultSet();
                break;
            case Items.Magnet:
                DataManager.Instance.playerInfo.SetMagnetItemCount(price, itemCount);
                UIManager.Instance.Get<ShopPopupUI>().DefaultSet();
                break;
            case Items.Life:
                DataManager.Instance.playerInfo.SetLifeItemCount(price, itemCount);
                UIManager.Instance.Get<ShopPopupUI>().DefaultSet();
                break;
        }
        ClosePopupUI();
    }

    private void OnNo(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnMinuse(PointerEventData data)
    {
        --itemCount;
        if(itemCount <= 0)
        {
            itemCount = 1;
        }
        SetItemCountText();
        SetCoinCountText();
    }

    private void OnPlus(PointerEventData data)
    {
        ++itemCount;
        SetItemCountText();
        SetCoinCountText();
    }

    private void SetCoinCountText()
    {
        switch (items)
        {
            case Items.Shield:
                price = 1200 * itemCount;
                break;
            case Items.Magnet:
                price = 2800 * itemCount;
                break;
            case Items.Life:
                price = 4000 * itemCount;
                break;
        }
        string Gold = string.Format("{0:#,###}", price);
        GetText((int)Texts.CoinCountText).text = Gold;
    }

    private void SetItemCountText()
    {
        GetText((int)Texts.ItemCountText).text = itemCount.ToString();
    }

    public void SetItemIcon(Sprite sprite,Items items)
    {
        GetImage((int)Images.Icon).sprite = sprite;
        this.items = items;

        SetItemCountText();
        SetCoinCountText();
    }
}
