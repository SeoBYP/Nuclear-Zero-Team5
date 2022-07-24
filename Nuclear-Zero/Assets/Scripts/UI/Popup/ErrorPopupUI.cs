using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ErrorPopupUI : PopupUI
{
    enum Buttons
    {
        ClosePopup,
    }

    enum Texts
    {
        PopupNameText,
        PopupText,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.ClosePopup).gameObject, OnClosePopup, UIEvents.Click);
    }

    private void OnClosePopup(PointerEventData data)
    {
        ClosePopupUI();
    }
}
