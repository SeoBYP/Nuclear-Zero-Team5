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
        Close,
        Tutorial,
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
        for(int i = 0; i <= (int)Chapters.Chapter4; i++)
        {
            Get<ChapterPanel>(i).Init();
        }
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Tutorial).gameObject, OnTutorial, UIEvents.Click);
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnTutorial(PointerEventData data)
    {
        print("OnTutorial");
    }
}
