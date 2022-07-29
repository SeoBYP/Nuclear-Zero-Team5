using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class StagePopupUI : PopupUI
{
    enum Stages
    {
        Stage1_1,
        Stage1_2,
        Stage1_3,
        Stage1_4,
        Stage2_1,
        Stage2_2,
        Stage2_3,
        Stage2_4,
        Stage3_1,
        Stage3_2,
        Stage3_3,
        Stage3_4,
        Stage4_1,
        Stage4_2,
        Stage4_3,
        Stage4_4,
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
        //Bind<ChapterPanel>(typeof(Chapters));
        //Bind<Button>(typeof(Buttons));
        //for(int i = 0; i <= (int)Chapters.Chapter4; i++)
        //{
        //    Get<ChapterPanel>(i).Init();
        //}
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
