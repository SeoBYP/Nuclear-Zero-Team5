using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        GameManager.Instance.GameStart();
        ShowDialoguePopup();
        UIManager.Instance.FadeIn();
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
