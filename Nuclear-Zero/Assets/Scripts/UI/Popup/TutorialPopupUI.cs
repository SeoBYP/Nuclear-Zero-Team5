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

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
    }

    private void OnClose(PointerEventData data)
    {
        //if(UIManager.Instance.Get<ProloguePopupUI>() != null)
        //{
        //    UIManager.Instance.Get<ProloguePopupUI>().ClosePopupUI();
        //}
        ClosePopupUI();
    }
}
