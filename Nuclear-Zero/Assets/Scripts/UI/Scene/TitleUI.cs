using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class TitleUI : SceneUI
{
    enum Buttons
    {
        PlayButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton(0).gameObject, OnPlayButton, UIEvents.Click);
    }

    private void OnPlayButton(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }
}
