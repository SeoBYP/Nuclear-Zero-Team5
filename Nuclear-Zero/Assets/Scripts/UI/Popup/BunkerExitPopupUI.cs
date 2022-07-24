using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class BunkerExitPopupUI : PopupUI
{
    enum GameObjects
    {
        Panel,
    }

    enum Buttons
    {
        Close,
        Exit,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        BindEvent(GetGameObject((int)GameObjects.Panel), OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnExit(PointerEventData data)
    {
        ClosePopupUI();
        UIManager.Instance.Get<BunkerPopupUI>().OnExit();
    }
}
