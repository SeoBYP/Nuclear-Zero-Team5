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
        UIManager.Instance.ShowPopupUi<NormalEndingPopupUI>();
        if (DataManager.Instance.playerInfo.LookPrologue == false)
        {
            UIManager.Instance.ShowPopupUi<ProloguePopupUI>();
            DataManager.Instance.playerInfo.LookPrologue = true;
        }
        UIManager.Instance.FadeIn();
        
        //DataManager.Instance.LoadText(TextType.Chapter1);
    }

    public override void Clear()
    {
        
    }
}
