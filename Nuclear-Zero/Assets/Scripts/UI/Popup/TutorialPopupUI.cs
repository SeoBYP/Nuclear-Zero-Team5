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
        Start,
    }
    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton(0).gameObject, OnStarted, UIEvents.Click);
    }

    private void OnStarted(PointerEventData data)
    {
        GameManager.Instance.GameStart();
        ClosePopupUI();
    }
}
