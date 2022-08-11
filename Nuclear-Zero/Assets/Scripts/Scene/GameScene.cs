using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        //GoogleMobileAdsManager.Instance.DestroyBannerAd();
        GameManager.Instance.LoadGameMap();
        SetGameStageBGM();
        UIManager.Instance.FadeIn();
        UIManager.Instance.ShowPopupUi<GamePlayPopupUI>();
        ShowDialoguePopup();
    }

    private void SetGameStageBGM()
    {
        int chapterindex = DataManager.Instance.playerInfo.SelectChapter;
        switch (chapterindex)
        {
            case 1:
                GameAudioManager.Instance.PlayBackGround("Chapter1BGM");
                break;
            case 2:
                GameAudioManager.Instance.PlayBackGround("Chapter2BGM");
                break;
            case 3:
                GameAudioManager.Instance.PlayBackGround("Chapter3BGM");
                break;
            case 4:
                GameAudioManager.Instance.PlayBackGround("Chapter4BGM");
                break;
        }
    }

    private void ShowDialoguePopup()
    {
        if (DataManager.Instance.playerInfo.DialogueObjectName != string.Empty)
        {
            int curchapterIndex = DataManager.Instance.playerInfo.SelectChapter;
            if (DataManager.Instance.playerInfo.GetPlayerChapter(curchapterIndex).ChapterStory == false)
            {
                UIManager.Instance.ShowPopupUi<DialoguePopupUI>();
                DataManager.Instance.playerInfo.GetPlayerChapter(curchapterIndex).ChapterStory = true;
            }
        }
    }

    public override void Clear()
    {
        //UIManager.Instance.Clear();
        //GameAudioManager.Instance.DestroyWarning();
    }
}
