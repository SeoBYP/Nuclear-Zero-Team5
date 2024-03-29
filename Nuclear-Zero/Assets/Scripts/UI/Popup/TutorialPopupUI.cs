using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class TutorialPopupUI : PopupUI
{
    enum Buttons
    {
        Close,
    }

    private swipe _swipe;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        _swipe = GetComponentInChildren<swipe>();
        if (_swipe != null)
            _swipe.Init();
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
}
