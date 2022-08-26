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

    enum Images
    {
        BunkerEffect,
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
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        //InitStageButtons();
        BindEvent(GetButton((int)Buttons.Bunker).gameObject, OnBunker, UIEvents.Click);
        CheckBunkerColor();
        //curChapterIndex = curentChapter.ChapterIndex;
    }

    public void ReSetting()
    {
        CheckBunkerColor();
    }

    private void CheckBunkerColor()
    {
        switch (curChapterIndex)
        {
            case 1:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                        if(DataManager.Instance.playerInfo.FindAllChapterItems(curChapterIndex) == false)
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(true);
                        else
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
                    else
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                        GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
                }
                break;
            case 2:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                        if (DataManager.Instance.playerInfo.FindAllChapterItems(curChapterIndex) == false)
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(true);
                        else
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
                    else
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                        GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
                }
                break;
            case 3:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                        if (DataManager.Instance.playerInfo.FindAllChapterItems(curChapterIndex) == false)
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(true);
                        else
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
                    else
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                        GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
                }
                break;
            case 4:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.white;
                        if (DataManager.Instance.playerInfo.FindAllChapterItems(curChapterIndex) == false)
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(true);
                        else
                            GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    } 
                    else
                    {
                        GetButton((int)Buttons.Bunker).image.color = Color.gray;
                        GetImage((int)Images.BunkerEffect).gameObject.SetActive(false);
                    }
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
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker1";
                    }
                    else
                    {
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                    }

                }
                break;
            case 2:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker2";
                    }
                    else
                    {
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                    }
                }
                break;
            case 3:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker3";
                    }
                    else
                    {
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                    }

                }
                break;
            case 4:
                {
                    if (DataManager.Instance.playerInfo.CheckChapterStageStart(curChapterIndex))
                    {
                        StartCoroutine(ShowBunkerPopupUI());
                        DataManager.Instance.playerInfo.SelectChapter = curChapterIndex;
                        DataManager.Instance.playerInfo.DialogueObjectName = "Bunker4";
                    }
                    else
                    {
                        UIManager.Instance.ShowPopupUi<BunkerErrorPopupUI>();
                    }
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
