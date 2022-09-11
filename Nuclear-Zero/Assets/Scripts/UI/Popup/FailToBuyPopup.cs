using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class FailToBuyPopup : PopupUI
{
    enum Buttons
    {
        Charge,
        Close,
    }

    enum GameObjects
    {
        BackGround,
    }

    public override void Init()
    {
        base.Init();
        Bind();
    }

    private void Bind()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Charge).gameObject, OnCharge, UIEvents.Click);
    }

    private void OnCharge(PointerEventData data)
    {
        ClosePopupUI();
        UIManager.Instance.Get<ShopPopupUI>().SetCoinMenu();
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
}
