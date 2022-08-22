using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class ChapterPanel : SubUI
{ 
    enum Buttons
    {
        Bunker,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    //private PlayerChapter curentChapter;
    [SerializeField] private int curChapterIndex;
    private void Binds()
    {
        //Bind<StageBtn>(typeof(StageBtns));
        Bind<Button>(typeof(Buttons));
        //InitStageButtons();
        BindEvent(GetButton((int)Buttons.Bunker).gameObject, OnBunker, UIEvents.Click);
        CheckBunkerColor();
        //curChapterIndex = curentChapter.ChapterIndex;
    }

    private void CheckBunkerColor()
    {
        switch (curChapterIndex)
        {
            case 1:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                    else
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                }
                break;
            case 2:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                    else
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                }
                break;
            case 3:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                    else
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                }
                break;
            case 4:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                    else
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                }
                break;
        }
    }

    private void OnBunker(PointerEventData data)
    {
        //print(DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex));
        switch (curChapterIndex)
        {
            case 1:
                {
                    //if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker1";
                    }
                    //else
                    //{
                    //    UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                    //}
                        
                }
                break;
            case 2:
                {
                    //if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker2";
                    }
                    //else
                    //{
                    //    UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                    //}  
                }
                break;
            case 3:
                {
                    //if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker3";
                    }
                    //else
                    //    UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            case 4:
                {
                    //if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker4";
                    }
                    //else
                    //    UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                }
                break;
            default:
                DataManager.Instance.playerInfo.DialogueObjectName = string.Empty;
                break;
        }
    }

    IEnumerator ShowBunkerPopupUI()
    {
        GameAudioManager.Instance.Play2DSound("OpenBunker");
        UIManager.Instance.FadeOut();
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        UIManager.Instance.Get<FadePopupUI>().ClosePopupUI();
        UIManager.Instance.ShowPopupUi<BunkerPopupUI>().ShowDialoguePopup(curChapterIndex);
        UIManager.Instance.FadeIn();
    }
}
