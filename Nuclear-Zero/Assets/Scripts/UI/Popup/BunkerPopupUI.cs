using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Define;

public class BunkerPopupUI : PopupUI
{
    enum Buttons
    {
        BunkerItem1,
        BunkerItem2,
        BunkerItem3,
        BunkerItem4,
        Close,
    }

    enum GameObjects
    {
        Bunker1,
        Bunker2,
        Bunker3,
        Bunker4,
    }

    public override void Init()
    {
        base.Init();
        Binds();
    }

    private void Binds()
    {
        Bind<GameObject>(typeof(GameObjects));
        SetChapterBunker();
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.BunkerItem1).gameObject, OnBunkerItem1, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem2).gameObject, OnBunkerItem2, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem3).gameObject, OnBunkerItem3, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem4).gameObject, OnBunkerItem4, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
    }

    private void SetChapterBunker()
    {
        for(int i = 0; i < 4; i++)
        {
            GetGameObject(i).SetActive(false);
        }
        int chapterIndex = DataManager.Instance.playerInfo.SelectChapter - 1;
        switch(chapterIndex)
        {
            case (int)GameObjects.Bunker1:
                GetGameObject(chapterIndex).SetActive(true);
                break;
            case (int)GameObjects.Bunker2:
                GetGameObject(chapterIndex).SetActive(true);
                break;
            case (int)GameObjects.Bunker3:
                GetGameObject(chapterIndex).SetActive(true);
                break;
            case (int)GameObjects.Bunker4:
                GetGameObject(chapterIndex).SetActive(true);
                break;
        }
    }

    public void ShowDialoguePopup(int chapterIndex)
    {
        if (DataManager.Instance.playerInfo.GetPlayerChapter(chapterIndex).BunkerStory == false)
        {
            UIManager.Instance.ShowPopupUi<DialoguePopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(chapterIndex).BunkerStory = true;
        }
    }

    private void OnBunkerItem1(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem1 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem1 = true;
        }
    }
    private void OnBunkerItem2(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2 = true;
        }
    }
    private void OnBunkerItem3(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3 = true;
        }
    }
    private void OnBunkerItem4(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4 = true;
        }
    }

    public void OnExit()
    {
        StartCoroutine(CloseBunkerPopupUI());
    }

    private void OnClose(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.FindAllChapterItems(curChapterIndex) == false)
        {
            UIManager.Instance.ShowPopupUi<BunkerExitPopupUI>();
            return;
        }
        OnExit();
    }
    IEnumerator CloseBunkerPopupUI()
    {
        UIManager.Instance.FadeOut();
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        UIManager.Instance.Get<FadePopupUI>().ClosePopupUI();
        ClosePopupUI();
        UIManager.Instance.FadeIn();

    }
}
