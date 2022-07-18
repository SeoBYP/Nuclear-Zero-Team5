using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class GameClearPopupUI : PopupUI
{
    enum Buttons
    {
        Replay,
        Exit,
    }

    private ResultStar[] resultStars;

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
        FindResultStars();
    }

    private void FindResultStars()
    {
        resultStars = GetComponentsInChildren<ResultStar>();
        for (int i = 0; i < resultStars.Length; i++)
        {
            resultStars[i].Init();
            resultStars[i].SetShineStar();
        }
    }

    private void OnReplay(PointerEventData data)
    {
        SceneManagerEx.Instance.ReLoadScene(Scene.Game);
    }

    private void OnExit(PointerEventData data)
    {
        SceneManagerEx.Instance.ReLoadScene(Scene.Stage);
    }
}
