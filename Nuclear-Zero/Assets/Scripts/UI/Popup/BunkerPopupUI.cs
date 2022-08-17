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
        BunkerItem5,
        Close,
    }

    enum GameObjects
    {
        Bunker1,
        Bunker2,
        Bunker3,
        Bunker4,
    }

    enum Images
    {
        Apb1,
        Apb2,
        Apb3,
        Apb4,
        Apb5,
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
        Bind<Image>(typeof(Images));

        SetItemAlphabet();

        BindEvent(GetButton((int)Buttons.BunkerItem1).gameObject, OnBunkerItem1, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem2).gameObject, OnBunkerItem2, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem3).gameObject, OnBunkerItem3, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem4).gameObject, OnBunkerItem4, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem5).gameObject, OnBunkerItem5, UIEvents.Click);
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

    private void SetItemAlphabet()
    {
        for (int i = 0; i < 5; i++)
        {
            CheckGetBunkerItem(i);
        }
    }

    private void CheckGetBunkerItem(int alphabet)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        switch (alphabet)
        {
            case (int)Images.Apb1:
                if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem1)
                    GetImage(alphabet).color = Color.white;
                else
                    GetImage(alphabet).color = Color.black;
                break;
            case (int)Images.Apb2:
                if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2)
                    GetImage(alphabet).color = Color.white;
                else
                    GetImage(alphabet).color = Color.black;
                break;
            case (int)Images.Apb3:
                if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3)
                    GetImage(alphabet).color = Color.white;
                else
                    GetImage(alphabet).color = Color.black;
                break;
            case (int)Images.Apb4:
                if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4)
                    GetImage(alphabet).color = Color.white;
                else
                    GetImage(alphabet).color = Color.black;
                break;
            case (int)Images.Apb5:
                if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem5)
                    GetImage(alphabet).color = Color.white;
                else
                    GetImage(alphabet).color = Color.black;
                break;
        }
        return;
    }

    private void CheckGetBunkerItem(Images alphabet)
    {
        switch (alphabet)
        {
            case Images.Apb1:
                GetImage(alphabet.GetHashCode()).color = Color.white;
                break;
            case Images.Apb2:
                GetImage(alphabet.GetHashCode()).color = Color.white;
                break;
            case Images.Apb3:
                GetImage(alphabet.GetHashCode()).color = Color.white;
                break;
            case Images.Apb4:
                GetImage(alphabet.GetHashCode()).color = Color.white;
                break;
            case Images.Apb5:
                GetImage(alphabet.GetHashCode()).color = Color.white;
                break;
        }
    }

    public void ShowDialoguePopup(int chapterIndex)
    {
        if (chapterIndex == 4)
        {
            DataManager.Instance.playerInfo.GetPlayerChapter(chapterIndex).BunkerStory = true;
            return;
        }
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
            CheckGetBunkerItem(Images.Apb1);
        }
        else
            return;
    }
    private void OnBunkerItem2(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2 = true;
            CheckGetBunkerItem(Images.Apb2);
        }
        else
            return;
    }
    private void OnBunkerItem3(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3 = true;
            CheckGetBunkerItem(Images.Apb3);
        }
        else
            return;
    }
    private void OnBunkerItem4(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4 = true;
            CheckGetBunkerItem(Images.Apb4);
        }
        else
            return;
    }

    private void OnBunkerItem5(PointerEventData data)
    {
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem5 == false)
        {
            UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem5 = true;
            CheckGetBunkerItem(Images.Apb5);
        }
        else
            return;
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
