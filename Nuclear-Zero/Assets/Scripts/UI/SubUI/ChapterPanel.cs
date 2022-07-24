using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ChapterPanel : SubUI
{
    enum StageBtns
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }
    enum Buttons
    {
        Bunker,
    }

    public Chapter currentChapter;

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<StageBtn>(typeof(StageBtns));
        Bind<Button>(typeof(Buttons));
        InitStageButtons();
        BindEvent(GetButton((int)Buttons.Bunker).gameObject, OnBunker, UIEvents.Click);
    }

    private void InitStageButtons()
    {
        for(int i = 0; i <= (int)StageBtns.Stage4; i++)
        {
            Get<StageBtn>(i).Init();
        }
        SetChapters();
    }

    private void SetChapters()
    {
        if (GameData.playerCurChapter < currentChapter)
        {
            for (int i = 0; i <= (int)StageBtns.Stage4; i++)
            {
                Get<StageBtn>(i).SetButtonInfo(currentChapter,i, false, OnErrorStageBtn);
            }
        }
        else if(GameData.playerCurChapter == currentChapter)
        {
            for (int i = 0; i <= (int)StageBtns.Stage4; i++)
            {
                bool clear = false;
                bool currently = false;
                if (i < ((int)GameData.playerCurStage % 4))
                {
                    clear = true;
                    Get<StageBtn>(i).SetButtonInfo(currentChapter, i, clear, OnClickStageBtn, currently);
                    continue;
                }
                if (i == ((int)GameData.playerCurStage % 4))
                {
                    currently = true;
                    Get<StageBtn>(i).SetButtonInfo(currentChapter, i, clear, OnClickStageBtn, currently);
                    continue;
                }
                Get<StageBtn>(i).SetButtonInfo(currentChapter, i, clear, OnErrorStageBtn, currently);
            }
        }
        else
        {
            for (int i = 0; i <= (int)StageBtns.Stage4; i++)
            {
                Get<StageBtn>(i).SetButtonInfo(currentChapter, i, true, OnClickStageBtn);
            }
        }
    }

    private void OnClickStageBtn(string stageText)
    {
        UIManager.Instance.ShowPopupUi<StageCharacterSelectPopupUI>().SetSeleteStageText(stageText);
    }

    private void OnErrorStageBtn(string stageError)
    {
        UIManager.Instance.ShowPopupUi<StageErrorPopupUI>();
    }

    private void OnBunker(PointerEventData data)
    {
        switch (currentChapter)
        {
            case Chapter.Chapter1:
                {
                    if (GameData.playerStars > 0 && GameData.playerStars % 12 == 0)
                        StartCoroutine(ShowBunkerPopupUI());
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case Chapter.Chapter2:
                {
                    if (GameData.playerStars % 24 == 0)
                        StartCoroutine(ShowBunkerPopupUI());
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case Chapter.Chapter3:
                {
                    if (GameData.playerStars % 36 == 0)
                        StartCoroutine(ShowBunkerPopupUI());
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case Chapter.Chapter4:
                {
                    if (GameData.playerStars % 48 == 0)
                        StartCoroutine(ShowBunkerPopupUI());
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
        }
    }

    IEnumerator ShowBunkerPopupUI()
    {
        UIManager.Instance.FadeOut();
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        UIManager.Instance.Get<FadePopupUI>().ClosePopupUI();
        UIManager.Instance.ShowPopupUi<BunkerPopupUI>();
        UIManager.Instance.FadeIn();
    }
}