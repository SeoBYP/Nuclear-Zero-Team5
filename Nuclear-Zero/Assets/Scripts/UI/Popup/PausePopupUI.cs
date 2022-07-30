using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class PausePopupUI : PopupUI
{
    enum Buttons
    {
        Replay,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Replay).gameObject, OnReplay, UIEvents.Click);
    }

    private void OnReplay(PointerEventData data)
    {
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<CountDownPopupUI>();
    }
}
