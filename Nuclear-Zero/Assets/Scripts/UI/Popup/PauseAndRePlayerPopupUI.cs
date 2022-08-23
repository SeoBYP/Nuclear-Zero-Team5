using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
using System;

public class PauseAndRePlayerPopupUI : PopupUI
{
    enum Buttons
    {
        Yes,
        No,
    }

    private Action _onYes;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.No).gameObject, OnNo, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Yes).gameObject, OnYes, UIEvents.Click);
    }

    public void SetYesBtn(Action eventFunc)
    {
        _onYes = eventFunc;
    }

    private void OnYes(PointerEventData data)
    {
        if(_onYes != null)
        {
            ClosePopupUI();
            _onYes();
        }
    }

    private void OnNo(PointerEventData data)
    {
        ClosePopupUI();
    }
}
