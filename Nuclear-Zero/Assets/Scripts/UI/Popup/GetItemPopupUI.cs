using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class GetItemPopupUI : PopupUI
{
    enum Buttons
    {
        ADButton,
    }

    enum GameObjects
    {
        BackGround,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.ADButton).gameObject, OnADButton, UIEvents.Click);
        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
    }

    private void OnADButton(PointerEventData data)
    {
        print("여기서 광고를 시청하면 된다.");
    }
    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }
}
