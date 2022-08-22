using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        GameManager.Instance.LoadGameMap();
        SetGameStageBGM();
        UIManager.Instance.FadeIn();
        UIManager.Instance.ShowPopupUi<GamePlayPopupUI>();
        ShowDialoguePopup();
    }

    private void SetGameStageBGM()
    {
        int stageindex = DataManager.Instance.playerInfo.SelectStage;
        switch (stageindex)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                DataManager.Instance.playerInfo.SelectChapter = 1;
                GameAudioManager.Instance.PlayBackGround("Chapter1BGM");
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                DataManager.Instance.playerInfo.SelectChapter = 2;
                GameAudioManager.Instance.PlayBackGround("Chapter2BGM");
                break;
            case 9:
            case 10:
            case 11:
            case 12:
                DataManager.Instance.playerInfo.SelectChapter = 3;
                GameAudioManager.Instance.PlayBackGround("Chapter3BGM");
                break;
            case 13:
            case 14:
            case 15:
            case 16:
                DataManager.Instance.playerInfo.SelectChapter = 4;
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
        GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        UIManager.Instance.Clear();
        //GameAudioManager.Instance.DestroyWarning();
    }
}
