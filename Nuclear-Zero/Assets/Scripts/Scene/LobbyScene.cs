using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        UIManager.Instance.ShowSceneUi<LobbyUI>();
        if (DataManager.Instance.playerInfo.LookTutorial == false)
        {
            UIManager.Instance.ShowPopupUi<TutorialPopupUI>();
            DataManager.Instance.playerInfo.LookTutorial = true;
        }
        if (DataManager.Instance.playerInfo.LookPrologue == false)
        {
            UIManager.Instance.ShowPopupUi<ProloguePopupUI>();
            DataManager.Instance.playerInfo.LookPrologue = true;
        }
        if (GameManager.Instance.OpenStagePopup)
        {
            UIManager.Instance.ShowPopupUi<StagePopupUI>();//StagePopupUI popupUI = 
            //print(DataManager.Instance.playerInfo.SelectChapter);
            //popupUI.GetComponentInChildren<swipe>().SetBtnPage(DataManager.Instance.playerInfo.SelectChapter);
            GameManager.Instance.OpenStagePopup = false;
        }
        UIManager.Instance.FadeIn();
        //DataManager.Instance.LoadText(TextType.Chapter1);
    }

    public override void Clear()
    {
        UIManager.Instance.Clear();
    }
}
