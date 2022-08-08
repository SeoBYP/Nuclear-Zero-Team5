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
        GameAudioManager.Instance.PlayBackGround("LobbyBGM");
        //UIManager.Instance.ShowPopupUi<BadEndingPopupUI>();
        if (DataManager.Instance.playerInfo.LookPrologue == false)
        {
            UIManager.Instance.ShowPopupUi<ProloguePopupUI>();
            DataManager.Instance.playerInfo.LookPrologue = true;
        }
        if (DataManager.Instance.playerInfo.GetPlayerStages(5).Cleared)
        {
            DataManager.Instance.playerInfo.ShowEnding();
        }

        UIManager.Instance.FadeIn();
        //DataManager.Instance.LoadText(TextType.Chapter1);
    }

    public override void Clear()
    {
        UIManager.Instance.Clear();
    }
}
