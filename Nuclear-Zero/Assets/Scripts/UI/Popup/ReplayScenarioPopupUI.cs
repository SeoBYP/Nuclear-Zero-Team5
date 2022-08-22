using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ReplayScenarioPopupUI : PopupUI
{
    enum Buttons
    {
        Exit,
    }

    enum GameObjects
    {
        Prologue1,
        Prologue2Text,
        Prologue3Text,
        Prologue4Text,
        Prologue5Text,
        Chapter1_1,
        Chapter1_2,
        Chapter1_3,
        Chapter2,
        Chapter3_1,
        Chapter3_2,
        Chapter4,
        Bunker1,
        Bunker2,
        Bunker2_1,
        Bunker3,
        Bunker4,
        Bunker4_2,
        BadEnding1,
        BadEnding2,
        BadEnding3,
        BadEnding4,
        BadEnding5,
        NormalEnding1,
        NormalEnding2,
        NormalEnding3,
        NormalEnding4,
        NormalEnding5,
        NormalEnding6,
        NormalEnding7,
        HappyEnding1,
        HappyEnding2,
        HappyEnding3,
        HappyEnding4,
        HappyEnding5,
        HappyEnding6,
    }

    public override void Init()
    {
        base.Init();
        Binds();

        SetPrologues();
        SetChapters();
        SetBunkers();
        SetEnding();
    }

    private void Binds()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        BindEvent(GetButton((int)Buttons.Exit).gameObject, OnExit, UIEvents.Click);
    }

    private void SetPrologues()
    {
        if (DataManager.Instance.playerInfo.LookPrologue)
        {
            GetGameObject((int)GameObjects.Prologue1).SetActive(true);
            GetGameObject((int)GameObjects.Prologue2Text).SetActive(true);
            GetGameObject((int)GameObjects.Prologue3Text).SetActive(true);
            GetGameObject((int)GameObjects.Prologue4Text).SetActive(true);
            GetGameObject((int)GameObjects.Prologue5Text).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Prologue1).SetActive(false);
            GetGameObject((int)GameObjects.Prologue2Text).SetActive(false);
            GetGameObject((int)GameObjects.Prologue3Text).SetActive(false);
            GetGameObject((int)GameObjects.Prologue4Text).SetActive(false);
            GetGameObject((int)GameObjects.Prologue5Text).SetActive(false);
        }
    }

    private void SetChapters()
    {
        if (DataManager.Instance.playerInfo.GetPlayerChapter(1).ChapterStory)
        {
            GetGameObject((int)GameObjects.Chapter1_1).SetActive(true);
            GetGameObject((int)GameObjects.Chapter1_2).SetActive(true);
            GetGameObject((int)GameObjects.Chapter1_3).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Chapter1_1).SetActive(false);
            GetGameObject((int)GameObjects.Chapter1_2).SetActive(false);
            GetGameObject((int)GameObjects.Chapter1_3).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.GetPlayerChapter(2).ChapterStory)
        {
            GetGameObject((int)GameObjects.Chapter2).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Chapter2).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.GetPlayerChapter(3).ChapterStory)
        {
            GetGameObject((int)GameObjects.Chapter3_1).SetActive(true);
            GetGameObject((int)GameObjects.Chapter3_2).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Chapter3_1).SetActive(false);
            GetGameObject((int)GameObjects.Chapter3_2).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.GetPlayerChapter(4).ChapterStory)
        {
            GetGameObject((int)GameObjects.Chapter4).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Chapter4).SetActive(false);
        }
    }

    private void SetBunkers()
    {
        if (DataManager.Instance.playerInfo.GetPlayerChapter(1).BunkerStory)
        {
            GetGameObject((int)GameObjects.Bunker1).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Bunker1).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.GetPlayerChapter(2).BunkerStory)
        {
            GetGameObject((int)GameObjects.Bunker2).SetActive(true);
            GetGameObject((int)GameObjects.Bunker2_1).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Bunker2).SetActive(false);
            GetGameObject((int)GameObjects.Bunker2_1).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.GetPlayerChapter(3).BunkerStory)
        {
            GetGameObject((int)GameObjects.Bunker3).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Bunker3).SetActive(false);
        }
        if (DataManager.Instance.playerInfo.GetPlayerChapter(4).BunkerStory)
        {
            GetGameObject((int)GameObjects.Bunker4).SetActive(true);
            GetGameObject((int)GameObjects.Bunker4_2).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.Bunker4).SetActive(false);
            GetGameObject((int)GameObjects.Bunker4_2).SetActive(false);
        }
    }

    private void SetEnding()
    {
        if (DataManager.Instance.playerInfo.BadEnding)
        {
            GetGameObject((int)GameObjects.BadEnding1).SetActive(true);
            GetGameObject((int)GameObjects.BadEnding2).SetActive(true);
            GetGameObject((int)GameObjects.BadEnding3).SetActive(true);
            GetGameObject((int)GameObjects.BadEnding4).SetActive(true);
            GetGameObject((int)GameObjects.BadEnding5).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.BadEnding1).SetActive(false);
            GetGameObject((int)GameObjects.BadEnding2).SetActive(false);
            GetGameObject((int)GameObjects.BadEnding3).SetActive(false);
            GetGameObject((int)GameObjects.BadEnding4).SetActive(false);
            GetGameObject((int)GameObjects.BadEnding5).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.NormalEnding)
        {
            GetGameObject((int)GameObjects.NormalEnding1).SetActive(true);
            GetGameObject((int)GameObjects.NormalEnding2).SetActive(true);
            GetGameObject((int)GameObjects.NormalEnding3).SetActive(true);
            GetGameObject((int)GameObjects.NormalEnding4).SetActive(true);
            GetGameObject((int)GameObjects.NormalEnding5).SetActive(true);
            GetGameObject((int)GameObjects.NormalEnding6).SetActive(true);
            GetGameObject((int)GameObjects.NormalEnding7).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.NormalEnding1).SetActive(false);
            GetGameObject((int)GameObjects.NormalEnding2).SetActive(false);
            GetGameObject((int)GameObjects.NormalEnding3).SetActive(false);
            GetGameObject((int)GameObjects.NormalEnding4).SetActive(false);
            GetGameObject((int)GameObjects.NormalEnding5).SetActive(false);
            GetGameObject((int)GameObjects.NormalEnding6).SetActive(false);
            GetGameObject((int)GameObjects.NormalEnding7).SetActive(false);
        }

        if (DataManager.Instance.playerInfo.HappyEnding)
        {
            GetGameObject((int)GameObjects.HappyEnding1).SetActive(true);
            GetGameObject((int)GameObjects.HappyEnding2).SetActive(true);
            GetGameObject((int)GameObjects.HappyEnding3).SetActive(true);
            GetGameObject((int)GameObjects.HappyEnding4).SetActive(true);
            GetGameObject((int)GameObjects.HappyEnding5).SetActive(true);
            GetGameObject((int)GameObjects.HappyEnding6).SetActive(true);
        }
        else
        {
            GetGameObject((int)GameObjects.HappyEnding1).SetActive(false);
            GetGameObject((int)GameObjects.HappyEnding2).SetActive(false);
            GetGameObject((int)GameObjects.HappyEnding3).SetActive(false);
            GetGameObject((int)GameObjects.HappyEnding4).SetActive(false);
            GetGameObject((int)GameObjects.HappyEnding5).SetActive(false);
            GetGameObject((int)GameObjects.HappyEnding6).SetActive(false);
        }
    }

    private void OnExit(PointerEventData data)
    {
        ClosePopupUI();
    }
}
