using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class StagePopupUI : PopupUI
{
    enum Chapters
    {
        Chapter1,
        Chapter2,
        Chapter3,
        Chapter4,
    }

    enum Buttons
    {
        Exit,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<ChapterPanel>(typeof(Chapters));
        Bind<Button>(typeof(Buttons));
        for(int i = 0; i < (int)Chapters.Chapter4; i++)
        {
            Get<ChapterPanel>(i).Init();
        }
        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
    }

    private void OnExit(PointerEventData data)
    {
        ClosePopupUI();
    }
}
