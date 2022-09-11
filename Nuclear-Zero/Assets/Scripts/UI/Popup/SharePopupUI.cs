using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class SharePopupUI : PopupUI
{
    enum Buttons
    {
        Share,
        Close,
    }

    private ClickHandler _clickHandler;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        _clickHandler = GetComponent<ClickHandler>();

        BindEvent(GetButton((int)Buttons.Share).gameObject, OnShare, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
    }

    private void OnShare(PointerEventData data)
    {
        _clickHandler.ShareSomething();
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

    public void Shared()
    {
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<GameSharedPopupUI>();
    }
}
