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
        Exit,
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
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
    }

    private void OnExit(PointerEventData data)
    {
        //print("OnExit");
        GameManager.Instance.GamePause();
        ClosePopupUI();
        //SceneManagerEx.Instance.ReLoadScene(Scene.Lobby);
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }

    private void OnReplay(PointerEventData data)
    {
        ClosePopupUI();
        UIManager.Instance.ShowPopupUi<CountDownPopupUI>();
    }
}
