using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        GameManager.Instance.LoadGameMap();
        UIManager.Instance.FadeIn();
        ShowDialoguePopup();
        UIManager.Instance.ShowPopupUi<GamePlayPopupUI>();
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
        
    }
}
