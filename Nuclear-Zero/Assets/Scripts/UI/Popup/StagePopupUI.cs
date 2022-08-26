using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;
public class StagePopupUI : PopupUI
{
    enum StageBtns
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

    private swipe _swipe;

    public override void Init()
    {
        base.Init();
        Binds();
        _swipe = GetComponentInChildren<swipe>();
        if(_swipe != null)
        {
            if (GameManager.Instance.OpenStagePopup)
                _swipe.SetBtnPage(DataManager.Instance.playerInfo.SelectChapter);
            else
                _swipe.Init();
        }
    }

    private void Binds()
    {
        Bind<StageBtn>(typeof(StageBtns));
        Bind<Button>(typeof(Buttons));
        Bind<ChapterPanel>(typeof(Chapters));

        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Tutorial).gameObject, OnTutorial, UIEvents.Click);
        SetChapterPanel();
        SetStageBtn();
    }

    private void SetStageBtn()
    {
        for (int i = 0; i < DataManager.Instance.playerInfo.PlayerStages.Count; i++)
        {
            StageBtn stage = Get<StageBtn>(i);
            PlayerStages stages = DataManager.Instance.playerInfo.GetPlayerStages(stage.StageIndex);
            stage.Init();
            // ???????? ?????????????? ?????????? ?? ???????? ???????? ??????????.
            if (stage.StageIndex < DataManager.Instance.playerInfo.ClearStage)
            {
                stage.SetButtonInfo(stages, OnClickStageBtn);
                stage.SetColor(Color.white);
            }
            // ???? ???????? ???????????? ?????? ?????? ????????????.
            else if (stage.StageIndex == DataManager.Instance.playerInfo.ClearStage)
            {
                stage.SetButtonInfo(stages, OnClickStageBtn);
                stage.SetColor(Color.white);
            }
            else
            {
                stage.SetButtonInfo(stages, OnErrorStageBtn);
                stage.SetColor(Color.gray);
            }
                
        }
    }

    private void SetChapterPanel()
    {
        for(int i = 0; i <= (int)Chapters.Chapter4; i++)
        {
            Get<ChapterPanel>(i).Init();
        }
    }

    public void ReSetChapterPanel()
    {
        for(int i = 0; i <= (int)Chapters.Chapter4; i++)
        {
            Get<ChapterPanel>(i).ReSetting();
        }
    }

    private void OnClickStageBtn(string stageText,int stageIndex)
    {
        GameAudioManager.Instance.Play2DSound("Touch");
        UIManager.Instance.ShowPopupUi<StageSelectPopupUI>().SetSeleteStageText(stageText, stageIndex);
    }

    private void OnErrorStageBtn(string stageError, int stageIndex)
    {
        GameAudioManager.Instance.Play2DSound("Touch");
        UIManager.Instance.ShowPopupUi<StageErrorPopupUI>();
    }

    private void OnClose(PointerEventData data)
    {
        ClosePopupUI();
    }

    private void OnTutorial(PointerEventData data)
    {
        UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
    }
}
