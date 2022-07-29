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
        UIManager.Instance.ShowPopupUi<StageSelectPopupUI>().SetSeleteStageText(stageText);
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
                    if (DataManager.Instance.playerInfo.PlayerStars >= 12)
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker1";
                    }
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case Chapter.Chapter2:
                {
                    if (DataManager.Instance.playerInfo.PlayerStars >= 24)
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker2";
                    }
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case Chapter.Chapter3:
                {
                    if (DataManager.Instance.playerInfo.PlayerStars >= 36)
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker3";
                    }
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case Chapter.Chapter4:
                {
                    if (DataManager.Instance.playerInfo.PlayerStars >= 48)
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker4";
                    }
                    else
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            default:
                DataManager.Instance.playerInfo.DialogueObjectName = string.Empty;
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
