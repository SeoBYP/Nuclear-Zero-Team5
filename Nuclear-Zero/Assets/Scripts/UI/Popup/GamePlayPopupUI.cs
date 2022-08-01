using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class GamePlayPopupUI : PopupUI
{
    enum Buttons
    {
        Play,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Play).gameObject, OnPlay, UIEvents.Click);
    }

    private void OnPlay(PointerEventData data)
    {
        GameManager.Instance.GameStart();
        ClosePopupUI();
    }
}
