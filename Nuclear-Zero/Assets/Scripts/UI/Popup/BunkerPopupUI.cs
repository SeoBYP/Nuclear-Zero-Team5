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

    [SerializeField] private ParticleSystem _effect;
    private RectTransform _rectTransform;
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
        _rectTransform = _effect.GetComponent<RectTransform>();
        PlayDefaultParticle(new Vector2(2000, 2000));

        BindEvent(GetButton((int)Buttons.BunkerItem1).gameObject, OnBunkerItem1, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem2).gameObject, OnBunkerItem2, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem3).gameObject, OnBunkerItem3, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem4).gameObject, OnBunkerItem4, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.BunkerItem5).gameObject, OnBunkerItem5, UIEvents.Click);
        BindEvent(GetButton((int)Buttons.Close).gameObject, OnClose, UIEvents.Click);
    }

    private void PlayDefaultParticle(Vector2 position)
    {
        _rectTransform.position = position;
        _effect.gameObject.SetActive(false);
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


    IEnumerator PlayParticle(Vector2 position,int curChapterIndex)
    {
        if (_effect.gameObject.activeSelf)
            yield break;
        GameAudioManager.Instance.Play2DSound("BunkerGetItem");
        _rectTransform.position = position;
        _effect.gameObject.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(1.1f);
        _effect.gameObject.SetActive(false);
        UIManager.Instance.ShowPopupUi<GetItemPopupUI>();
    }

    private void OnBunkerItem1(PointerEventData data)
    {
        
        Vector2 pos = GetButton((int)Buttons.BunkerItem1).transform.position;
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem1 == false)
        {
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem1 = true;
            CheckGetBunkerItem(Images.Apb1);
            StartCoroutine(PlayParticle(pos, curChapterIndex));
        }
        else
            return;
    }
    private void OnBunkerItem2(PointerEventData data)
    {
        Vector2 pos = GetButton((int)Buttons.BunkerItem2).transform.position;
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2 == false)
        {
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem2 = true;
            CheckGetBunkerItem(Images.Apb2);
            StartCoroutine(PlayParticle(pos, curChapterIndex));
        }
        else
            return;
    }
    private void OnBunkerItem3(PointerEventData data)
    {
        Vector2 pos = GetButton((int)Buttons.BunkerItem3).transform.position;
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3 == false)
        {
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem3 = true;
            CheckGetBunkerItem(Images.Apb3);
            StartCoroutine(PlayParticle(pos, curChapterIndex));
        }
        else
            return;
    }
    private void OnBunkerItem4(PointerEventData data)
    {
        Vector2 pos = GetButton((int)Buttons.BunkerItem4).transform.position;
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4 == false)
        {
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem4 = true;
            CheckGetBunkerItem(Images.Apb4);
            StartCoroutine(PlayParticle(pos, curChapterIndex));
        }
        else
            return;
    }

    private void OnBunkerItem5(PointerEventData data)
    {
        Vector2 pos = GetButton((int)Buttons.BunkerItem5).transform.position;
        int curChapterIndex = DataManager.Instance.playerInfo.SelectChapter;
        if (DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem5 == false)
        {
            DataManager.Instance.playerInfo.GetPlayerChapter(curChapterIndex).BunkerItem5 = true;
            CheckGetBunkerItem(Images.Apb5);
            StartCoroutine(PlayParticle(pos, curChapterIndex));
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
