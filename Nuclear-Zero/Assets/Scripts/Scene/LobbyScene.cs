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
            UIManager.Instance.ShowPopupUi<StagePopupUI>();
            GameManager.Instance.OpenStagePopup = false;
        }
        DataManager.Instance.playerInfo.ShowSharePopup();
        UIManager.Instance.FadeIn();
        GPGSManager.Instance.SetLogin();
    }

    public override void Clear()
    {
        UIManager.Instance.Clear();
    }
}
