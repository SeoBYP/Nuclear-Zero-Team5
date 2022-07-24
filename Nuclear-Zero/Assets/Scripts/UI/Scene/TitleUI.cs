using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class TitleUI : SceneUI
{
    enum GameObjects
    {
        BackGround,
    }

    enum Buttons
    {
        PlayButton,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton(0).gameObject, OnPlayButton, UIEvents.Click);
        BindEvent(GetGameObject(0).gameObject, OnPlayButton, UIEvents.Click);
    }

    private void OnPlayButton(PointerEventData data)
    {
        SceneManagerEx.Instance.LoadScene(Scene.Lobby);
    }
}
