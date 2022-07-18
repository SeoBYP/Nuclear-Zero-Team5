using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameOverPopupUI : PopupUI
{
    private int timer;

    enum Buttons
    {
        Exit,
        Replay,
    }

    enum Texts 
    {
        Timer,
    }

    private bool IsExit = false;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Replay).gameObject, OnReplay, UIEvents.Click);
    }

    private void OnExit(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(Scene.Stage);
    }

    private void OnReplay(PointerEventData data)
    {
        SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }
}
