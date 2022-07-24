using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ShopPopupUI : PopupUI
{
    enum Images
    {

    }

    enum GameObjects
    {
        BackGround,
    }
    //public Animation popupAni;

    public override void Init()
    {
        base.Init();

        Binds();
        //popupAni.Play();
    }

    private void Binds()
    {
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetGameObject((int)GameObjects.BackGround), OnClose, UIEvents.Click);
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

}
